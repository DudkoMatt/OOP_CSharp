using System;
using System.IO;
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
            var taskService = new TasksService(taskFileRepository, staffService);
            var reportService = new ReportService(reportFileRepository, staffService, taskService);

            // Создание UI
            var userInterfaceController = new UIController(reportService, staffService, taskService);
            
            // Создание работников
            var staffId = userInterfaceController.CreateStaff(new StaffVM("someName"));
            var staff = userInterfaceController.GetStaffById(staffId);

            var staffId2 = userInterfaceController.CreateStaff(new StaffVM("Name 2"));
            var staff2 = userInterfaceController.GetStaffById(staffId2);
            
            // Обновим:
            staff = userInterfaceController.GetStaffById(staffId);

            // Создание отчетов
            var reportId = userInterfaceController.CreateDailyReport(staff);
            var report = userInterfaceController.GetReportById(reportId);
            
            var reportId2 = userInterfaceController.CreateDailyReport(staff2);
            var report2 = userInterfaceController.GetReportById(reportId2);
            
            // Создание задач
            var taskId = userInterfaceController.CreateTask(new TaskVM("Task #1", "Test task"), staff);
            var taskId2 = userInterfaceController.CreateTask(new TaskVM("Task #2", "Test task 2"), staff2);
            var taskId3 = userInterfaceController.CreateTask(new TaskVM("Task #3", "Test task 3"), staff2);

            var task = userInterfaceController.GetTaskById(taskId);
            var task2 = userInterfaceController.GetTaskById(taskId2);
            var task3 = userInterfaceController.GetTaskById(taskId3);

            // Пример обновления задачи через UI
            var taskUpdated = new TaskVM(task.Name, "There is new comment", task.StaffId, task.State);
            userInterfaceController.UpdateTaskComment(task, taskUpdated);
            
            // Отмечаем задачи решенными
            userInterfaceController.MarkTaskAsResolved(task);
            userInterfaceController.MarkTaskAsResolved(task2);
            userInterfaceController.MarkTaskAsResolved(task3);
            
            // Обновляем отчет в связи с выполненными задачами
            report = userInterfaceController.GetReportById(reportId);
            report2 = userInterfaceController.GetReportById(reportId2);
            userInterfaceController.UpdateDailyReport(report);
            userInterfaceController.UpdateDailyReport(report2);

            // Помечаем отчет выполненным
            report = userInterfaceController.GetReportById(reportId);
            userInterfaceController.MarkDailyReportFinalised(report);
            
            report2 = userInterfaceController.GetReportById(reportId2);
            userInterfaceController.MarkDailyReportFinalised(report2);

            var sprintReportId = userInterfaceController.CreateSprintReport();
            userInterfaceController.UpdateSprintReport();
            userInterfaceController.MarkDailyReportFinalised(userInterfaceController.GetReportById(sprintReportId));

            // ---------------------------------------------------------------------------------------------------------
            
            // Вывод после всех изменений:
            report = userInterfaceController.GetReportById(reportId);
            Console.WriteLine("Состояние отчета:");
            Console.WriteLine($"Finalised: {report.Finalised}");
            Console.Write("Resolved taskId: ");
            foreach (var resolvedTaskId in report.ResolvedTasks)
            {
                Console.Write($"{resolvedTaskId} ");
            }
            Console.WriteLine();
            Console.WriteLine($"StaffId: {report.StaffId}");
            Console.WriteLine($"CreationDateTime: {report.CreationDateTime}");
            
            // ---------------------------------------------------------------------------------------------------------
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sprint report:");
            report = userInterfaceController.GetReportById(sprintReportId);
            Console.WriteLine("Состояние отчета:");
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