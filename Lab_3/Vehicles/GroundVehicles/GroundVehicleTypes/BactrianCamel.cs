namespace Lab_3
{
    public class BactrianCamel : GroundVehicle
    {
        public BactrianCamel() : base(10, 30)
        {}

        public override double GetRestDuration => NumberOfRest == 1 ? 5 : 8;
    }
}