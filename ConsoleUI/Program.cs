using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarTest();

            //ColorTest();

            //BrandTest();

            IUserService userManager = new UserManager(new EfUserDal());
            ICustomerService customerManager = new CustomerManager(new EfCustomerDal());
            IRentalService rentalManager = new RentalManager(new EfRentalDal());

            var result = userManager.GetByMail("ahmet@ozpolat.com");
            Console.WriteLine(result.Data.FirstName);

        }

        private static void BrandTest()
        {
            IBrandService brandManager = new BrandManager(new EfBrandDal());

            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine(brand.BrandName);
            }
        }

        private static void ColorTest()
        {
            IColorService colorManager = new ColorManager(new EfColorDal());

            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.ColorName);
            }
        }

        private static void CarTest()
        {
            ICarService carManager = new CarManager(new EfCarDal());

            var result = carManager.Get(3);
            Console.WriteLine(result.Data);

            var result1 = carManager.GetCarDetails();

            if (result1.Success)
            {
                foreach (var car in result1.Data)
                {
                    Console.WriteLine(car.CarName + " / " + car.BrandName + " / " + car.ColorName + " / " + car.DailyPrice);
                }
            }

            
        }
    }
}
