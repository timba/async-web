using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace async_client
{
    class Program
    {
        static void Main(string[] args)
        {
            string mode = Console.ReadLine();
            string baseAddress = mode == "async" ?
                /* Option for local IIS, hosted on port 80*/
                "http://localhost/async-web/computersasync/ping/" :
                "http://localhost/async-web/computerssync/ping/";

                /* Option for dev web server, port 49000 */
                //"http://localhost:49000/computersasync/ping/" : 
                //"http://localhost:49000/computerssync/ping/";

            List<Task> tasks = new List<Task>();
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 300; i++)
            {
                Task task = new Task(() =>
                {
                    while (true)
                    {
                        WebRequest request = HttpWebRequest.Create(baseAddress + rand.Next(1, 15000));
                        var resp = request.GetResponse();
                        resp.Close();
                        Thread.Sleep(50);
                    }
                });

                task.Start();
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
