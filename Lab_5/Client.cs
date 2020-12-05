namespace Lab_5
{
    public class Client
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Address { get; private set; }
        public string PassportNumber { get; private set; }

        public Client(string name, string surname, string address = null, string passportNumber = null)
        {
            Name = name;
            Surname = surname;
            Address = address;
            PassportNumber = passportNumber;
        }

        public void UpdateName(string name) => Name = name;
        public void UpdateSurname(string surname) => Surname = surname;
        public void UpdateAddress(string address) => Address = address;
        public void UpdatePassport(string passportNumber) => PassportNumber = passportNumber;
    }
}