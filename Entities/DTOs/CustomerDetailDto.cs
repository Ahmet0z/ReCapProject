using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.DTOs
{
    public class CustomerDetailDto:IDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public int Findeks { get; set; }


    }
}
