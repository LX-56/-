using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomobileSalesSystem.Entitys;

namespace AutomobileSalesSystem.Models
{
    /// <summary>
    /// 试驾业务模型
    /// </summary>
    public class DrivetestModel
    {
        /// <summary>
        /// 获得所有试驾记录
        /// </summary>
        /// <returns>试驾记录列表</returns>
        public static List<Drivetest> GetAllDrivetests()
        {
            List<Drivetest> drivetests = new List<Drivetest>();

            using (var context = new car_sales_dbContext())
            {
                drivetests = context.Drivetest.ToList();
                if (drivetests.Count == 0)
                    throw new Exception("数据库中无试驾记录");
            }

            return drivetests;
        }

        /// <summary>
        /// 根据用户查找试驾记录
        /// </summary>
        /// <param name="cid">用户编号</param>
        /// <returns>试驾列表</returns>
        public static List<Drivetest> GetDrivetestsByClient(int cid)
        {
            List<Drivetest> drivetests = new List<Drivetest>();

            using (var context = new car_sales_dbContext())
            {
                drivetests = context.Drivetest.Where(x => x.Cid == cid).ToList();
                if (drivetests.Count == 0)
                    throw new Exception("数据库中无有关此用户的试驾记录");
            }

            return drivetests;

        }

        /// <summary>
        /// 根据车辆查找试驾记录
        /// </summary>
        /// <param name="cid">车辆编号</param>
        /// <returns>试驾列表</returns>
        public static List<Drivetest> GetDrivetestsBySalesman(int cid)
        {
            List<Drivetest> drivetests = new List<Drivetest>();

            using (var context = new car_sales_dbContext())
            {
                drivetests = context.Drivetest.Where(x => x.Cid == cid).ToList();
                if (drivetests.Count == 0)
                    throw new Exception("数据库中无有关此车辆的试驾记录");
            }

            return drivetests;

        }

        /// <summary>
        /// 试驾预约
        /// </summary>
        /// <param name="cid">用户编号</param>
        /// <param name="aid">车辆编号</param>
        public static void Appointment(int cid, int aid)
        {
            using (var context = new car_sales_dbContext())
            {
                if (context.Car.Where(x => x.Id == aid).ToList().Count != 0)
                    throw new Exception("该车辆不存在，无法进行试驾");
                if (context.Client.Where(x => x.Id == cid).ToList().Count != 0)
                    throw new Exception("该用户不存在，无法进行试驾");

                var data = new Drivetest()
                {
                    Cid = cid,
                    Aid = aid,
                    Active = false//默认状态未试驾
                };
                data.Did = context.Drivetest.ToList().Count == 0 ? 1 : context.Drivetest.ToList().Max(x => x.Did) + 1;//自动编号
                data.Testtime = DateTime.Now;
                context.Drivetest.Add(data);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="id">预约id</param>
        public static void Disappointment(int id)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Drivetest.First(x => x.Did == id);
                if (data == null)
                    throw new Exception("无此预约，请检查预约id");
                else
                    context.Drivetest.Remove(data);

                context.SaveChanges();
            }
        }
        /// <summary>
        /// 预约确认
        /// </summary>
        /// <param name="id">预约id</param>
        public static void Confirm(int id)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Drivetest.First(x => x.Did == id);
                if (data == null)
                    throw new Exception("无此用户，请检查用户id");
                data.Active = true;
                context.SaveChanges();
            }
        }
    }
}
