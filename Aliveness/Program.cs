using System;
using System.IO;
using System.Threading;

namespace Aliveness
{
  internal class Program
  {
    static void Main()
    {
      Action<string> Display = Console.WriteLine;
      Display("testing aliveness of the VM");
      while (true)
      {
        Thread.Sleep(60000);
        WriteToFile();
        Display($"{Environment.MachineName} is alive at {DateTime.Now}");
      }

      Display("Press any key to exit:");
      Console.ReadKey();
    }

    private static void WriteToFile()
    {
      try
      {
        using (StreamWriter sw = new StreamWriter("alive.txt", true))
        {
          sw.WriteLine($"{Environment.MachineName} is alive at {DateTime.Now}");
        }
      }
      catch (Exception)
      {
      }
    }
  }
}
