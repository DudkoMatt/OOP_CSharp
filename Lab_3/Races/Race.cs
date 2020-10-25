namespace Lab_3
{
    public abstract class Race
    {
        public readonly double Distance;
        protected RaceEngine RaceEngine;

        protected Race(double distance)
        {
            Distance = distance;
            RaceEngine = new RaceEngine();
        }

        public abstract bool CheckVehicleType(Vehicle vehicle);

        protected ulong RegisterVehicle(Vehicle vehicle)
        {
            if (!CheckVehicleType(vehicle)) throw new VehicleTypeInvalidException();
            return RaceEngine.RegisterVehicle(vehicle);
        }

        public ulong Run()
        {
            return RaceEngine.Run(Distance);
        }
    }
}