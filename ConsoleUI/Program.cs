using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Abstract;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            Car car1 = new Car
            {
                BrandId = 1,
                ColorId = 3,
                DailyPrice = 1500,
                Description = "X6",
                ModelYear = 2020
            };

            ICarService manager = new CarManager(new EfCarDal());
            IBrandService manager2 = new BrandManager(new EfBrandDal());
            IColorService manager3 = new ColorManager(new EfColorDal());

            manager.Add(car1);

            Console.WriteLine("----");
            foreach (var car in manager.GetAll())
            {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("----");
            foreach (var car in manager.GetCarsByBrandId(2))
            {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("----");
            foreach (var car in manager.GetCarsByColorId(1))
            {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("----");
            foreach (var brand in manager2.GetAll())
            {
                Console.WriteLine(brand.BrandName);
            }

            Console.WriteLine("----");
            foreach (var color in manager3.GetAll())
            {
                Console.WriteLine(color.ColorName);
            }
        }
    }
}
