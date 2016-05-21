﻿using System;
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
        }

        public void Run()
        {
            _primaryVehicles = CreateVehicles(Partitions.Primary);
            _secondaryVehicles = CreateVehicles(Partitions.Secondary);

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
            if (args.Argument == null && args.GetType() != typeof(Tuple<List<Vehicle>, Project, Partitions>))
                throw new ArgumentException();

            var arguments = args.Argument as Tuple<List<Vehicle>, Project, Partitions>;
            BackgroundWorker worker = sender as BackgroundWorker;
            List<Vehicle> vehicles = arguments.Item1;
            Project project = arguments.Item2;
            Partitions partition = arguments.Item3;
            int vehicleCount = vehicles.Count;
            int onePercent = MsInDay / 100;

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

            if (worker.CancellationPending)
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

                SimulationData data = new SimulationData(Project.Clone() as Project, primaryData, secondaryData);
                FileHandler.SaveSimulation(data);
                Filename = data.Filename;
                OnSimulationDone();
            }
        }
        private enum SetupOrder { Equal, PrimaryFirst, SecondaryFirst }
        private void SetupVehicles()
        {
            SetupOrder order = GetOrder();
        }
        private SetupOrder GetOrder()
        {
            if (Project.Settings.PrimaryCarCount == Project.Settings.SecondaryCarCount)
                return SetupOrder.Equal;
            else if (Project.Settings.PrimaryCarCount < Project.Settings.SecondaryCarCount)
                return SetupOrder.PrimaryFirst;
            else return SetupOrder.SecondaryFirst;
        }






















































































































        // CreateVehicles //
        private List<Vehicle> CreateVehicles(Partitions partition)
        {
            int carCount, inbound, outbound, timeSpread, toDestTime, toHomeTime;

            if (partition == Partitions.Primary)
            {
                Pathfinder.SetProject(PrimaryProject, Partitions.Primary);
                carCount = Project.Settings.PrimaryCarCount;
                inbound = Project.Settings.PrimaryInbound;
                outbound = Project.Settings.PrimaryOutbound;
                timeSpread = Project.Settings.PrimaryTimeSpread;
                toDestTime = Project.Settings.PrimaryToDestTime;
                toHomeTime = Project.Settings.PrimaryToHomeTime;
            }
            else
            {
                Pathfinder.SetProject(SecondaryProject, Partitions.Secondary);
                carCount = Project.Settings.SecondaryCarCount;
                inbound = Project.Settings.SecondaryInbound;
                outbound = Project.Settings.SecondaryOutbound;
                timeSpread = Project.Settings.SecondaryTimeSpread;
                toDestTime = Project.Settings.SecondaryToDestTime;
                toHomeTime = Project.Settings.SecondaryToHomeTime;
            }

            List<Node> homes = GetHomes(carCount, inbound);
            List<Destination> destinations = GetDestinations(carCount, outbound);
            List<VehicleType> vehicleTypes = GetVehicleTypes(carCount);
            List<int> toDestTimes = GetTimes(carCount, timeSpread, toDestTime);
            List<int> toHomeTimes = GetTimes(carCount, timeSpread, toHomeTime);

            List<Vehicle> vehicles = new List<Vehicle>();

            if(partition == Partitions.Primary)
                for (int i = 0; i < carCount; i++)
                    vehicles.Add(new Vehicle(Project, homes[i], destinations[i], vehicleTypes[i], toDestTimes[i], toHomeTimes[i]));
            else
            {
                if (carCount >= _primaryVehicles.Count)
                {
                    for (int i = 0; i < _primaryVehicles.Count; i++)
                        vehicles.Add(new Vehicle(Project, _primaryVehicles[i].Home, _primaryVehicles[i].Destination, _primaryVehicles[i].Type, toDestTimes[i], toHomeTimes[i]));

                    for (int i = _primaryVehicles.Count; i < carCount; i++)
                        vehicles.Add(new Vehicle(Project, homes[i], destinations[i], vehicleTypes[i], toDestTimes[i], toHomeTimes[i]));
                }
                else
                    for (int i = 0; i < carCount; i++)
                        vehicles.Add(new Vehicle(Project, _primaryVehicles[i].Home, _primaryVehicles[i].Destination, _primaryVehicles[i].Type, toDestTimes[i], toHomeTimes[i]));
            }

            return vehicles;
        }
        private List<Node> GetHomes(int carCount, int inbound)
        {
            List<Node> homes = new List<Node>();

            List<Node> inboundNodes = Project.Nodes.FindAll(n => n.Type == NodeTypes.Inbound);

            int inboundNodesCount = inboundNodes.Count;

            if (inboundNodesCount == 0)
                throw new Exception("No inbound nodes found");
            
            int inboundIndex = 0;
            for (int i = 0; i < inbound; i++)
            {
                homes.Add(inboundNodes[inboundIndex]);
                if (inboundIndex + 1 == inboundNodesCount) inboundIndex = 0;
                else inboundIndex++;
            }

            List<Node> homeNodes = Project.Nodes.FindAll(n => n.Type == NodeTypes.Home);

            int homeNodesCount = homeNodes.Count;

            if (homeNodesCount == 0)
                throw new Exception("No home nodes found");

            int homeIndex = 0;
            for (int i = 0; i < carCount - inbound; i++)
            {
                homes.Add(homeNodes[homeIndex]);
                if (homeIndex + 1 == homeNodesCount) homeIndex = 0;
                else homeIndex++;
            }

            return homes;
        }
        private List<Destination> GetDestinations(int carCount, int outbound)
        {
            List<Destination> destinations = new List<Destination>();
            for (int i = 0; i < outbound; i++)
                destinations.Add(null);

            foreach (DestinationType destType in Project.DestinationTypes)
            {
                if (destType.Distribution == 0)
                    continue;

                List<Destination> destsWithType = Project.Destinations.FindAll(d => d.Type == destType);

                int destsWithTypeCount = destsWithType.Count;

                if (destsWithTypeCount == 0)
                    throw new Exception("No destinations with type: " + destType.Name);

                double addAmount = (destType.Distribution / 100) * (carCount - outbound);
                int index = 0;

                for (int i = 0; i < Math.Round(addAmount); i++)
                {
                    destinations.Add(destsWithType[index]);
                    if (index + 1 == destsWithTypeCount) index = 0;
                    else index++;
                }
            }

            while (destinations.Count > carCount)
                destinations.Remove(destinations.Last());

            return destinations;
        }
        private List<VehicleType> GetVehicleTypes(int carCount)
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();
            foreach (VehicleType vehicleType in Project.VehicleTypes)
            {
                if (vehicleType.Distribution == 0)
                    continue;

                double addAmount = (vehicleType.Distribution / 100) * (carCount);
                for (int i = 0; i < Math.Round(addAmount); i++)
                    vehicleTypes.Add(vehicleType);
            }

            while (vehicleTypes.Count > carCount)
                vehicleTypes.Remove(vehicleTypes.Last());

            return vehicleTypes;
        }
        private List<int> GetTimes(int carCount, int spread, int startTime)
        {
            Random random = new Random();
            List<int> times = new List<int>();
            for (int i = 0; i < carCount; i++)
                times.Add(startTime + random.Next(spread * -1, spread));
            
            return times;
        }
    }
}