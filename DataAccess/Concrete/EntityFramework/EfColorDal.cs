using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EfEntityRepositoryBase<Color, ReCapContext>, IColorDal
    {
        public Color GetByColorName(string colorName)
        {
            using (ReCapContext context = new ReCapContext())
            {
                return context.Set<Color>().FirstOrDefault(c => c.ColorName == colorName);
            }
        }
    }
}
