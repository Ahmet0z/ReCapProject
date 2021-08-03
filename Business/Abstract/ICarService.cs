﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        void Add(Car car);
        void Delete(int id);
        void Update(Car car);
        List<Car> GetAll();
        List<Car> GetById(int id);
    }
}
