using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal dal)
        {
            _carDal = dal;
        }

        public void Add(Car car)
        {
            _carDal.Add(car);
            Console.WriteLine(car.Id + " eklendi");
        }

        public void Delete(int id)
        {
            _carDal.Delete(id);
            Console.WriteLine(id + " id li veri silindi");
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetById(int id)
        {
            return _carDal.GetById(id);
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
            Console.WriteLine("güncellendi");
        }
    }
}
