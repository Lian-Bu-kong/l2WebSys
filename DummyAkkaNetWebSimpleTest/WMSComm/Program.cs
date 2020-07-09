using MMSComm;
using System;

namespace WMSComm
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            System.Console.ReadLine();
        }
    }
}
