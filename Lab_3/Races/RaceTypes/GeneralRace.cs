namespace Lab_3
{
    public class GeneralRace : Race
    {
        public GeneralRace(double distance) : base(distance)
        {}

        public new ulong RegisterVehicle(Vehicle vehicle)
        {
            return base.RegisterVehicle(vehicle);
        }
        
        public override bool CheckVehicleType(Vehicle vehicle)
        {
            return vehicle is GroundVehicle || vehicle is AirVehicle;
        }
    }
}