namespace Lab_3
{
    public class Broom : AirVehicle
    {
        public Broom() : base(20)
        {}

        public override double DistanceReducer(double distance)
        {
            return distance / 1000;
        }
    }
}