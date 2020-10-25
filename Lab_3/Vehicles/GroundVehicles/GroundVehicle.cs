namespace Lab_3
{
    public abstract class GroundVehicle : Vehicle
    {
        public abstract double GetRestDuration { get; }
        public void TakeRest() => NumberOfRest++;
        public void ResetNumberOfRest() => NumberOfRest = 1;
        public double RestInterval { get; }

        public int NumberOfRest { get; protected set; } = 1;

        protected GroundVehicle(double speed, double restInterval) : base(speed)
        {
            RestInterval = restInterval;
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
                    calculatedTime += GetRestDuration;
                    TakeRest();
                    distanceRemaining -= maxDistance;
                }
            }

            ResetNumberOfRest();
            return calculatedTime;
        }
    }
}