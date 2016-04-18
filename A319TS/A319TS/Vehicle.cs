namespace A319TS
{
    class Vehicle
    {
        public Vehicle(int _acceleration, int _deceleration, string _name, int _maxSpeed, int _x, int _y, bool _start, bool _end, int _currentX, int _currentY, int _id)
        {
            acceleration = _acceleration;
            deceleration = _deceleration;
            name = _name;
            maxSpeed = _maxSpeed;
            X = _x;
            Y = _y;
            start = _start;
            end = _end;
            currentX = _currentX;
            currentY = _currentY;
            ID = _id;
        }
        public int acceleration { get; set; }
        public int deceleration { get; set; }
        public string name { get; set; }
        public int maxSpeed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool start { get; set; }
        public bool end { get; set; }
        public int currentX { get; set; }
        public int currentY { get; set; }
        public int ID { get; set; }
    }
}
