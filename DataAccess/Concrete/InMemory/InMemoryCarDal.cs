using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal:ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{CarId=1, BrandId=1, ColorId=1, DailyPrice=300, ModelYear=2015 , Description="BMW 520"},
                new Car{CarId=2, BrandId=2, ColorId=1, DailyPrice=250, ModelYear=2014 , Description="Audi A3"},
                new Car{CarId=3, BrandId=3, ColorId=2, DailyPrice=150, ModelYear=2016 , Description="Fiat Egea"},
                new Car{CarId=4, BrandId=4, ColorId=3, DailyPrice=170, ModelYear=2018 , Description="Opel Astra"},
                new Car{CarId=5, BrandId=5, ColorId=2, DailyPrice=225, ModelYear=2019 , Description="Volkswagen Golf"},
                new Car{CarId=6, BrandId=6, ColorId=5, DailyPrice=350, ModelYear=2017 , Description="Mercedes Vito"}
            };

        }

        public void Add(Car entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car entity)
        {
            throw new NotImplementedException();
        }
    }
}
