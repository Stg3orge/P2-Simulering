using System;
using System.Drawing;
using System.Collections.Generic;

namespace A319TS
{
    public class Vehicle
    {
        public Node Home;
        public Destination Destination;
        public PointF Position { get; set; }
        public VehicleType Type;
        public bool Active;
        public Node PassedNode;

        public double Speed;
        public List<Road> CurrentPath;
        public int CurrentPathIndex;
        public Road CurrentRoad
        {
            get { return CurrentPath[CurrentPathIndex]; }
        }

        public int ToDestTime;
        public bool ToDestStarted;
        public List<Road> ToDestPath;

        public int ToHomeTime;
        public bool ToHomeStarted;
        public List<Road> ToHomePath;
        
        public Vehicle(Node home, Destination dest, VehicleType type, int toDestTime, int toHomeTime)
        {
            Home = home;
            Destination = dest;
            Type = type;

            if (ToDestTime > ToHomeTime)
                throw new ArgumentException("To Destination Time cannot be later than To Home Time");

            ToDestTime = toDestTime;
            ToHomeTime = toHomeTime;
            Active = false;
            Speed = 0;
        }

        public PointF Drive(int stepSize, int time)
        {
            if (!Active)
            {
                CheckActive(time);
                return new PointF(-1, -1);
            }
            else
            {
                SetSpeed();
                Move(stepSize);
                return Position;
            }
        }

        private void CheckActive(int time)
        {
            if (time > ToDestTime && !ToDestStarted)
                Activate(ToDestPath);
            else if(time > ToHomeTime && !ToHomeStarted)
                Activate(ToHomePath);
        }
        private void Activate(List<Road> path)
        {
            Active = true;
            CurrentPath = path;
            CurrentPathIndex = 0;
            Position = CurrentRoad.From.Position;
            CurrentRoad.Vehicles.Add(this);
        }

        private void SetSpeed()
        {
            Vehicle vehicleInfront = VehicleInfront(10);
            if (PassedNode != null && PassedNode.Type == NodeTypes.Light && !PassedNode.Green) // If at red light;
            {
                Speed = 0;
            }
            else if (PassedNode != null && PassedNode.Type == NodeTypes.Yield)
            {
                // Benja, do stuff! :)
            }
            else if (vehicleInfront != null)
            {
                Speed = vehicleInfront.Speed;
            }
            else
            {
                if (Type.MaxSpeed > CurrentRoad.Type.Speed)
                    Speed = CurrentRoad.Type.Speed;
                else
                    Speed = Type.MaxSpeed;
            }
        }
        private Vehicle VehicleInfront(double distanceToCheck)
        {
            return null; // Find the closest vehicle in front of this vehicle (Within the given distance) on the same road, or multiple roads if distance is long enough.
        }

        // Problem: If vehicle moves past node, then additional distance isnt applied to the next road.
        private void Move(int stepSize)
        {
            PassedNode = null;

            double distanceToMove = MathExtension.KmhToMetersPerMs(Speed) * stepSize;
            Vector2D roadVector = Vector2D.FromRoad(CurrentRoad);
            roadVector.ToUnit();
            roadVector.Scale(distanceToMove);
            PointF nextPosition = new PointF(Convert.ToSingle(Position.X + roadVector.X), 
                                             Convert.ToSingle(Position.Y + roadVector.Y));
            Position = nextPosition;

            // Check if vehicle has driven past the end of the road.
            if (MathExtension.Distance(Position, CurrentRoad.From.Position) > CurrentRoad.Length)
            {
                if (CurrentPathIndex + 1 > CurrentPath.Count) // Check if end is reached
                {
                    Active = false;
                }
                else
                {
                    CurrentRoad.Vehicles.Remove(this);
                    CurrentPathIndex++;
                    CurrentRoad.Vehicles.Add(this);
                    Position = CurrentRoad.From.Position;
                    PassedNode = CurrentRoad.From;
                }
            }
        }
    }
}