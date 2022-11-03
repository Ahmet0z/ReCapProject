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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal dal)
        {
            _colorDal = dal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.get")]
        public IResult Add(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorNameExist(color.ColorName));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            _colorDal.Add(color); ;
            return new SuccessResult(Messages.ColorAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.get")]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        [CacheAspect()]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorsListed);
        }

        public IDataResult<Color> GetById(int colorId)
        {
            var result = BusinessRules.Run(CheckIfColorExist(colorId));
            if (result != null)
            {
                return new ErrorDataResult<Color>(result.Message);
            }
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId));
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.get")]
        public IResult Update(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorExist(color.ColorId));
            if (result != null)
            {
                return new ErrorDataResult<Color>(result.Message);
            }
            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        //Business Rules

        private IDataResult<int> CheckIfColorExist(int colorId)
        {
            var result = _colorDal.Get(c => c.ColorId == colorId);
            if (result == null)
            {
                return new SuccessDataResult<int>();
            }
            return new ErrorDataResult<int>(Messages.ColorExists);
        }

        private IDataResult<string> CheckIfColorNameExist(string colorName)
        {
            var result = _colorDal.GetByColorName(colorName);
            if (result == null)
            {
                return new SuccessDataResult<string>();
            }
            return new ErrorDataResult<string>(Messages.ColorExists);
        }
    }
}
