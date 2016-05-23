using System;
using System.Drawing;
using System.Collections.Generic;

namespace A319TS
{
    [Serializable]
    public class Vehicle
    {
        ////////// PROPERTIES //////////
        public Node Home { get; set; }
        public Destination Destination { get; set; }
        public PointD Position { get; set; }
        public VehicleType Type { get; set; }
        public bool Active { get; set; }
        public List<PointD> ToDestRecord { get; set; }
        public List<PointD> ToHomeRecord { get; set; }
        
        private double _speed;
        public double Speed
        {
            get { return _speed; }
            private set
            {
                _speed = value;
                if (_speed == 0)
                    RemoveAllIncoming(_currentPath);
            }
        }
        
        ////////// MEMBERS //////////
        private SimulationSettings _settings;
        private List<Road> _currentPath;
        private int _currentPathIndex;
        private Road _currentRoad
        {
            get { return _currentPath[_currentPathIndex]; }
        }
        private Node CurrentNode
        {
            get
            {
                return _currentNode;
            }
            set
            {
                if(value == null && _currentNode != null)
                {
                    _currentNode.IsEmpty = true;
                    _currentNode = value;
                }
                else if(value != null && _currentNode != null)
                {
                    _currentNode.IsEmpty = false;
                    _currentNode = value;
                }
                else
                {
                    _currentNode = value;
                }
            }
        }

        private Node _currentNode;

        public int ToDestTime;
        private bool _toDestStarted;
        private List<Road> _toDestPath;

        public int ToHomeTime;
        private bool _toHomeStarted;
        private List<Road> _toHomePath;

        ////////// CONSTRUCTOR //////////
        public Vehicle(Project project, Node home, Destination dest, VehicleType type, int toDestTime, int toHomeTime)
        {
            _settings = project.Settings;
            ToDestRecord = new List<PointD>();
            ToHomeRecord = new List<PointD>();

            Home = home;
            Destination = dest;
            Type = type;

            if (ToDestTime > ToHomeTime)
                throw new ArgumentException("ToDestinationTime cannot be later than ToHomeTime");

            ToDestTime = toDestTime;
            ToHomeTime = toHomeTime;
            _toDestStarted = false;
            _toHomeStarted = false;
            
            Node end = FindEnd(project);
            _toDestPath = Pathfinder.FindPath(Home, end);
            _toHomePath = Pathfinder.FindPath(end, Home);

            Active = false;
            Speed = 0;
        }
        private Node FindEnd(Project project)
        {
            if (Destination == null) return FindOutbound(project);
            else return FindParking(project);
        }
        private Node FindOutbound(Project project)
        {
            Random random = new Random();
            List<Node> OutboundNodes = project.Nodes.FindAll(n => n.Type == NodeTypes.Outbound);
            if (OutboundNodes.Count == 0)
                throw new Exception("No Outbound Nodes");
            return OutboundNodes[random.Next(OutboundNodes.Count - 1)];
        }
        private Node FindParking(Project project)
        {
            List<Node> parkingNodes = project.Nodes.FindAll(n => n.Type == NodeTypes.Parking);
            double closestDistance = double.MaxValue;
            int closestIndex = 0;
            for (int i = 0; i < parkingNodes.Count; i++)
            {
                double distance = MathExtension.Distance(parkingNodes[i].Position, Destination.Position);
                if (distance < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = distance;
                }
            }
            return parkingNodes[closestIndex];
        }

        ////////// DRIVE //////////
        public void Drive(int time)
        {
            if (!Active)
            {
                CheckActive(time);
            }
            else
            {
                Speed = GetSpeed();
                if (Speed != 0)
                    Move(MathExtension.KmhToMms(Speed) * _settings.StepSize);
                if (time % Simulation.RecordInterval == 0 && !_toHomeStarted)
                    ToDestRecord.Add(new PointD(Position));
                else if (time % Simulation.RecordInterval == 0 && _toHomeStarted)
                    ToHomeRecord.Add(new PointD(Position));
            }
        }

        ////////// ACTIVATION //////////
        private void CheckActive(int time)
        {
            if (time > ToDestTime && !_toDestStarted)
            {
                if (true) // _toDestPath[0].From.IsEmpty
                {
                    Activate(_toDestPath);
                    _toDestStarted = true;
                }
            } 
            else if(time > ToHomeTime && !_toHomeStarted)
            {
                if (true) // _toHomePath[0].From.IsEmpty
                {
                    Activate(_toHomePath);
                    _toHomeStarted = true;
                }
            }
        }
        private void Activate(List<Road> path)
        {
            Active = true;
            _currentPath = path;
            _currentPathIndex = 0;
            Position = new PointD(_currentRoad.From.Position);
            _currentRoad.Vehicles.Add(this);
        }
        private void Deactivate()
        {
            _currentRoad.Vehicles.Remove(this);
            Speed = 0;
            Active = false;
        }

        ////////// SPEED //////////
        private double GetSpeed()
        {
            int incomingVehiclesCount = _currentRoad.To.IncomingVehicles.Count;

            Vehicle vehicleInfront = VehicleInfront(_settings.VehicleSpace);
            if (CurrentNode != null && CurrentNode.Type == NodeTypes.Light && !CurrentNode.Green) // If at red light;
            {
                return 0;
            }
            else if (CurrentNode != null && CurrentNode.Type == NodeTypes.Yield && incomingVehiclesCount > 0)
            {
                return 0;
            }
            else if (vehicleInfront != null)
            {
                if (Type.MaxSpeed > vehicleInfront.Speed)
                    return vehicleInfront.Speed * (_settings.TrailingSpeed / 100);
                else
                    return Type.MaxSpeed;
            }
            else
            {
                if (Type.MaxSpeed > _currentRoad.Type.Speed)
                    return _currentRoad.Type.Speed;
                else
                    return Type.MaxSpeed;
            }
        }

