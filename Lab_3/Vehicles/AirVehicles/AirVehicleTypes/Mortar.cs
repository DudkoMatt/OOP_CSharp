namespace Lab_3
{
    public class Mortar : AirVehicle
    {
        public Mortar() : base(8)
        {}

        public override double DistanceReducer(double distance) => 6;
    }
}