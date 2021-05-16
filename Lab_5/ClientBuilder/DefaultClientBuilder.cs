using System;

namespace Lab_5
{
    public class DefaultClientBuilder : ClientBuilder
    {
        private Client _client;

        public override void Reset()
        {
            _client = new Client(null, null);
            Console.WriteLine("Создание нового пользователя: [default values]");
        }

        public override void SetName()
        {
            Console.WriteLine("Имя: Name");
            _client.UpdateName("Name");
        }

        public override void SetSurname()
        {
            Console.WriteLine("Фамилия: Surname");
            _client.UpdateSurname("Surname");
        }

        public override void SetAddress()
        {
            Console.WriteLine("Адрес: Address");
            _client.UpdateAddress("Address");
        }

        public override void SetPassportNumber()
        {
            Console.WriteLine("Номер паспорта: 1234 123456");
            _client.UpdatePassport("1234 123456");
        }

        public override Client GetResult()
        {
            return _client;
        }
    }
}