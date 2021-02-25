using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            Console.WriteLine("GetAll");
            Console.WriteLine("  ");
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }

            
        }
    }
}
