using System;
using System.IO;

public struct LogEntry
{
    public DateTime Timestamp;
    public string Level;
    public string Category;
    public string Message;
}

public class Program
{
    public static void Main()
    {
        string[] txt = File.ReadAllLines("event_server.log");
        LogEntry[] massiv = ParseLog(txt);
        
        string status = GetServerStatus(massiv);
        Console.WriteLine(status);
    }
    public static LogEntry[] ParseLog(string[] lines)
    {
        LogEntry[] entries = new LogEntry[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string s = lines[i];
            LogEntry entry = new LogEntry();

            entry.Timestamp = DateTime.Parse(s.Substring(0, 23));

            int op1 = s.IndexOf('[');
            int cl1 = s.IndexOf(']', op1);
            int op2 = s.IndexOf('[', cl1);
            int cl2 = s.IndexOf(']', op2);

            entry.Level = s.Substring(op1 + 1, cl1 - op1 - 1);
            entry.Category = s.Substring(op2 + 1, cl2 - op2 - 1);
            entry.Message = s.Substring(cl2 + 2);

            entries[i] = entry;
        }

        return entries;
    }
    public static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
    {
        LogEntry[] temporaryResult = new LogEntry[entries.Length];
        int resultCount = 0;

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Timestamp.Date == date.Date)
            {
                temporaryResult[resultCount] = entries[i];
                resultCount++;
            }
        }

        LogEntry[] result = new LogEntry[resultCount];
        Array.Copy(temporaryResult, result, resultCount);
        return result;
    }

    public static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
    {
        LogEntry[] temporaryResult = new LogEntry[entries.Length];
        int resultCount = 0;

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == level)
            {
                temporaryResult[resultCount] = entries[i];
                resultCount++;
            }
        }

        LogEntry[] result = new LogEntry[resultCount];
        Array.Copy(temporaryResult, result, resultCount);
        return result;
    }

    public static LogEntry[] FilterByCategory(LogEntry[] entries, string category)
    {
        LogEntry[] temporaryResult = new LogEntry[entries.Length];
        int resultCount = 0;

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Category == category)
            {
                temporaryResult[resultCount] = entries[i];
                resultCount++;
            }
        }

        LogEntry[] result = new LogEntry[resultCount];
        Array.Copy(temporaryResult, result, resultCount);
        return result;
    }

    public static LogEntry[] Search(LogEntry[] entries, string text)
    {
        LogEntry[] temporaryResult = new LogEntry[entries.Length];
        int resultCount = 0;

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Message.ToLower().Contains(text.ToLower()))
            {
                temporaryResult[resultCount] = entries[i];
                resultCount++;
            }
        }

        LogEntry[] result = new LogEntry[resultCount];
        Array.Copy(temporaryResult, result, resultCount);
        return result;
    }

    public static int CountByLevel(LogEntry[] entries, string level)
    {
        int count = 0;
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == level)
            {
                count++;
            }
        }
        return count;
    }

    public static string GetServerStatus(LogEntry[] entries)
    {
        bool estOshibka = false;

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == "Fatal" && entries[i].Category == "Server")
            {
                return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
            }

            if (entries[i].Level == "Error")
            {
                estOshibka = true;
            }
        }

        if (estOshibka == true)
        {
            return "Есть ошибки: требуется проверка";
        }

        return "Сервер работает штатно";
    }

    public static void ExportFiltered(string path, LogEntry[] entries)
    {
        string[] outLines = new string[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string d = entries[i].Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
            outLines[i] = d + " [" + entries[i].Level + "][" + entries[i].Category + "] " + entries[i].Message;
        }

        File.WriteAllLines(path, outLines);
    }
}
