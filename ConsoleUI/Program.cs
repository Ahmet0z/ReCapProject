using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car { Id = 2, BrandId = 1, ColorId = 3, DailyPrice = 500, Description = "Deneme", ModelYear = 2010 };


            ICarService manager = new CarManager(new InMemoryCarDal());

            foreach (var car in manager.GetAll())
            {
                Console.WriteLine(car.Description);
            }
            

            foreach (var car in manager.GetById(2))
            {
                Console.WriteLine(car.Description);
            }

            manager.Add(new Car {Id=6, BrandId=3, ColorId=3, DailyPrice=540, Description="A200", ModelYear=2000});

            foreach (var car in manager.GetById(6))
            {
                Console.WriteLine(car.Description);
            }

            manager.Delete(6);

            manager.Update(car1);

            foreach (var car in manager.GetById(2))
            {
                Console.WriteLine(car.Description);
            }

        }
    }
}
