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
            // var clientDirector = new ClientDirector(new ConsoleClientBuilder());
            var clientDirector = new ClientDirector(new DefaultClientBuilder());
            
            bankManager[0].AddClient(clientDirector.Make());  // clientId: 0
            
            // clientDirector.ChangeBuilder(new DefaultClientBuilder());
            bankManager[0].AddClient(clientDirector.Make());  // clientId: 1

            clientDirector.SetAddress = false;
            clientDirector.SetPassport = false;
            bankManager[0].AddClient(clientDirector.Make());  // clientId: 2
            
            bankManager[1].AddClient(clientDirector.Make());  // clientId: 0

            var debitAccountCreator = new DebitAccountCreator(bankManager, 3.65);
            var creditAccountCreator = new CreditAccountCreator(bankManager, 100);
            var depositAccountCreator = new DepositAccountCreator(bankManager, new DateTime(2020, 12, 20));

            bankManager[0].ConcreteDepositAccountPercentStrategy = new ConcreteDepositAccountPercentStrategy();
            bankManager[1].ConcreteDepositAccountPercentStrategy = new ConcreteDepositAccountPercentStrategy();
            
            bankManager[0].AddAccountToClient(0, debitAccountCreator);    // accountId: 0
            bankManager[0].AddAccountToClient(1, creditAccountCreator);   // accountId: 1
            bankManager[1].AddAccountToClient(0, depositAccountCreator);  // accountId: 0

            bankManager[0].Put(0, 100);

            // ------------------------------------- Testing ----------------------------------------------------------
            
            Console.WriteLine();
            Console.WriteLine("Testing fast forwarding:");
            Console.WriteLine("AccountId: 0, BankId: 0");
            
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[0].GetAccountById(0).Money);

            FastForwardTime.FastForward(bankManager[0].GetAccountById(0), 30);
            
            Console.WriteLine("After 30 days: ");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[0].GetAccountById(0).Money);
            
            // --------------------------------------------------------------------------------------------------------

            Console.WriteLine();
            
        }
    }
}