using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.DTOs
{
    public class GetUserClaimsDto:IDto
    {
        public List<UserOperationClaim> userOperationClaim { get; set; }
        public List<OperationClaim> operationClaim { get; set; }
    }
}
