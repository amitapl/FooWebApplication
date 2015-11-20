using FooWebApplication.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Linq;

namespace FooWebApplication.Selfhost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://+:2222"))
            {
                Console.WriteLine("Http");
                Console.ReadLine();
            }
        }
    }
}
