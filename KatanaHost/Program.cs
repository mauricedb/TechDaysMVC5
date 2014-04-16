using System;
using Microsoft.Owin.Hosting;

namespace KatanaHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string url = "http://localhost:8081";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Listening at {0}", url);
                Console.ReadLine();
            }
        }
    }
}