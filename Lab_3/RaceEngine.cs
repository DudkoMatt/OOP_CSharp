using System;
using System.Collections.Generic;

namespace Lab_3
{
    public class RaceEngine
    {
        private Race _race;
        private ulong _nextVehicleId = 1000;
        private Dictionary<ulong, Vehicle> _vehicles;

        public RaceEngine(Race race)
        {
            _race = race;
            _vehicles = new Dictionary<ulong, Vehicle>();
        }

        public ulong RegisterVehicle(Vehicle vehicle)
        {
            // Checking type of race and type of vehicle
            if (_race is GroundRace)
            {
                if (!(vehicle is GroundVehicle))
                    throw new ArgumentException("Cannot add not GroundVehicle type of vehicle to Ground race");
            }
            else if (_race is AirRace)
            {
                if (!(vehicle is AirVehicle))
                    throw new ArgumentException("Cannot add not AirVehicle type of vehicle to Air race");
            }
            else if (!(_race is GeneralRace))
                throw new Exception("Unsupported type of race");
            
            _vehicles.Add(_nextVehicleId, vehicle);
            return _nextVehicleId++;
        }

        private double _CalculateTime(Vehicle vehicle)
        {
            double calculatedTime = 0;
            var distanceRemaining = _race.Distance;
            switch (vehicle)
            {
                case GroundVehicle groundVehicle:
                    while (distanceRemaining > 0)
                    {
                        var maxDistance = groundVehicle.Speed * groundVehicle.RestInterval;
                        if (maxDistance > distanceRemaining)
                        {
                            calculatedTime += distanceRemaining / groundVehicle.Speed;
                            distanceRemaining = 0;
                        }
                        else
                        {
                            calculatedTime += groundVehicle.RestInterval;
                            calculatedTime += groundVehicle.RestDuration;
                            distanceRemaining -= maxDistance;
                        }
                    }
                    break;
                case AirVehicle airVehicle:
                    distanceRemaining = distanceRemaining * (1 - airVehicle.DistanceReducer(distanceRemaining) / 100);
                    calculatedTime = distanceRemaining / airVehicle.Speed;
                    break;
            }

            return calculatedTime;
        }
        
        public ulong Run()
        {
            ulong winnerId = 0;
            double minTime = double.MaxValue;

            foreach (var (vehicleId, vehicle) in _vehicles)
            {
                var calculateTime = _CalculateTime(vehicle);
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

        protected Race(double distance)
        {
            Distance = distance;
        }
    }

    public class GroundRace : Race
    {
        public GroundRace(double distance) : base(distance)
        {}
    }
    public class AirRace : Race
    {
        public AirRace(double distance) : base(distance)
        {}
    }
    public class GeneralRace : Race
    {
        public GeneralRace(double distance) : base(distance)
        {}
    }
}