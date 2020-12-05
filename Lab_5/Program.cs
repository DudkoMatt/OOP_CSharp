using System;

namespace Lab_5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bankManager = new BankManager();
            bankManager.RegisterBank(new Bank());  // bankId: 0
            bankManager.RegisterBank(new Bank());  // bankId: 1

            // Паттерн: строитель
            var clientDirector = new ClientDirector(new ConsoleClientBuilder());
            
            bankManager[0].AddClient(clientDirector.Make());  // clientId: 0
            bankManager[0].AddClient(clientDirector.Make());  // clientId: 1
            bankManager[1].AddClient(clientDirector.Make());  // clientId: 0

            var creditAccountCreator = new CreditAccountCreator(bankManager, 100);
            var debitAccountCreator = new DebitAccountCreator(bankManager);
            var depositAccountCreator = new DepositAccountCreator(bankManager, new DateTime(2020, 12, 20));

            bankManager[0].AddAccountToClient(0, debitAccountCreator);
            bankManager[0].AddAccountToClient(1, creditAccountCreator);
            bankManager[1].AddAccountToClient(0, depositAccountCreator);
        }
    }
}