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
        public CarImage CarImage { get; set; }
    }
}
