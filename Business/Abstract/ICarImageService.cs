using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult UploadCarImage(IFormFile formFile, CarImage carImage);
        IResult Delete(CarImage carImage);
        IResult Update(CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetImagesByCarId(int id);
    }
}
