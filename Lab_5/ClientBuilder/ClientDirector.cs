namespace Lab_5
{
    public class ClientDirector
    {
        private ClientBuilder _builder;
        public bool SetAddress = true;
        public bool SetPassport = true;

        public ClientDirector(ClientBuilder builder)
        {
            _builder = builder;
        }

        public void ChangeBuilder(ClientBuilder builder) => _builder = builder;

        public Client Make()
        {
            _builder.Reset();
            _builder.SetName();
            _builder.SetSurname();
            if (SetAddress)
                _builder.SetAddress();
            if (SetPassport)
                _builder.SetPassportNumber();

            return _builder.GetResult();
        }
    }
}