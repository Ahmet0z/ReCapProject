using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;
using Entities.DTOs;
using Core.Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(UserUpdateDto user);
        IResult ChangeUserPassword(ChangePasswordDto changePasswordDto);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetByUserId(int userId);
        IDataResult<List<OperationClaim>> GetUserClaims(User user);
        IDataResult<GetUserClaimsDto> GetClaimsById(int userId);
        IDataResult<List<OperationClaim>> GetAllClaims();
        IDataResult<int> GetUserFindeks(int userId);
        IResult AddFindeks(int userId, int findeks);
        IResult AddUserOperationClaim(UserOperationClaim userOperationClaim);
        IResult DeleteUserOperationClaim(UserOperationClaim userOperationClaim);
    }
}