        ////////// VEHICLEINFRONT //////////
        private Vehicle VehicleInfront(double distanceToCheck)
        {
            Vehicle vehicleInfront = null;
            double distanceChecked = MathExtension.Distance(Position, new PointD(_currentRoad.To.Position));
            int index = _currentPathIndex;
            int currentPathCount = _currentPath.Count;

            do
            {
                if (index == _currentPathIndex)
                    vehicleInfront = CheckCurrentRoad(distanceToCheck);
                else
                {
                    vehicleInfront = CheckFutureRoad(index, distanceToCheck - distanceChecked);
                    distanceChecked += _currentPath[index].Length;
                }

                if (vehicleInfront != null) break;

                if (index + 1 < currentPathCount) index++;
                else break;
            }
            while (distanceChecked < distanceToCheck);
            return vehicleInfront;
        }
        private Vehicle CheckCurrentRoad(double distanceToCheck)
        {
            List<Vehicle> vehiclesInfront = new List<Vehicle>();
            PointD RoadEndPosition = new PointD(_currentRoad.To.Position);
            double distanceToEnd = MathExtension.Distance(Position, RoadEndPosition);

            foreach (Vehicle vehicle in _currentRoad.Vehicles)
                if (MathExtension.Distance(vehicle.Position, RoadEndPosition) < distanceToEnd // is infront
                    && MathExtension.Distance(Position, vehicle.Position) < distanceToCheck) // is within distance to check
                    vehiclesInfront.Add(vehicle);

            if (vehiclesInfront.Count == 0)
                return null;

            return GetClosestToStart(vehiclesInfront);
        }
        private Vehicle CheckFutureRoad(int index, double distanceToCheck)
        {
            List<Vehicle> vehiclesInfront = new List<Vehicle>();
            PointD RoadStartPosition = new PointD(_currentPath[index].From.Position);

            foreach (Vehicle vehicle in _currentPath[index].Vehicles)
                if (MathExtension.Distance(vehicle.Position, RoadStartPosition) < distanceToCheck) // is within distance to check
                    vehiclesInfront.Add(vehicle);

            if (vehiclesInfront.Count == 0)
                return null;

            return GetClosestToStart(vehiclesInfront);
        }
        private Vehicle GetClosestToStart(List<Vehicle> vehicles)
        {
            int closestIndex = 0;
            double closestDistance = double.MaxValue;
            int vehicleCount = vehicles.Count;

            for (int i = 0; i < vehicleCount; i++)
            {
                double distance = MathExtension.Distance(vehicles[i].Position, new PointD(vehicles[i]._currentRoad.From.Position));
                if (distance < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = distance;
                }
            }
            return vehicles[closestIndex];
        }

        ////////// MOVE //////////
        private void Move(double distanceToMove)
        {
            CurrentNode = null;
            TranslateVehicle(distanceToMove);
            ControlOverreach();
            ShowAsIncoming();
        }
        private void TranslateVehicle(double distanceToMove)
        {
            Vector2D roadVector = Vector2D.FromRoad(_currentRoad);
            roadVector.ToUnit();
            roadVector.Scale(distanceToMove);
            Position = new PointD(Position.X + roadVector.X, Position.Y + roadVector.Y);
        }
        private void ControlOverreach()
        {
            double currentRoadStartDistance = MathExtension.Distance(Position, new PointD(_currentRoad.From.Position));
            if (currentRoadStartDistance > _currentRoad.Length)
            {
                if (_currentPathIndex + 1 == _currentPath.Count) // Check if end of path is reached
                {
                    Deactivate();
                }
                else if (_currentRoad.To.Type == NodeTypes.Light || _currentRoad.To.Type == NodeTypes.Yield)
                {
                    GoToNextRoad();
                }
                else
                {
                    double remainingDistanceToMove = currentRoadStartDistance - _currentRoad.Length;
                    GoToNextRoad();
                    Move(remainingDistanceToMove);
                }
            }
        }
        private void GoToNextRoad()
        {
            _currentRoad.Vehicles.Remove(this);
            _currentPathIndex++;
            _currentRoad.Vehicles.Add(this);
            Position = new PointD(_currentRoad.From.Position);
            CurrentNode = _currentRoad.From;
        }
        private void ShowAsIncoming()
        {
            RemoveAllIncoming(_currentPath);
            double distance = MathExtension.Distance(Position, new PointD(_currentRoad.To.Position));
            if (distance < _settings.IncommingRange)
            {
                SetIncoming(_currentRoad.To);
                for (int i = _currentPathIndex + 1; i < _currentPath.Count; i++)
                {
                    if (_currentPath[i].To.Type == NodeTypes.Yield)
                        break;
                    distance += _currentPath[i].Length;
                    if (distance < _settings.IncommingRange)
                        SetIncoming(_currentPath[i].To);
                    else break;
                }
            }
        }
        private void RemoveAllIncoming(List<Road> path)
        {
            if (path == null) return;

            foreach (Road road in path)
            {
                if (road.From.IncomingVehicles.Contains(this))
                    road.From.IncomingVehicles.Remove(this);
                if (road.To.IncomingVehicles.Contains(this))
                    road.To.IncomingVehicles.Remove(this);
            }
        }
        private void SetIncoming(Node node)
        {
            if (!node.IncomingVehicles.Contains(this))
                node.IncomingVehicles.Add(this);
        }

        ////////// EXTRACT DATA //////////
        public VehicleData ExtractData()
        {
            return new VehicleData(Type, ToDestRecord, ToHomeRecord, ToDestTime, ToHomeTime);
        }
    }
}