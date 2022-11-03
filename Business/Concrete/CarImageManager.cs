using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly ICarDal _carDal;

        public CarImageManager(ICarImageDal carImageDal, ICarDal carDal)
        {
            _carImageDal = carImageDal;
            _carDal = carDal;
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.ImagessListed);
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            var checkIfCarImage = CheckIfCarHasImage(carId);
            var images = checkIfCarImage.Success
                ? checkIfCarImage.Data
                : _carImageDal.GetAll(c => c.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(images, checkIfCarImage.Message);
        }

        public IDataResult<CarImage> GetById(int imageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == imageId), Messages.ImagessListed);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(CarImage carImage)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfCarImageLimitExceeded(carImage.CarId));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            //var imageResult = FileHelper.Upload(carImage.Image.File);

            //if (!imageResult.Success)
            //{
            //    return new ErrorResult(imageResult.Message);
            //}
            //carImage.CarImage.ImagePath = imageResult.Message;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ImageAdded);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(CarImage carImage, Image image)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfCarIdExist(carImage.Id),
                CheckIfCarImageLimitExceeded(carImage.CarId));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var updatedImage = _carImageDal.Get(c => c.Id == carImage.Id);
            var result = FileHelper.Update(image.File, updatedImage.ImagePath);
            if (!result.Success)
            {
                return new ErrorResult(Messages.ErrorUpdatingImage);
            }
            carImage.ImagePath = result.Message;
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(CarImage carImage)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfCarIdExist(carImage.CarId));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var deletedImage = _carImageDal.Get(c => c.Id == carImage.Id);
            var result = FileHelper.Delete(deletedImage.ImagePath);
            if (!result.Success)
            {
                return new ErrorResult(Messages.ErrorDeletingImage);
            }
            _carImageDal.Delete(deletedImage);
            return new SuccessResult(Messages.ImageDeleted);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult DeleteAllImagesOfCarByCarId(int carId)
        {
            var deletedImages = _carImageDal.GetAll(c => c.CarId == carId);
            if (deletedImages == null)
            {
                return new ErrorResult(Messages.NoPictureOfTheCar);
            }
            foreach (var deletedImage in deletedImages)
            {
                _carImageDal.Delete(deletedImage);
                FileHelper.Delete(deletedImage.ImagePath);
            }
            return new SuccessResult(Messages.ImageDeleted);
        }

        //Business Rules

        private IResult CheckIfCarImageLimitExceeded(int carId)
        {
            int result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceded);
            }
            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> CheckIfCarHasImage(int carId)
        {
            string logoPath = "/images/default.jpg";
            bool result = _carImageDal.GetAll(c => c.CarId == carId).Any();
            if (!result)
            {
                List<CarImage> imageList = new List<CarImage>
                {
                    new CarImage
                    {
                        ImagePath = logoPath,
                        CarId = carId,
                        Date = DateTime.Now
                    }
                };
                return new SuccessDataResult<List<CarImage>>(imageList, Messages.GetDefaultImage);
            }
            return new ErrorDataResult<List<CarImage>>(new List<CarImage>(), Messages.ImagessListed);
        }

        private IResult CheckIfCarIdExist(int carId)
        {
            var result = _carDal.Get(c => c.Id == carId);
            if (result == null)
            {
                return new ErrorResult(Messages.CarNotFound);
            }
            return new SuccessResult();
        }
    }
}