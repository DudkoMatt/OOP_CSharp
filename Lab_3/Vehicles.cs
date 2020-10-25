namespace Lab_3
{
    public abstract class Vehicle
    {
        protected double _speed;
        public double Speed => _speed;

        protected Vehicle(double speed)
        {
            _speed = speed;
        }

        public abstract double CalculateTime(double distance);
    }

    public abstract class GroundVehicle : Vehicle
    {
        protected double _restInterval;
        protected int _numberOfRest = 1;

        public abstract double RestDuration { get; }
        public double RestInterval => _restInterval;
        public int NumberOfRest => _numberOfRest;

        protected GroundVehicle(double speed, double restInterval) : base(speed)
        {
            _restInterval = restInterval;
        }
        
        public override double CalculateTime(double distance)
        {
            double calculatedTime = 0;
            var distanceRemaining = distance;

            while (distanceRemaining > 0)
            {
                var maxDistance = Speed * RestInterval;
                if (maxDistance > distanceRemaining)
                {
                    calculatedTime += distanceRemaining / Speed;
                    distanceRemaining = 0;
                }
                else
                {
                    calculatedTime += RestInterval;
                    calculatedTime += RestDuration;
                    distanceRemaining -= maxDistance;
                }
            }

            return calculatedTime;
        }
    }

    public class BactrianCamel : GroundVehicle
    {
        public BactrianCamel() : base(10, 30)
        {}

        public override double RestDuration => _numberOfRest++ == 1 ? 5 : 8;
    }

    public class FastCamel : GroundVehicle
    {
        public FastCamel() : base(40, 10)
        {}
        
        public override double RestDuration
        {
            get
            {
                return _numberOfRest++ switch
                {
                    1 => 5,
                    2 => 6.5,
                    _ => 8
                };
            }
        }
    }

    public class Centaur : GroundVehicle
    {
        public Centaur() : base(15, 8)
        {}

        public override double RestDuration => 2;
    }

    public class SpeedBoots : GroundVehicle
    {
        public SpeedBoots() : base(6, 60)
        {}

        public override double RestDuration => _numberOfRest++ == 1 ? 10 : 5;
    }

    public abstract class AirVehicle : Vehicle
    {
        protected AirVehicle(double speed) : base(speed)
        {}

        public abstract double DistanceReducer(double distance);
        
        public override double CalculateTime(double distance)
        {
            return distance * (1 - DistanceReducer(distance) / 100) / Speed;
        }
    }

    public class Carpet : AirVehicle
    {
        public Carpet() : base(10)
        {}

        public override double DistanceReducer(double distance)
        {
            if (distance < 1000)
                return 0;
            if (distance < 5000)
                return 3;
            if (distance < 10000)
                return 10;
            return 5;
        }
    }

    public class Mortar : AirVehicle
    {
        public Mortar() : base(8)
        {}

        public override double DistanceReducer(double distance) => 6;
    }

    public class Broom : AirVehicle
    {
        public Broom() : base(20)
        {}

        public override double DistanceReducer(double distance)
        {
            return distance / 1000;
        }
    }
}