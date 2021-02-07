﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        List<Car> GetByBrandId(int brandId);
        List<Car> GetByColorId(int colorId);
        Car GetById(int id);
        List<Car> GetByDailyPrice(int min, int max);
        List<Car> GetByModelYear(int modelYear);

    }
}
