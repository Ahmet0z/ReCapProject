using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Business;
using Core.Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        private IUserService _userService;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal, IUserService userService, ICarService carService)
        {
            _rentalDal = rentalDal;
            _userService = userService;
            _carService = carService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.get")]
        [CacheRemoveAspect("ICarService.get")]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(IsFindexEnough(rental), CheckIsCarRentable(rental.CarId));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            Car car = GetCar(rental.CarId).Data;
            AddFindeksToUser(rental.UserId, car.Findeks);
            _rentalDal.Add(rental);

            return new SuccessResult(Messages.RentalAdded);

        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IRentalService.get")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalDetailsListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IRentalService.get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsByUser(int userId)
        {
            var result = _rentalDal.GetRentalDetails(r=> r.UserId == userId);
            return new SuccessDataResult<List<RentalDetailDto>>(result, Messages.RentalDetailsListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsByCar(int carId)
        {
            var result = _rentalDal.GetRentalDetails(r => r.CarId == carId);
            return new SuccessDataResult<List<RentalDetailDto>>(result, Messages.RentalDetailsListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsByCarAndUser(int carId, int userId)
        {
            var result = _rentalDal.GetRentalDetails(r => r.UserId == userId && r.CarId == carId);
            return new SuccessDataResult<List<RentalDetailDto>>(result, Messages.RentalDetailsListed);
        }



        //Business Codes

        private IResult IsFindexEnough(Rental rental)
        {
            int carFindex = this._carService.GetCarFindeks(rental.CarId).Data;
            int userFindex = _userService.GetUserFindeks(rental.UserId).Data;

            if (userFindex >= carFindex)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.NotEnoughFindex);
        }

        private void AddFindeksToUser(int userId, int findeks)
        {
            int userFindeks = _userService.GetUserFindeks(userId).Data;

            if (userFindeks < 1900)
            {
                _userService.AddFindeks(userId, findeks);
            }
        }

        private IDataResult<Car> GetCar(int carId)
        {
            var result = _carService.GetByCarId(carId);
            if (result.Success)
            {
                return new SuccessDataResult<Car>(result.Data);
            }
            return new ErrorDataResult<Car>(Messages.CarNotFound);
        }

        private IResult CheckIsCarRentable(int carId)
        {
            var rentalCar = _rentalDal.GetAll().FindLast(r => r.CarId == carId);

            if (rentalCar == null || (rentalCar != null && rentalCar.ReturnDate != new DateTime()))
            {

                return new SuccessResult();
            }

            return new ErrorResult(Messages.CarIsOnRent);
        }

    }
}
