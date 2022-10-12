using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<RentalDetailDto, bool>> filter = null)
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
                                 UserName = user.FirstName + " " + user.LastName,
                                 Id = rental.Id,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate,
                                 CarId = car.Id,
                                 UserId = user.Id,
                                 Plate = car.Plate
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }
    }
}
