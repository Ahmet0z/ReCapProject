using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerList(Expression<Func<CustomerDetailDto, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                IQueryable<CustomerDetailDto> customerDetails = from u in context.Users
                                                                join c in context.Customers
                                                                    on u.Id equals c.UserId
                                                                join r in context.Rentals
                                                                    on u.Id equals r.UserId
                                                                join cr in context.Cars
                                                                    on r.CarId equals cr.Id
                                                                select new CustomerDetailDto
                                                                {
                                                                    UserId = u.Id,
                                                                    CustomerId = c.CustomerId,
                                                                    CarId = cr.Id,
                                                                    CustomerName = u.FirstName + " " + u.LastName,
                                                                    CompanyName = c.CompanyName,
                                                                    Findeks = u.Findeks

                                                                };
                return filter == null ? customerDetails.ToList() : customerDetails.Where(filter).ToList();
            }
        }
    }
}
