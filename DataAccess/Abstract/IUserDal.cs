using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<OperationClaim> GetClaimsByUserId(int userId);
        void AddFindeks(int userId, int findeks);
    }
}
