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
      var timeToSleep = 60000;
      while (true)
      {
        Thread.Sleep(timeToSleep);
        var oneLine = $"{DateTime.Now.ToShortDateString()},{Environment.MachineName},{DateTime.Now.ToLongTimeString()}";
        WriteToFile(oneLine, true, "alive.txt");
        Display(oneLine);
        WriteToFile(oneLine, false, "lastEntry.txt");
      }
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
