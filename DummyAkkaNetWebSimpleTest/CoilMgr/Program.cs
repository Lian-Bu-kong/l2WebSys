using System;

namespace CoilMgr
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
