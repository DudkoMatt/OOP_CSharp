namespace Lab_3
{
    public class AirRace : Race
    {
        public AirRace(double distance) : base(distance)
        {}
        
        public ulong RegisterVehicle(AirVehicle vehicle)
        {
            return base.RegisterVehicle(vehicle);
        }
        
        public override bool CheckVehicleType(Vehicle vehicle)
        {
            return vehicle is AirVehicle;
        }
    }
}