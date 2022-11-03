using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetCarImagesByCarId(int carId);
        IDataResult<CarImage> GetById(int imageId);
        IResult Add(CarImage carImage);
        IResult Update(CarImage carImage, Image file);
        IResult Delete(CarImage carImage);
        IResult DeleteAllImagesOfCarByCarId(int carId);
    }
}