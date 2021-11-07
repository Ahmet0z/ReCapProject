using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from car in context.Cars
                             join rental in context.Rentals
                             on car.Id equals rental.CarId
                             join user in context.Users
                             on rental.UserId equals user.Id
                             select new RentalDetailDto
                             {
                                 CarName = car.CarName,
                                 CustomerName = user.FirstName + " " + user.LastName,
                                 Id = rental.Id,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.ToList();

            }
        }

    }
}
