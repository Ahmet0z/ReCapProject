using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
        IResult ChangeUserPassword(ChangePasswordDto changePasswordDto);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetByUserId(int userId);
        IDataResult<List<OperationClaim>> GetUserClaims(User user);
        IDataResult<List<OperationClaim>> GetClaimsById(int userId);
    }
}
