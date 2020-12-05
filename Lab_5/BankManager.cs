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
    }
}