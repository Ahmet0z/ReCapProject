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
using Core.Utilities.Business;
using Core.Entities.DTOs;

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

        [SecuredOperation("admin")]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetByUserId(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId), Messages.UserGetted);
        }

        [SecuredOperation("user,admin")]
        public IResult ChangeUserPassword(ChangePasswordDto changePasswordDto)
        {
            byte[] passwordHash, passwordSalt;

            var userToCheck = GetByMail(changePasswordDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var result = BusinessRules.Run(IsPasswordCorrect(changePasswordDto.OldPassword, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt));

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

        [SecuredOperation("admin")]
        public IDataResult<GetUserClaimsDto> GetClaimsById(int userId)
        {
            return new SuccessDataResult<GetUserClaimsDto>(_userDal.GetClaimsByUserId(userId), Messages.ClaimsListed);
        }

        public IDataResult<int> GetUserFindeks(int userId)
        {
            var result = _userDal.Get(u => u.Id == userId);
            return new SuccessDataResult<int>(result.Findeks);
        }

        public IResult AddFindeks(int userId, int findeks)
        {
            _userDal.AddFindeks(userId, findeks);
            return new SuccessResult();
        }

        [SecuredOperation("user,admin")]
        public IResult Update(UserUpdateDto user)
        {
            var userToUpdate = GetByMail(user.Email);

            userToUpdate.Data.Email = user.Email;
            userToUpdate.Data.FirstName = user.FirstName;
            userToUpdate.Data.LastName = user.LastName;

            _userDal.Update(userToUpdate.Data);
            return new SuccessResult(Messages.UserUpdated);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult AddUserOperationClaim(UserOperationClaim userOperationClaim)
        {
            var result = BusinessRules.Run(IsClaimExist(userOperationClaim.OperationClaimId), IsUserExist(userOperationClaim.UserId), 
                IsUserHasClaim(userOperationClaim.UserId, userOperationClaim.OperationClaimId));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            _userDal.AddCLaim(userOperationClaim);
            return new SuccessResult(Messages.ClaimAdded);
        }

        [SecuredOperation("admin")]
        public IResult DeleteUserOperationClaim(UserOperationClaim userOperationClaim)
        {
            var result = BusinessRules.Run(IsUserHasClaim(userOperationClaim.UserId, userOperationClaim.OperationClaimId));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            _userDal.DeleteClaim(userOperationClaim);
            return new SuccessResult(Messages.ClaimDeleted);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<OperationClaim>> GetAllClaims()
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetAllClaims(), Messages.ClaimsListed);
        }

        [SecuredOperation("admin")]
        public IResult DisableUser(User user)
        {
            _userDal.DisableUser(user);
            return new SuccessResult(Messages.UserDisabled);
        }


        //Business Rules

        private IResult IsUserExist(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.UserNotFound);
        }

        private IResult IsClaimExist(int operationClaimId)
        {
            var claim = _userDal.GetOperationClaim(o => o.Id == operationClaimId);
            if (claim != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.InvalidClaimId);
        }

        private IResult IsUserHasClaim(int userId, int operationClaimId)
        {
            var result = _userDal.GetUserOperationClaim(uoc => uoc.UserId == userId && uoc.OperationClaimId == operationClaimId);

            if (result == null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.UserAlreadyHaveThisClaim);
        }

        private IResult IsPasswordCorrect(string oldPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(oldPassword, passwordHash, passwordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }
            return new SuccessResult();
        }

    }
}
