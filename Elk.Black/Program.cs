using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Diagnostics.Eventing.Reader;


namespace Elk.Black
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Load Elk configuration first
                Elk.Config.LoadConfiguration();

                Console.WriteLine("CONFIG SOURCES FOUND");

                foreach (var item in Elk.Config.SourceOptions)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("SOME LOGS...");
                var records = new ElkEventLog("Application", "IIS Express", "localhost")
                    .NewQuery()
                    .FilterByLogLevel(LogLevels.Warning)
                    .FilterByTimeAgo(TimeSpan.FromDays(1))
                    .Run();

                foreach (var eventRecord in records)
                {
                    Console.WriteLine(String.Format("{0} - {1}",
                        eventRecord.TimeCreated,
                        eventRecord.FormatDescription()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }
    }
}
