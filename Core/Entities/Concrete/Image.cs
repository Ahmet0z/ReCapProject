using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Entities.Concrete
{
    public class Image
    {
        public IFormFile File { get; set; }
    }
}
