using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Core.Entities;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<OperationClaim> GetClaimsByUserId(int userId);
        OperationClaim GetOperationClaim(Expression<Func<OperationClaim, bool>> filter = null);
        UserOperationClaim GetUserOperationClaim(Expression<Func<UserOperationClaim, bool>> filter = null);
        void AddCLaim(UserOperationClaim userOperationClaim);
        void DeleteClaim(UserOperationClaim userOperationClaim);
        void AddFindeks(int userId, int findeks);
    }
}
