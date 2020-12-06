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
            if (_vehicles.Count == 0) throw new ZeroRegisteredVehiclesException();
            
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
}