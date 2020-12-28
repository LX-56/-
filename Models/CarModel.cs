using AutomobileSalesSystem.Entitys;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomobileSalesSystem.Models
{
    public class CarModel
    {
        /// <summary>
        /// 获得所有车辆信息
        /// </summary>
        /// <returns>汽车列表</returns>
        public static List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            using (var context = new car_sales_dbContext())
            {
                cars = context.Car.ToList();
                if (cars.Count == 0)
                {
                    throw new Exception("数据库中无车辆信息");
                }
            }

            return cars;
        }

        /// <summary>
        /// 根据车辆编号查找车辆
        /// </summary>
        /// <param name="id">车辆编号</param>
        /// <returns>车辆信息</returns>
        public static Car GetCarById(int id)
        {
            Car car = new Car();
            using(var context = new car_sales_dbContext())
            {
                car = context.Car.First(x => x.Id == id);
                if(car == null)
                {
                    throw new Exception("数据库中无此车辆信息");
                }
            }
            return car;
        }

        /// <summary>
        /// 根据车辆型号车辆信息
        /// </summary>
        /// <param name="model">车辆型号</param>
        /// <returns>车辆列表</returns>

        public static List<Car> GetCarsByModel(string model)
        {
            List<Car> cars = new List<Car>();
            using (var context = new car_sales_dbContext())
            {
                cars = context.Car.Where(x=>x.Model == model).ToList();
                if (cars.Count == 0)
                {
                    throw new Exception("数据库中无此类型车辆信息");
                }
            }

            return cars;
        }

        /// <summary>
        /// 根据车辆类型查找车辆信息
        /// </summary>
        /// <param name="type">车辆类型</param>
        /// <returns>车辆列表</returns>
        public static List<Car> GetCarsByType(string type)
        {
            List<Car> cars = new List<Car>();
            using (var context = new car_sales_dbContext())
            {
                cars = context.Car.Where(x => x.Type == type).ToList();
                if (cars.Count == 0)
                {
                    throw new Exception("数据库中无此类型车辆信息");
                }
            }

            return cars;
        }

        /// <summary>
        /// 根据车辆厂家查找车辆
        /// </summary>
        /// <param name="factory">车辆厂家</param>
        /// <returns>车辆列表</returns>
        public static List<Car> GetCarsByFactory(string factory)
        {
            List<Car> cars = new List<Car>();
            using (var context = new car_sales_dbContext())
            {
                cars = context.Car.Where(x => x.Factory == factory).ToList();
                if (cars.Count == 0)
                {
                    throw new Exception("数据库中无此类型车辆信息");
                }
            }

            return cars;
        }

        /// <summary>
        /// 新车入库
        /// </summary>
        /// <param name="type">车辆型号</param>
        /// <param name="model">车辆类型</param>
        /// <param name="factory">生产商家</param>
        public static void NewCar(string type, string model, string factory)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = new Car()
                {
                    Type = type,
                    Model = model,
                    Factory = factory
                };
                data.Id = context.Car.ToList().Count == 0 ? 1 : context.Car.ToList().Max(x => x.Id) + 1;//自动编号
                data.Datatime = DateTime.Now;
                context.Car.Add(data);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 修改车辆信息
        /// </summary>
        /// <param name="id">车辆编号</param>
        /// <param name="type">车辆类型</param>
        /// <param name="model">车辆型号</param>
        /// <param name="factory">车辆厂家</param>
        public static void ChangeCar(int id, string type, string model, string factory)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Car.First(x => x.Id == id);
                if (data == null)
                    throw new Exception("无此车辆，请检查车辆id");
                data.Type = type;
                data.Model = model;
                data.Factory = factory;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="id">车辆编号</param>
        public static void DeleteCar(int id)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Car.First(x => x.Id == id);

                if (data == null)
                    throw new Exception("无此车辆，请检查车辆id");
                else if (data.Market != null)
                    throw new Exception("该车辆已被销售，不能删除");
                else
                    context.Car.Remove(data);

                context.SaveChanges();
            }
        }
    }
}
