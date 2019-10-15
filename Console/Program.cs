using Check;
using System;
using System.Data.Entity;

namespace CheckConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());
            using (var context=new CheckContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Console.Write;
            }
        }
    }
}
