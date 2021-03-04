using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ICarService carManager = new CarManager(new EfCarDal());

            foreach (var car in carManager.GetCarsByBrandId(3))
            {
                Console.WriteLine(car.CarId + " " + car.Description);
            }

            Console.ReadLine();
            
            foreach (var cars in carManager.GetCarsByColorId(1))
            {
                Console.WriteLine(cars.CarId + " " + cars.Description);
            }

        }
    }
}
