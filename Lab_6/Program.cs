using System;
using System.IO;
using System.Linq;
using Lab_6.UI;
using Lab_6.UI.ViewModels;
using Lab_6_BLL.Services;
using Lab_6_DAL.Repositories;

namespace Lab_6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Настройка репозиториев
            const string reportFileRepositoryPath = "Reports/";
            const string staffFileRepositoryPath = "Staff/";
            const string taskFileRepositoryPath = "Tasks/";

            // Очистка предыдущих запусков
            DeleteDirectory(reportFileRepositoryPath);
            DeleteDirectory(staffFileRepositoryPath);
            DeleteDirectory(taskFileRepositoryPath);
            
            // Создание репозиториев
            var taskFileRepository = new TaskFileRepository(taskFileRepositoryPath);
            var staffFileRepository = new StaffFileRepository(staffFileRepositoryPath);
            var reportFileRepository = new ReportFileRepository(reportFileRepositoryPath);
            
            // Создание сервисов
            var staffService = new StaffService(staffFileRepository);
            var taskService = new TasksService(taskFileRepository);
            var reportService = new ReportService(reportFileRepository, staffService, taskService);

            // Создание UI
            var userInterfaceController = new UIController(reportService, staffService, taskService);
            
            // Создание работников
            var staffId = userInterfaceController.CreateStaff("someName");
            var staff = userInterfaceController.GetAllStaff().First(t => t.Id == staffId);

            var staffId2 = userInterfaceController.CreateStaff("Name 2");
            var staff2 = userInterfaceController.GetAllStaff().First(t => t.Id == staffId2);

            // Создание отчетов
            var reportId = userInterfaceController.CreateDailyReport(staff);
            var report = userInterfaceController.GetAllReports().First(r => r.Id == reportId);
            
            var reportId2 = userInterfaceController.CreateDailyReport(staff);
            var report2 = userInterfaceController.GetAllReports().First(r => r.Id == reportId2);
            
            // Создание задач
            var taskId = userInterfaceController.CreateTask("Task #1", "Test task", staff);
            var taskId2 = userInterfaceController.CreateTask("Task #2", "Test task 2", staff2);
            var taskId3 = userInterfaceController.CreateTask("Task #3", "Test task 3", staff2);

            var task = userInterfaceController.GetAllTasks().First(t => t.Id == taskId);
            var task2 = userInterfaceController.GetAllTasks().First(t => t.Id == taskId2);
            var task3 = userInterfaceController.GetAllTasks().First(t => t.Id == taskId3);

            // Пример обновления задачи через UI
            task.Comment = "There is new comment";
            userInterfaceController.UpdateTaskComment(task);
            
            // Отмечаем задачи решенными
            userInterfaceController.MarkTaskAsResolved(task);
            userInterfaceController.MarkTaskAsResolved(task2);
            userInterfaceController.MarkTaskAsResolved(task3);
            
            // Обновляем отчет в связи с выполненными задачами
            userInterfaceController.UpdateDailyReport(report);
            userInterfaceController.UpdateDailyReport(report2);

            // Помечаем отчет выполненным
            report = userInterfaceController.GetAllReports().First(r => r.Id == reportId);
            userInterfaceController.MarkDailyReportFinalised(report);
            
            report2 = userInterfaceController.GetAllReports().First(r => r.Id == reportId2);
            userInterfaceController.MarkDailyReportFinalised(report2);

            // ---------------------------------------------------------------------------------------------------------
            
            // Вывод после всех изменений:
            report = userInterfaceController.GetAllReports().First(r => r.Id == reportId);
            Console.WriteLine("Состояние отчета:");
            Console.WriteLine($"Id: {report.Id}");
            Console.WriteLine($"Finalised: {report.Finalised}");
            Console.Write("Resolved taskId: ");
            foreach (var resolvedTaskId in report.ResolvedTasks)
            {
                Console.Write($"{resolvedTaskId} ");
            }
            Console.WriteLine();
            Console.WriteLine($"StaffId: {report.StaffId}");
            Console.WriteLine($"CreationDateTime: {report.CreationDateTime}");
        }
        
        public static void DeleteDirectory(string targetDir)
        {
            var files = Directory.GetFiles(targetDir);
            var dirs = Directory.GetDirectories(targetDir);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);
        }
    }
}