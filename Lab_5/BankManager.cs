using System.Collections.Generic;

namespace Lab_5
{
    public class BankManager
    {
        private Dictionary<ulong, Bank> _registeredBanks;

        public BankManager()
        {
            _registeredBanks = new Dictionary<ulong, Bank>();
        }

        public ulong RegisterBank(Bank bank)
        {
            if (_registeredBanks.ContainsValue(bank)) throw new BankAlreadyRegisteredException();
            _registeredBanks.Add(bank.Id, bank);
            
            return bank.Id;
        }

        public Bank GetBankById(ulong bankId) => _registeredBanks[bankId];
        public Bank this[ulong bankId] => _registeredBanks[bankId];
        public ulong Transfer(ulong bankId, ulong accountId, double money, ulong bankIdTo, ulong accountIdTo)
        {
            return _registeredBanks[bankId].Transfer(accountId, money, _registeredBanks[bankIdTo], accountIdTo);
        }
        
        public ulong Withdraw(ulong bankId, ulong accountId, double money)
        {
            return _registeredBanks[bankId].Withdraw(accountId, money);
        }
        
        public ulong Put(ulong bankId, ulong accountId, double money)
        {
            return _registeredBanks[bankId].Put(accountId, money);
        }
        
        public ulong AddClient(ulong bankId, Client client)
        {
            return _registeredBanks[bankId].AddClient(client);
        }
        
        public void RemoveClient(ulong bankId, ulong clientId)
        {
            _registeredBanks[bankId].RemoveClient(clientId);
        }

        public ulong AddAccountToClient(ulong bankId, ulong clientId, AccountCreator accountCreator)
        {
            return _registeredBanks[bankId].AddAccountToClient(clientId, accountCreator);
        }

        public void RemoveAccount(ulong bankId, ulong accountId)
        {
            _registeredBanks[bankId].RemoveAccount(accountId);
        }

        public void CancelTransaction(ulong bankId, ulong transactionId)
        {
            _registeredBanks[bankId].CancelTransaction(transactionId);
        }
    }
}