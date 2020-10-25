namespace Lab_3
{
    public abstract class AirVehicle : Vehicle
    {
        protected AirVehicle(double speed) : base(speed)
        {}

        public abstract double DistanceReducer(double distance);
        
        public override double CalculateTime(double distance)
        {
            return distance * (1 - DistanceReducer(distance) / 100) / Speed;
        }
    }
}