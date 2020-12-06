namespace Lab_3
{
    public class FastCamel : GroundVehicle
    {
        public FastCamel() : base(40, 10)
        {}
        
        public override double GetRestDuration
        {
            get
            {
                return NumberOfRest switch
                {
                    1 => 5,
                    2 => 6.5,
                    _ => 8
                };
            }
        }
    }
}