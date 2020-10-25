namespace Lab_3
{
    public class GroundRace : Race
    {
        public GroundRace(double distance) : base(distance)
        {}

        public override bool CheckVehicleType(Vehicle vehicle)
        {
            return vehicle is GroundVehicle;
        }
    }
}