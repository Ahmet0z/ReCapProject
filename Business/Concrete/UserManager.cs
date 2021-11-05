using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System.Collections.Generic;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Security.Hashing;
using Entities.DTOs;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }


        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }


        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetByUserId(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId),Messages.UserGetted);
        }

        public IResult ChangeUserPassword(ChangePasswordDto changePasswordDto)
        {
            byte[] passwordHash, passwordSalt;
            var userToCheck = GetByMail(changePasswordDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(changePasswordDto.OldPassword, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            userToCheck.Data.PasswordHash = passwordHash;
            userToCheck.Data.PasswordSalt = passwordSalt;
            _userDal.Update(userToCheck.Data);
            return new SuccessResult(Messages.PasswordChanged);
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email), Messages.UserGetted);
        }

        public IDataResult<List<OperationClaim>> GetUserClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user), Messages.ClaimsListed);
        }

        public IDataResult<List<OperationClaim>> GetClaimsById(int userId)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaimsByUserId(userId));
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
