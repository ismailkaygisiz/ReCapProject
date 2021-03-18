﻿using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    join carImage in context.CarImages on car.Id equals carImage.CarId
                    select new CarDetailDto()
                    {
                        Id = car.Id,
                        BrandName = brand.BrandName,
                        CarName = car.Description,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice,
                        ModelYear = car.ModelYear,
                        ImagePath = carImage.ImagePath
                    };

                return result.ToList();
            }
        }
    }
}
