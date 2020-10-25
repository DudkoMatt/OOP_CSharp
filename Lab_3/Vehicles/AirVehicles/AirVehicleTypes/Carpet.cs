namespace Lab_3
{
    public class Carpet : AirVehicle
    {
        public Carpet() : base(10)
        {}

        public override double DistanceReducer(double distance)
        {
            if (distance < 1000)
                return 0;
            if (distance < 5000)
                return 3;
            if (distance < 10000)
                return 10;
            return 5;
        }
    }
}