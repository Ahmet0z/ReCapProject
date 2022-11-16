using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AddCarImageDTO
    {
        public Image Image { get; set; }
        public int CarId { get; set; }
    }
}
