using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal dal)
        {
            _brandDal = dal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.get")]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.get")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            var result = BusinessRules.Run(CheckIfBrandExist(brandId));
            if (result != null)
            {
                return new ErrorDataResult<Brand>(Messages.BrandNotFound);
            }
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId));
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
        }

        [CacheRemoveAspect("IBrandService.get")]
        [SecuredOperation("admin")]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        //Business Rules

        private IDataResult<int> CheckIfBrandExist(int brandId)
        {
            var result = _brandDal.Get(b => b.BrandId == brandId);
            if (result == null)
            {
                return new ErrorDataResult<int>();
            }
            return new SuccessDataResult<int>(brandId);
        }
    }
}
