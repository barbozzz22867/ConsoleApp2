using System;
using System.Diagnostics;
using System.ComponentModel;

//Доп вопрос Без WaitForExitAsync() чтение ExitCode у ещё работающего процесса бросит
//InvalidOperationException, а если он успел завершиться — даст нестабильный результат из-за
//гонки, так как код возврата фиксируется ОС только в момент завершения.
public class Program
{
    public static async Task Main()
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "dotnet",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // TODO 1. Аргумент "--info"
        startInfo.ArgumentList.Add("--info");

        // TODO 2. Рабочая директория
        startInfo.WorkingDirectory = Environment.CurrentDirectory;

        using Process process = new()
        {
            StartInfo = startInfo
        };

        Stopwatch stopwatch = new();

        try
        {
            // TODO 3. Запуск секундомера
            stopwatch.Start();

            // TODO 4. Запуск процесса
            process.Start();

            // TODO 5. PID процесса
            Console.WriteLine($"PID: {process.Id}");

            // TODO 6. Ожидание завершения
            await process.WaitForExitAsync();

            // TODO 7. Остановка секундомера
            stopwatch.Stop();

            // TODO 8. Длительность и ExitCode
            Console.WriteLine($"Длительность: {stopwatch.Elapsed}");
            Console.WriteLine($"Exit code: {process.ExitCode}");
        }
        catch (Win32Exception ex)
        {
            // TODO 9. Обработанное сообщение об ошибке
            stopwatch.Stop();
            Console.WriteLine($"Ошибка запуска: {ex.Message} (код {ex.NativeErrorCode})");
        }
    }
}