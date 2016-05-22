using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace A319TS
{
    public enum Partitions { Shared, Primary, Secondary };
    class Simulation
    {
        public const int MsInDay = 86400000;
        public const int RecordInterval = 100;
        public BackgroundWorker PrimaryWorker;
        public BackgroundWorker SecondaryWorker;
        public string Filename;
        private Project Project;
        private Project PrimaryProject;
        private Project SecondaryProject;
        private List<Vehicle> _primaryVehicles;
        private List<Vehicle> _secondaryVehicles;
        private List<Node> _primaryInboundNodes;
        private List<Node> _secondaryInboundNodes;
        private List<Node> _primaryHomeNodes;
        private List<Node> _secondaryHomeNodes;

        public event EventHandler<EventArgs> SimulationDone;
        protected virtual void OnSimulationDone()
        {
            SimulationDone?.Invoke(this, new EventArgs());
        }

        public Simulation(Project project)
        {
            Project = project;
            PrimaryProject = project.Clone() as Project;
            SecondaryProject = project.Clone() as Project;
            
            PrimaryWorker = new BackgroundWorker();
            PrimaryWorker.WorkerReportsProgress = true;
            PrimaryWorker.WorkerSupportsCancellation = true;
            PrimaryWorker.DoWork += Simulate;
            PrimaryWorker.RunWorkerCompleted += SimulationCompleted;

            SecondaryWorker = new BackgroundWorker();
            SecondaryWorker.WorkerReportsProgress = true;
            SecondaryWorker.WorkerSupportsCancellation = true;
            SecondaryWorker.DoWork += Simulate;
            SecondaryWorker.RunWorkerCompleted += SimulationCompleted;

            _primaryVehicles = new List<Vehicle>();
            _secondaryVehicles = new List<Vehicle>();
        }

        public void Run()
        {
            _primaryInboundNodes = PrimaryProject.Nodes.FindAll(n => n.Type == NodeTypes.Inbound);
            _secondaryInboundNodes = SecondaryProject.Nodes.FindAll(n => n.Type == NodeTypes.Inbound);
            if (_primaryInboundNodes.Count == 0)
                throw new Exception("No inbound nodes found");

            _primaryHomeNodes = PrimaryProject.Nodes.FindAll(n => n.Type == NodeTypes.Home);
            _secondaryHomeNodes = SecondaryProject.Nodes.FindAll(n => n.Type == NodeTypes.Home);
            if (_primaryHomeNodes.Count == 0)
                throw new Exception("No home nodes found");

            SetupVehicles();

            Tuple<List<Vehicle>, Project, Partitions> primaryArguments;
            primaryArguments = new Tuple<List<Vehicle>, Project, Partitions>(_primaryVehicles, PrimaryProject, Partitions.Primary);
            Tuple<List<Vehicle>, Project, Partitions> secondaryArguments;
            secondaryArguments = new Tuple<List<Vehicle>, Project, Partitions>(_secondaryVehicles, SecondaryProject, Partitions.Secondary);

            PrimaryWorker.RunWorkerAsync(primaryArguments);
            SecondaryWorker.RunWorkerAsync(secondaryArguments);
        }
        public void Cancel()
        {
            PrimaryWorker.CancelAsync();
            SecondaryWorker.CancelAsync();
        }
        private void Simulate(object sender, DoWorkEventArgs args)
        {
            var arguments = args.Argument as Tuple<List<Vehicle>, Project, Partitions>;
            BackgroundWorker worker = sender as BackgroundWorker;
            List<Vehicle> vehicles = arguments.Item1;
            Project project = arguments.Item2;
            Partitions partition = arguments.Item3;
            int vehicleCount = vehicles.Count;
            int onePercent = MsInDay / 100;
            bool error = false;

            try
            {
                for (int i = 0; i < MsInDay; i += Project.Settings.StepSize)
                {
                    if (worker.CancellationPending)
                        break;
                    if (i % onePercent == 0)
                        worker.ReportProgress(i, i / onePercent + "% " + partition);
                    foreach (LightController controller in project.LightControllers)
                        controller.Update(project.Settings.StepSize);
                    for (int j = 0; j < vehicleCount; j++)
                        vehicles[j].Drive(i);
                }
            }
            catch (Exception e)
            {
                worker.ReportProgress(0, partition + " crashed with error: " + e.Message);
                error = true;
                Cancel();
            }

            if (error && worker.CancellationPending)
            {
                args.Cancel = true;
            }
            else if (worker.CancellationPending)
            {
                worker.ReportProgress(0, partition + " cancelled");
                args.Cancel = true;
            }
            else worker.ReportProgress(MsInDay, partition + " completed");
        }
        private void SimulationCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            if (PrimaryWorker.IsBusy || SecondaryWorker.IsBusy || args.Cancelled)
                return;
            else
            {
                List<VehicleData> primaryData = new List<VehicleData>();
                foreach (Vehicle vehicle in _primaryVehicles)
                    primaryData.Add(vehicle.ExtractData());

                List<VehicleData> secondaryData = new List<VehicleData>();
                foreach (Vehicle vehicle in _secondaryVehicles)
                    secondaryData.Add(vehicle.ExtractData());

                SimulationData data = new SimulationData(Project, primaryData, secondaryData);
                FileHandler.SaveSimulation(data);
                Filename = data.Filename;
                OnSimulationDone();
            }
        }

        // SetupVehicles //
        private enum SetupOrder { Equal, PrimaryFirst, SecondaryFirst }
        private SetupOrder GetOrder()
        {
            if (Project.Settings.PrimaryCarCount == Project.Settings.SecondaryCarCount)
                return SetupOrder.Equal;
            else if (Project.Settings.PrimaryCarCount < Project.Settings.SecondaryCarCount)
                return SetupOrder.PrimaryFirst;
            else return SetupOrder.SecondaryFirst;
        }
        private void SetupVehicles()
        {
            SetupOrder order = GetOrder();
            if (order == SetupOrder.PrimaryFirst)
            {
                _primaryVehicles.AddRange(CreateVehicles(Partitions.Primary, Project.Settings.PrimaryCarCount));
                _secondaryVehicles.AddRange(CopyVehicles(Partitions.Primary));
                int additionalVehicles = Project.Settings.SecondaryCarCount - Project.Settings.PrimaryCarCount;
                _secondaryVehicles.AddRange(CreateVehicles(Partitions.Secondary, additionalVehicles));
            }
            else if (order == SetupOrder.SecondaryFirst)
            {
                _secondaryVehicles.AddRange(CreateVehicles(Partitions.Secondary, Project.Settings.SecondaryCarCount));
                _primaryVehicles.AddRange(CopyVehicles(Partitions.Secondary));
                int additionalVehicles = Project.Settings.PrimaryCarCount - Project.Settings.SecondaryCarCount;
                _primaryVehicles.AddRange(CreateVehicles(Partitions.Primary, additionalVehicles));
            }
            else
            {
                _primaryVehicles.AddRange(CreateVehicles(Partitions.Primary, Project.Settings.PrimaryCarCount));
                _secondaryVehicles.AddRange(CopyVehicles(Partitions.Primary));
            }
        }
        private Vehicle[] CopyVehicles(Partitions partition)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            if (partition == Partitions.Primary)
            {
                Pathfinder.SetProject(SecondaryProject, Partitions.Secondary);
                foreach (Vehicle vehicle in _primaryVehicles)
                    vehicles.Add(new Vehicle(SecondaryProject, vehicle.Home, vehicle.Destination,
                        vehicle.Type, vehicle.ToDestTime, vehicle.ToHomeTime));
            }
            else
            {
                Pathfinder.SetProject(PrimaryProject, Partitions.Primary);
                foreach (Vehicle vehicle in _secondaryVehicles)
                    vehicles.Add(new Vehicle(PrimaryProject, vehicle.Home, vehicle.Destination,
                        vehicle.Type, vehicle.ToDestTime, vehicle.ToHomeTime));
            }
            return vehicles.ToArray();
        }
        private Vehicle[] CreateVehicles(Partitions partition, int count)
        {
            Project project;
            if (partition == Partitions.Primary) project = PrimaryProject;
            else project = SecondaryProject;

            Node[] homes = GetHomes(partition, count);
            Destination[] destinations = GetDestinations(partition, count);
            VehicleType[] vehicleTypes = GetVehicleTypes(count);
            int[] toDestTimes = GetTimes(partition, count, true);
            int[] toHomeTimes = GetTimes(partition, count, false);

            Pathfinder.SetProject(project, partition);
            Vehicle[] vehicles = new Vehicle[count];
            for (int i = 0; i < count; i++)
                vehicles[i] = new Vehicle(project, homes[i], destinations[i], 
                    vehicleTypes[i], toDestTimes[i], toHomeTimes[i]);
            return vehicles;
        }
        // GetHomes //
        private Node[] GetHomes(Partitions partition, int count)
        {
            int inbound;
            if (partition == Partitions.Primary) inbound = Project.Settings.PrimaryInbound;
            else inbound = Project.Settings.SecondaryInbound;
            int inboundCount = count * (inbound / 100);

            List<Node> homes = new List<Node>();
            for (int i = 0; i < inboundCount; i++)
                homes.Add(GetNextInbound(partition));

            int remaining = count - inboundCount;
            for (int i = 0; i < remaining; i++)
                homes.Add(GetNextHome(partition));

            return homes.ToArray();
        }
        private int _primaryInboundIndex = 0;
        private int _secondaryInboundIndex = 0;
        private Node GetNextInbound(Partitions partition)
        {
            Node inbound;
            if (partition == Partitions.Primary)
            {
                inbound = _primaryInboundNodes[_primaryInboundIndex];
                if (_primaryInboundIndex + 1 == _primaryInboundNodes.Count) _primaryInboundIndex = 0;
                else _primaryInboundIndex++;
            }
            else
            {
                inbound = _secondaryInboundNodes[_primaryInboundIndex];
                if (_secondaryInboundIndex + 1 == _secondaryInboundNodes.Count) _secondaryInboundIndex = 0;
                else _secondaryInboundIndex++;
            }
            return inbound;
        }
        private int _primaryHomeIndex = 0;
        private int _secondaryHomeIndex = 0;
        private Node GetNextHome(Partitions partition)
        {
            Node home;
            if (partition == Partitions.Primary)
            {
                home = _primaryHomeNodes[_primaryHomeIndex];
                if (_primaryHomeIndex + 1 == _primaryHomeNodes.Count) _primaryHomeIndex = 0;
                else _primaryHomeIndex++;
            }
            else
            {
                home = _secondaryHomeNodes[_secondaryHomeIndex];
                if (_secondaryHomeIndex + 1 == _secondaryHomeNodes.Count) _secondaryHomeIndex = 0;
                else _secondaryHomeIndex++;
            }
            return home;
        }
        // GetDestinations //
        private Destination[] GetDestinations(Partitions partition, int count)
        {
            int outbound;
            if (partition == Partitions.Primary) outbound = Project.Settings.PrimaryOutbound;
            else outbound = Project.Settings.SecondaryOutbound;
            int outboundCount = count * (outbound / 100);

            List<Destination> destinations = new List<Destination>();
            for (int i = 0; i < outboundCount; i++)
                destinations.Add(null);

            int remaining = count - outboundCount;
            foreach (DestinationType destType in Project.DestinationTypes)
            {
                if (destType.Distribution == 0)
                    continue;

                List<Destination> destsWithType = Project.Destinations.FindAll(d => d.Type == destType);
                if (destsWithType.Count == 0)
                    throw new Exception("No destinations with type: " + destType.Name);

                int addAmount = Convert.ToInt32(remaining * (destType.Distribution / 100));
                int index = 0;
                for (int i = 0; i < addAmount; i++)
                {
                    destinations.Add(destsWithType[index]);
                    if (index + 1 == destsWithType.Count) index = 0;
                    else index++;
                }
            }

            return destinations.ToArray();
        }
        private VehicleType[] GetVehicleTypes(int count)
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();
            foreach (VehicleType vehicleType in Project.VehicleTypes)
            {
                if (vehicleType.Distribution == 0)
                    continue;

                int addAmount = Convert.ToInt32(count * (vehicleType.Distribution / 100));
                for (int i = 0; i < addAmount; i++)
                    vehicleTypes.Add(vehicleType);
            }
            return vehicleTypes.ToArray();
        }
        private int[] GetTimes(Partitions partition, int count, bool toDest)
        {
            int spread, time;
            if (partition == Partitions.Primary)
            {
                spread = Project.Settings.PrimaryTimeSpread;
                if (toDest) time = Project.Settings.PrimaryToDestTime;
                else time = Project.Settings.PrimaryToHomeTime;
            }
            else
            {
                spread = Project.Settings.SecondaryTimeSpread;
                if (toDest) time = Project.Settings.SecondaryToDestTime;
                else time = Project.Settings.SecondaryToHomeTime;
            }
                
            Random random = new Random();
            List<int> times = new List<int>();
            for (int i = 0; i < count; i++)
                times.Add(time + random.Next(spread * -1, spread));
            return times.ToArray();
        }
    }
}