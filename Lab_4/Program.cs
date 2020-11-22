using System;
using System.Threading;

namespace Lab_4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Кейс 1
            Console.WriteLine("Кейс 1");
            Console.WriteLine("------\n");
            
            var restoreManager1 = new RestoreManager();
            restoreManager1.AddObjectForBackupList("a.txt");
            restoreManager1.AddObjectForBackupList("b.txt");
            
            restoreManager1.CreateFullBackup();
            Thread.Sleep(1000);
            
            restoreManager1.CreateFullBackup();
            restoreManager1.PrintHistory();

            Console.WriteLine("------------------ Cleaning --------------------------------\n");
            
            restoreManager1.CleaningAlgorithm = new CountCleaningAlgorithm(1);
            Console.WriteLine($"Count of points after cleaning: {restoreManager1.CountLeftPoints()}\n");
            restoreManager1.Clean();
            
            restoreManager1.PrintHistory();
            
            // ---------------------------------------------------------------------------------------------------------
            
            // Кейс 2
            Console.WriteLine();
            Console.WriteLine("Кейс 2");
            Console.WriteLine("------\n");
            
            var restoreManager2 = new RestoreManager();
            restoreManager2.AddObjectForBackupList("a.txt");
            restoreManager2.AddObjectForBackupList("b.txt");
            
            restoreManager2.CreateFullBackup();
            Thread.Sleep(1000);
            
            restoreManager2.CreateFullBackup();
            restoreManager2.PrintHistory();

            Console.WriteLine("------------------ Cleaning --------------------------------\n");
            
            restoreManager2.CleaningAlgorithm = new SizeCleaningAlgorithm(35);
            Console.WriteLine($"Count of points after cleaning: {restoreManager2.CountLeftPoints()}\n");
            restoreManager2.Clean();
            
            restoreManager2.PrintHistory();
            
            // ---------------------------------------------------------------------------------------------------------
            
            // Кейс 3
            Console.WriteLine();
            Console.WriteLine("Кейс 3");
            Console.WriteLine("------\n");
            
            var restoreManager3 = new RestoreManager();
            restoreManager3.AddObjectForBackupList("c.txt");
            restoreManager3.AddObjectForBackupList("d.txt");
            
            restoreManager3.CreateFullBackup();

            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine("Change files and press enter...");
                Console.ReadLine();
                restoreManager3.CreateIncrementalBackup();
            }
            
            // restoreManager3.CreateFullBackup();
            
            restoreManager3.PrintHistory();
            
            Console.WriteLine("------------------ Cleaning --------------------------------\n");
            
            restoreManager3.CleaningAlgorithm = new CountCleaningAlgorithm(1);
            Console.WriteLine($"Count of points after cleaning: {restoreManager3.CountLeftPoints()}\n");
            restoreManager3.Clean();
            
            restoreManager3.PrintHistory();

        }
    }
}