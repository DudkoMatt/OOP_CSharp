namespace Lab_5
{
    public abstract class ClientBuilder
    {
        public abstract void Reset();
        public abstract void SetName();
        public abstract void SetSurname();
        public abstract void SetAddress();
        public abstract void SetPassportNumber();
        public abstract Client GetResult();
    }
}