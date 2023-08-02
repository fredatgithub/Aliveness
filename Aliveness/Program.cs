using System;
using System.IO;
using System.Threading;

namespace TestAliveness
{
  internal class Program
  {
    static void Main()
    {
      Action<string> Display = Console.WriteLine;
      Display("testing aliveness of the VM");
      var timeToSleep = 60000; // test 1000 and prod 60000
      var fileHistory = "alive.txt";
      var fileLastEntry = "lastEntry.txt";
      if (!File.Exists(fileHistory))
      {
        File.Create(fileHistory);
      }

      if (!File.Exists(fileLastEntry))
      {
        File.Create(fileLastEntry);
      }

      string lastEntry = ReadFile(fileLastEntry);
      WriteToFile(lastEntry, true, fileHistory);

      while (true)
      {
        Thread.Sleep(timeToSleep);
        var oneLine = $"{DateTime.Now.ToShortDateString()},{Environment.MachineName},{DateTime.Now.ToLongTimeString()}";
        Display(oneLine);
        WriteToFile(oneLine, false, "lastEntry.txt");
      }
    }

    private static string ReadFile(string fileLastEntry)
    {
      string result = string.Empty;
      try
      {
        using (StreamReader sr = new StreamReader(fileLastEntry))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            result = line;
          }
        }
      }
      catch (Exception)
      {
        // do nothing
      }

      return result;
    }

    private static void WriteToFile(string message, bool append, string filename)
    {
      try
      {
        using (StreamWriter sw = new StreamWriter(filename, append))
        {
          sw.WriteLine(message);
        }
      }
      catch (Exception)
      {
      }
    }
  }
}
