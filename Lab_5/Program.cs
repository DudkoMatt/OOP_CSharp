using System;

namespace Lab_5
{
    public class Program
    {
        public static void Main()
        {
            var bankManager = new BankManager();
            bankManager.RegisterBank(new Bank());  // bankId: 0
            bankManager.RegisterBank(new Bank());  // bankId: 1

            // Паттерн: строитель
            // var clientDirector = new ClientDirector(new ConsoleClientBuilder());
            var clientDirector = new ClientDirector(new DefaultClientBuilder());
            
            bankManager.AddClient(0, clientDirector.Make());  // clientId: 0
            
            // clientDirector.ChangeBuilder(new DefaultClientBuilder());
            bankManager.AddClient(0, clientDirector.Make());  // clientId: 1

            clientDirector.SetAddress = false;
            clientDirector.SetPassport = false;
            bankManager.AddClient(0, clientDirector.Make());  // clientId: 2
            
            bankManager.AddClient(1, clientDirector.Make());  // clientId: 0

            var debitAccountCreator = new DebitAccountCreator(bankManager, 3.65);
            var creditAccountCreator = new CreditAccountCreator(bankManager, 100);
            var depositAccountCreator = new DepositAccountCreator(bankManager, new DateTime(2020, 12, 20));

            bankManager[0].ConcreteDepositAccountPercentStrategy = new ConcreteDepositAccountPercentStrategy();
            bankManager[1].ConcreteDepositAccountPercentStrategy = new ConcreteDepositAccountPercentStrategy();
            
            bankManager.AddAccountToClient(0, 0, debitAccountCreator);    // accountId: 0
            bankManager.AddAccountToClient(0, 1, creditAccountCreator);   // accountId: 1
            bankManager.AddAccountToClient(1, 0, depositAccountCreator);  // accountId: 0

            bankManager.Put(0, 0, 100);
            bankManager.Put(0, 1, 100);

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
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------");
            
            // --------------------------------------------------------------------------------------------------------

            Console.WriteLine();
            Console.WriteLine("Testing transfer: bankId: 0, accountId: 0 --> bankId: 1, accountId: 0");
            Console.WriteLine("BankId: 0, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[0].GetAccountById(0).Money);
            
            Console.WriteLine("BankId: 1, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[1].GetAccountById(0).Money);

            var transferId = bankManager.Transfer(0, 0, 50, 1, 0);
            
            Console.WriteLine();
            Console.WriteLine("After transfer:");
            Console.WriteLine("BankId: 0, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[0].GetAccountById(0).Money);
            
            Console.WriteLine("BankId: 1, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[1].GetAccountById(0).Money);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------");
            
            // --------------------------------------------------------------------------------------------------------
            
            Console.WriteLine();
            Console.WriteLine("Testing canceling transaction:");
            
            bankManager.CancelTransaction(0, transferId);
            
            Console.WriteLine("BankId: 0, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[0].GetAccountById(0).Money);
            Console.WriteLine("BankId: 1, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[1].GetAccountById(0).Money);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------");
            
            // --------------------------------------------------------------------------------------------------------
            
            Console.WriteLine();
            Console.WriteLine("Testing withdraw transaction:");
            
            Console.WriteLine("BankId: 0, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[0].GetAccountById(0).Money);
            
            bankManager.Withdraw(0, 0, 20);
            
            Console.WriteLine("BankId: 0, AccountId: 0");
            Console.Write("Current amount of money: ");
            Console.WriteLine(bankManager[0].GetAccountById(0).Money);
        }
    }
}