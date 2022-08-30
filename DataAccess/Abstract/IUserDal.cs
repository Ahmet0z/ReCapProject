using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Core.Entities;
using Core.Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        GetUserClaimsDto GetClaimsByUserId(int userId);
        List<OperationClaim> GetAllClaims(Expression<Func<OperationClaim, bool>> filter = null);
        OperationClaim GetOperationClaim(Expression<Func<OperationClaim, bool>> filter = null);
        UserOperationClaim GetUserOperationClaim(Expression<Func<UserOperationClaim, bool>> filter = null);
        void AddCLaim(UserOperationClaim userOperationClaim);
        void DeleteClaim(UserOperationClaim userOperationClaim);
        void AddFindeks(int userId, int findeks);
    }
}
