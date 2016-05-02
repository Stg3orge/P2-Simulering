using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    public enum Partitions { Shared, Primary, Secondary };
    class Simulation
    {
        // Should Probably Be Settings
        private const int CarCount = 1000;
        private const int Inbound = 100;
        private const int Outbound = 200;
        // Should Probably Be Settings
        private const int MsInDay = 86400000;

        private Project Project;
        private int _progress;
        private int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnProgressChanged(value);
            }
        }

        public EventHandler<ProgressEventArgs> ProgressChanged;
        protected virtual void OnProgressChanged(int progress)
        {
            ProgressChanged?.Invoke(this, new ProgressEventArgs() { Progress = progress });
        }
        public EventHandler<MessageEventArgs> ProcessStart;
        protected virtual void OnProcessStart(string process)
        {
            ProcessStart?.Invoke(this, new MessageEventArgs() { Message = process });
        }
        public EventHandler<MessageEventArgs> OperationCompleted;
        protected virtual void OnOperationCompleted(string operation)
        {
            OperationCompleted?.Invoke(this, new MessageEventArgs() { Message = operation });
        }

        public Simulation(Project project)
        {
            Project = project;
        }
        
        public void Run()
        {
            OnProcessStart("Creating Vehicles");
            Tuple<List<Vehicle>, List<Vehicle>> vehicles = CreateVehicles();
            List<Vehicle> primaryVehicles = vehicles.Item1;
            List<Vehicle> secondaryVehicles = vehicles.Item2;

            OnProcessStart("Running Primary Simulation");
            PointF[,] primaryRecord = Simulate(primaryVehicles);
            OnProcessStart("Running Secondary Simulation");
            PointF[,] secondaryRecord = Simulate(secondaryVehicles);

            OnProcessStart("Saving Simulation Data");
            Project.Simulations.Add(new SimulationData(Project.Clone() as Project,
                primaryVehicles, secondaryVehicles, primaryRecord, secondaryRecord));
        }
        private PointF[,] Simulate(List<Vehicle> vehicles)
        {
            PointF[,] record = new PointF[vehicles.Count, MsInDay / Project.Settings.StepSize];
            for (int i = 0; i < MsInDay; i += Project.Settings.StepSize)
                for (int j = 0; j < vehicles.Count; j++)
                    record[j, i / Project.Settings.StepSize] = vehicles[j].Drive(Project.Settings.StepSize);
            return record;
        }

        private Tuple<List<Vehicle>, List<Vehicle>> CreateVehicles()
        {
            List<Vehicle> primaryVehicles = new List<Vehicle>();
            List<Vehicle> secondaryVehicles = new List<Vehicle>();
            
            int[] primaryDestDistribution = CalculateDestinationDistribution(Partitions.Primary);
            int[] secondaryDestDistribution = CalculateDestinationDistribution(Partitions.Secondary);
            int[] primaryVehicleDistribution = CalculateVehicleDistribution(Partitions.Primary);
            int[] secondaryVehicleDistribution = CalculateVehicleDistribution(Partitions.Secondary);
            
            for (int i = 0; i < CarCount; i++)
            {
                // Vehicles.Add(new Vehicle());
            }

            return new Tuple<List<Vehicle>, List<Vehicle>>(primaryVehicles, secondaryVehicles);
        }
        private int[] CalculateDestinationDistribution(Partitions partition)
        {
            int[] destDistribution = new int[Project.DestinationTypes.Count];
            destDistribution[0] = Outbound;
            for (int i = 1; i < Project.DestinationTypes.Count; i++)
            {
                double amount = (Project.DestinationTypes[i - 1].Distribution / 100) * CarCount - Outbound;
                destDistribution[i] = Convert.ToInt32(amount) + destDistribution[i - 1];
            }
            return destDistribution;
        }
        private int[] CalculateVehicleDistribution(Partitions partition)
        {
            int[] vehicleDistribution = new int[Project.VehicleTypes.Count];



            return vehicleDistribution;
        }
    }
}
