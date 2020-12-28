using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomobileSalesSystem.Entitys;

namespace AutomobileSalesSystem.Models
{
    /// <summary>
    /// 销售员业务模型
    /// </summary>
    public class SalesmanModel
    {
        /// <summary>
        /// 获得所有销售员信息
        /// </summary>
        /// <returns>销售员列表</returns>
        public static List<Salesman> GetAllSalesman()
        {
            List<Salesman> salesman = new List<Salesman>();
            using (var context = new car_sales_dbContext())
            {
                salesman = context.Salesman.ToList();
                if (salesman.Count == 0)
                {
                    throw new Exception("数据库中无销售员信息");
                }
            }

            return salesman;
        }

        /// <summary>
        /// 根据id查找销售员
        /// </summary>
        /// <param name="id">销售员id</param>
        /// <returns>销售员信息</returns>
        public static Salesman GetSalesmanById(int id)
        {
            Salesman salesman = new Salesman();
            using (var context = new car_sales_dbContext())
            {
                salesman = context.Salesman.Where(x => x.Id == id).FirstOrDefault();
                if (salesman == null)
                {
                    throw new Exception("数据库中无此销售员");
                }
            }

            return salesman;
        }

        /// <summary>
        /// 根据销售员姓名查找销售员（不唯一）
        /// </summary>
        /// <param name="name">销售员姓名</param>
        /// <returns>销售员列表</returns>
        public static List<Salesman> GetSalesmansByName(string name)
        {
            List<Salesman> salesman = new List<Salesman>();
            using (var context = new car_sales_dbContext())
            {
                salesman = context.Salesman.Where(x => x.Name == name).ToList();
                if (salesman.Count == 0)
                {
                    throw new Exception("数据库中无此销售员");
                }
            }

            return salesman;
        }

        /// <summary>
        /// 创建新销售员
        /// </summary>
        /// <param name="name">销售员姓名</param>
        /// <param name="sex">销售员性别</param>
        /// <param name="tel">销售员电话</param>
        public static void NewSalesman(string name, string sex, string tel)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = new Salesman()
                {
                    Name = name,
                    Sex = sex,
                    Tel = tel,
                    Active = true
                };
                data.Id = context.Salesman.ToList().Count == 0 ? 1 : context.Salesman.ToList().Max(x => x.Id) + 1;//自动编号
                context.Salesman.Add(data);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 修改销售员信息
        /// </summary>
        /// <param name="id">销售员编号</param>
        /// <param name="name">销售员姓名</param>
        /// <param name="sex">销售员性别</param>
        /// <param name="tel">销售员电话</param>
        /// <param name="active">账户状态</param>
        public static void ChangeSalesman(int id, string name, string sex, string tel, bool active)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Salesman.First(x => x.Id == id);
                if (data == null)
                    throw new Exception("无此销售员，请检查销售员id");
                data.Name = name;
                data.Sex = sex;
                data.Tel = tel;
                data.Active = active;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除销售员
        /// 如果销售员已售车车则仅禁用账户，否则删除账户
        /// </summary>
        /// <param name="id">销售员编号</param>
        public static void DeleteSalesman(int id)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Salesman.First(x => x.Id == id);

                if (data == null)
                    throw new Exception("无此销售员，请检查销售员id");
                else if (data.Market != null)
                    data.Active = false;
                else
                    context.Salesman.Remove(data);

                context.SaveChanges();
            }
        }

    }
}
