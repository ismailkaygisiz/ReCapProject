using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCar();
            TestBrand();
            TestColor();
        }

        private static void TestColor()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            foreach (Color color in colorManager.GetAll())
            {
                Console.WriteLine(color.ColorName);
            }
        }

        private static void TestBrand()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            foreach (Brand brand in brandManager.GetAll())
            {
                Console.WriteLine(brand.BrandName);
            }
        }

        private static void TestCar()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (Car car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }
        }
    }
}
