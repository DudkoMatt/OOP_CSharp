using System.Collections.Generic;

namespace Lab_5
{
    public class Client
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Address { get; private set; }
        public string PassportNumber { get; private set; }

        public Dictionary<ulong, Account> Accounts { get; }

        public Client(string name, string surname, string address = null, string passportNumber = null)
        {
            Name = name;
            Surname = surname;
            Address = address;
            PassportNumber = passportNumber;
            Accounts = new Dictionary<ulong, Account>();
        }

        public void UpdateName(string name) => Name = name;
        public void UpdateSurname(string surname) => Surname = surname;
        public void UpdateAddress(string address) => Address = address;
        public void UpdatePassport(string passportNumber) => PassportNumber = passportNumber;

        public bool CheckProfile()
        {
            return !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(PassportNumber);
        }
        
        public void AddAccount(ulong accountId, Account account)
        {
            if (!Accounts.ContainsKey(accountId))
                Accounts.Add(accountId, account);
        }
        
        public void RemoveAccount(ulong accountId) => Accounts.Remove(accountId);
    }
}