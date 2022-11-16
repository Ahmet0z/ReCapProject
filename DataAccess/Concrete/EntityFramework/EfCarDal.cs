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
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                                 on c.BrandId equals b.BrandId
                             join co in context.Colors
                                 on c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandId = b.BrandId,
                                 ColorId = c.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 CarName = c.CarName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 Findeks = c.Findeks,
                                 Plate = c.Plate,

                                 IsRentable = !context.Rentals.Any(r => r.CarId == c.Id && (r.ReturnDate == null || (r.ReturnDate.HasValue && r.ReturnDate > DateTime.Now))),
                                 ImagePath = (from ci in context.CarImages where c.Id == ci.CarId select ci.ImagePath).FirstOrDefault()
                             };
                return filter == null ? result.OrderByDescending(c => c.Id).ToList() : result.Where(filter).OrderByDescending(c => c.Id).ToList();
            }
        }
    }
}
