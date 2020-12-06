namespace Lab_3
{
    public abstract class Vehicle
    {
        public double Speed { get; }

        protected Vehicle(double speed)
        {
            Speed = speed;
        }

        public abstract double CalculateTime(double distance);
    }
}