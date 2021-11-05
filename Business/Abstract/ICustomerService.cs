using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Delete(Customer customer);
        IResult Update(Customer customer);
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> Get(int customerId);
        IDataResult<Customer> GetByUserId(int userId);
    }
}
