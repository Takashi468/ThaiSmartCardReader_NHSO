using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace BPH_ER_Smart_Kiosk.Helpers
{
    public static class Logger
    {
        public enum LEVEL
        {
            INFO,
            WARNING,
            ERROR
        }

        public enum LogFrequency
        {
            Daily,
            Weekly
        }

        private static readonly string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static LogFrequency logFrequency = LogFrequency.Daily;

        private static string GetLogFilePath()
        {
            string logFileName = logFrequency == LogFrequency.Daily
                ? $"log_{DateTime.Now:yyyyMMdd}.log"
                : $"log_{DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek):yyyyMMdd}.log";

            return Path.Combine(logDirectory, logFileName);
        }

        static Logger()
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public static void Log(
            string message,
            LEVEL logType = LEVEL.INFO,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                string logLevel = logType.ToString();
                string fileName = Path.GetFileName(filePath); // ดึงแค่ชื่อไฟล์

                string logFilePath = GetLogFilePath();
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {fileName}({lineNumber}) {memberName}: {message}";

                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing log: {ex.Message}");
            }
        }

        public static string ReadLog()
        {
            try
            {
                string logFilePath = GetLogFilePath();
                return File.Exists(logFilePath) ? File.ReadAllText(logFilePath) : "Log file is empty.";
            }
            catch (Exception ex)
            {
                return $"Error reading log: {ex.Message}";
            }
        }

        public static void ClearLog()
        {
            try
            {
                string logFilePath = GetLogFilePath();
                if (File.Exists(logFilePath))
                {
                    File.WriteAllText(logFilePath, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing log: {ex.Message}");
            }
        }

        public static void SetLogFrequency(LogFrequency frequency)
        {
            logFrequency = frequency;
        }
    }
}