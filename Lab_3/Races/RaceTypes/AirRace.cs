namespace Lab_3
{
    public class AirRace : Race
    {
        public AirRace(double distance) : base(distance)
        {}
        
        public override bool CheckVehicleType(Vehicle vehicle)
        {
            return vehicle is AirVehicle;
        }
    }
}