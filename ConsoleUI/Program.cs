using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
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

            User user = new User { FirstName = "Ahmet", LastName = "Özpolat", Email = "ozpolatahmet02@gmail.com", Password = "Ahmet123" };
            Customer customer = new Customer { CompanyName = "Timon MEGA" };
            Rental rental = new Rental { CustomerId = 1, CarId = 4, RentDate = new DateTime(2021, 08, 31) };

            customerManager.Add(customer);
            rentalManager.Add(rental);
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
