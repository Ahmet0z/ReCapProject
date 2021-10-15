using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete
{
    public class Image
    {
        public IFormFile file { get; set; }
    }
}
