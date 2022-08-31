using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IDataResult<List<RentalDetailDto>> GetRentalDetailsByUser(int userId);
        IDataResult<List<RentalDetailDto>> GetRentalDetailsByCar(int carId);
        IDataResult<List<RentalDetailDto>> GetRentalDetailsByCarAndUser(int carId, int userId);
    }
}
