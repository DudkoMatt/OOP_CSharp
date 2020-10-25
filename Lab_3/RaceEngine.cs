using System.Collections.Generic;

namespace Lab_3
{
    public class RaceEngine
    {
        private ulong _nextVehicleId = 1000;
        private Dictionary<ulong, Vehicle> _vehicles;

        public RaceEngine()
        {
            _vehicles = new Dictionary<ulong, Vehicle>();
        }

        public ulong RegisterVehicle(Vehicle vehicle)
        {
            _vehicles.Add(_nextVehicleId, vehicle);
            return _nextVehicleId++;
        }

        public ulong Run(double distance)
        {
            ulong winnerId = 0;
            var minTime = double.MaxValue;

            foreach (var (vehicleId, vehicle) in _vehicles)
            {
                var calculateTime = vehicle.CalculateTime(distance);
                if (calculateTime < minTime)
                {
                    winnerId = vehicleId;
                    minTime = calculateTime;
                }
            }

            return winnerId;
        }
    }

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

        public ulong RegisterVehicle(Vehicle vehicle)
        {
            if (!CheckVehicleType(vehicle)) throw new VehicleTypeInvalidException();
            return RaceEngine.RegisterVehicle(vehicle);
        }

        public ulong Run()
        {
            return RaceEngine.Run(Distance);
        }
    }

    public class GroundRace : Race
    {
        public GroundRace(double distance) : base(distance)
        {}

        public override bool CheckVehicleType(Vehicle vehicle)
        {
            return vehicle is GroundVehicle;
        }
    }
    public class AirRace : Race
    {
        public AirRace(double distance) : base(distance)
        {}
        
        public override bool CheckVehicleType(Vehicle vehicle)
        {
            return vehicle is AirVehicle;
        }
    }
    public class GeneralRace : Race
    {
        public GeneralRace(double distance) : base(distance)
        {}

        public override bool CheckVehicleType(Vehicle vehicle)
        {
            return vehicle is GroundVehicle || vehicle is AirVehicle;
        }
    }
}