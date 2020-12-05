using System.Collections.Generic;

namespace Lab_5
{
    public class Bank
    {
        private static ulong _nextBankId = 0;
        private ulong _nextClientId = 0;
        private ulong _nextAccountId = 0;
        private ulong _nextTransactionId = 0;
        
        public double DebitPercent { get; set; }
        public double SuspiciousAccountLimit { get; set; }
        
        public double TransferCommission { get; set; }
        public double WithdrawCommission { get; set; }

        public readonly ulong Id;
        
        private Dictionary<ulong, Account> Accounts { get; }
        private Dictionary<ulong, Client> Clients { get; }
        
        private Dictionary<ulong, Transaction> Transactions { get; }

        public Bank()
        {
            Id = _nextBankId++;
            Accounts = new Dictionary<ulong, Account>();
            Clients = new Dictionary<ulong, Client>();
            Transactions = new Dictionary<ulong, Transaction>();
        }

        public ulong AddClient(Client client)
        {
            if (Clients.ContainsValue(client)) throw new ClientAlreadyRegisteredException();
            
            Clients.Add(_nextClientId, client);
            return _nextClientId++;
        }

        public void RemoveClient(ulong clientId)
        {
            if (!Clients.ContainsKey(clientId)) throw new ClientIdNotFoundException();
            
            foreach (var (accountId, _) in Clients[clientId].Accounts)
            {
                Accounts.Remove(accountId);
            }
            
            Clients.Remove(clientId);
        }

        public ulong AddAccountToClient(ulong clientId, AccountCreator accountCreator)
        {
            var account = accountCreator.CreateAccount(Id, clientId);
            if (Accounts.ContainsValue(account)) throw new AccountAlreadyRegisteredException();
            Accounts.Add(_nextAccountId, account);
            Clients[clientId].AddAccount(_nextAccountId, account);

            return _nextAccountId++;
        }

        public void RemoveAccount(ulong accountId)
        {
            if (!Accounts.ContainsKey(accountId)) throw new AccountIdNotFoundException();
            Accounts[accountId].Client.Accounts.Remove(accountId);
            Accounts.Remove(accountId);
        }

        public Account GetAccountById(ulong accountId) => Accounts[accountId];
        public Client GetClientById(ulong clientId) => Clients[clientId];

        public ulong Withdraw(ulong accountId, double money)
        {
            Transactions.Add(_nextTransactionId, new WithdrawTransaction(DateTimeProvider.Now, Accounts[accountId], money));
            return _nextTransactionId++;
        }
        
        public ulong Put(ulong accountId, double money)
        {
            Transactions.Add(_nextTransactionId, new PutTransaction(DateTimeProvider.Now, Accounts[accountId], money));
            return _nextTransactionId++;
        }
        
        public ulong Transfer(ulong accountId, double money, ulong accountTo)
        {
            Transactions.Add(_nextTransactionId, new TransferTransaction(DateTimeProvider.Now, Accounts[accountId], money, Accounts[accountTo]));
            return _nextTransactionId++;
        }

        public void CancelTransaction(ulong transactionId)
        {
            Transactions[transactionId].Cancel();
        }
    }
}