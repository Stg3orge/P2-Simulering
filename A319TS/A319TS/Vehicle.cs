using System;
using System.Drawing;
using System.Collections.Generic;

namespace A319TS
{
    public class Vehicle
    {
        ////////// PROPERTIES //////////
        public Node Home { get; set; }
        public Destination Destination { get; set; }
        public PointD Position { get; set; }
        public VehicleType Type { get; set; }
        public bool Active { get; set; }
        
        private double _speed;
        public double Speed
        {
            get { return _speed; }
            private set
            {
                _speed = value;
                if (value == 0)
                    foreach (Node node in _nodesIncommingAt)
                        node.IncomingVehicles.Remove(this);
                _nodesIncommingAt.Clear();
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
        private Node _currentNode;
        private List<Node> _nodesIncommingAt;

        private int _toDestTime;
        private bool _toDestStarted;
        private List<Road> _toDestPath;

        private int _toHomeTime;
        private bool _toHomeStarted;
        private List<Road> _toHomePath;

        ////////// CONSTRUCTOR //////////
        public Vehicle(Project project, Node home, Destination dest, VehicleType type, int toDestTime, int toHomeTime)
        {
            _settings = project.Settings;

            Home = home;
            Destination = dest;
            Type = type;

            if (_toDestTime > _toHomeTime)
                throw new ArgumentException("ToDestinationTime cannot be later than ToHomeTime");

            _toDestTime = toDestTime;
            _toHomeTime = toHomeTime;
            _toDestStarted = false;
            _toHomeStarted = false;

            Node parking = FindParking(project, Destination);
            _toDestPath = Pathfinder.FindPath(Home, parking);
            _toHomePath = Pathfinder.FindPath(parking, Home);

            Active = false;
            Speed = 0;
        }
        private Node FindParking(Project project, Destination dest)
        {
            int closestIndex = 0;
            double closestDistance = double.MaxValue;
            for (int i = 0; i < project.Nodes.Count; i++)
            {
                double distance = MathExtension.Distance(project.Nodes[i].Position, dest.Position);
                if (distance < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = distance;
                }
            }
            return project.Nodes[closestIndex];
        }

        ////////// DRIVE //////////
        public PointD Drive(int time)
        {
            if (!Active)
            {
                CheckActive(time);
                return new PointD(-1, -1);
            }
            else
            {
                Speed = GetSpeed();
                if (Speed != 0)
                    Move(MathExtension.KmhToMms(Speed) * _settings.StepSize);
                return Position;
            }
        }

        ////////// ACTIVATION //////////
        private void CheckActive(int time)
        {
            if (time > _toDestTime && !_toDestStarted)
            {
                Activate(_toDestPath);
                _toDestStarted = true;
            } 
            else if(time > _toHomeTime && !_toHomeStarted)
            {
                Activate(_toHomePath);
                _toHomeStarted = true;
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
            Vehicle vehicleInfront = VehicleInfront(_settings.VehicleSpace);
            if (_currentNode != null && _currentNode.Type == NodeTypes.Light && !_currentNode.Green) // If at red light;
            {
                return 0;
            }
            else if (_currentNode != null && _currentNode.Type == NodeTypes.Yield
                     && _currentRoad.To.IncomingVehicles.Count > 0) // If at yield and cars are passing
            {
                return 0;
            }
            else if (vehicleInfront != null)
            {
                return vehicleInfront.Speed;
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
            do
            {
                if (index == _currentPathIndex) vehicleInfront = CheckCurrentRoad(distanceToCheck);
                else
                {
                    vehicleInfront = CheckFutureRoad(index, distanceToCheck - distanceChecked);
                    distanceChecked += _currentPath[index].Length;
                }

                if (vehicleInfront != null) break;

                if (index + 1 < _currentPath.Count) index++;
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
            for (int i = 0; i < vehicles.Count; i++)
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
            _currentNode = null;
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
            if (_currentRoad.To.IncomingVehicles.Contains(this))
            {
                _currentRoad.To.IncomingVehicles.Remove(this);
                _nodesIncommingAt.Remove(_currentRoad.To);
            }

            _currentPathIndex++;
            _currentRoad.Vehicles.Add(this);
            Position = new PointD(_currentRoad.From.Position);
            _currentNode = _currentRoad.From;
        }
        private void ShowAsIncoming()
        {
            double distance = MathExtension.Distance(Position, new PointD(_currentRoad.To.Position));
            if (distance < _settings.IncommingRange)
                SetIncoming(_currentRoad.To);

            for (int i = _currentPathIndex + 1; i < _currentPath.Count; i++)
            {
                distance += _currentPath[i].Length;
                if (distance < _settings.IncommingRange)
                    SetIncoming(_currentPath[i].To);
                else break;
            }
        }
        private void SetIncoming(Node node)
        {
            if (!node.IncomingVehicles.Contains(this))
            {
                node.IncomingVehicles.Add(this);
                _nodesIncommingAt.Add(node);
            }
        }
    }
}