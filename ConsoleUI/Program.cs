﻿using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("**********************************");
                Console.WriteLine("Description : {0}", car.Description);
                Console.WriteLine("Daily Price : {0}", car.DailyPrice);
                Console.WriteLine("Model Year : {0}", car.ModelYear + "\n");
            }
        }
    }
}