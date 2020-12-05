using System;

namespace Lab_5
{
    public class ConsoleClientBuilder : ClientBuilder
    {
        private Client _client;

        public override void Reset()
        {
            _client = new Client(null, null);
            Console.WriteLine("Создание нового пользователя:");
        }

        public override void SetName()
        {
            Console.Write("Введите имя: ");
            _client.UpdateName(Console.ReadLine());
        }

        public override void SetSurname()
        {
            Console.Write("Введите фамилию: ");
            _client.UpdateSurname(Console.ReadLine());
        }

        public override void SetAddress()
        {
            Console.Write("Введите адрес: ");
            _client.UpdateAddress(Console.ReadLine());
        }

        public override void SetPassportNumber()
        {
            Console.Write("Введите номер паспорта: ");
            _client.UpdatePassport(Console.ReadLine());
        }

        public override Client GetResult()
        {
            return _client;
        }
    }
}