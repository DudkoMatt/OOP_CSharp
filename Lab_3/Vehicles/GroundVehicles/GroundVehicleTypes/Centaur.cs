namespace Lab_3
{
    public class Centaur : GroundVehicle
    {
        public Centaur() : base(15, 8)
        {}

        public override double GetRestDuration => 2;
    }
}