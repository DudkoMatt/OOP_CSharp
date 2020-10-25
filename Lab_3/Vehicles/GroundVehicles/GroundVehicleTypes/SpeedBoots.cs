namespace Lab_3
{
    public class SpeedBoots : GroundVehicle
    {
        public SpeedBoots() : base(6, 60)
        {}

        public override double GetRestDuration => NumberOfRest == 1 ? 10 : 5;
    }
}