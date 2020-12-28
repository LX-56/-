using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomobileSalesSystem.Entitys;

namespace AutomobileSalesSystem.Models
{
    /// <summary>
    /// 用户业务模型
    /// </summary>
    public class ClientModel
    {
        /// <summary>
        /// 获得所有用户信息
        /// </summary>
        /// <returns>用户列表</returns>
        public static List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            using (var context = new car_sales_dbContext())
            {
                clients = context.Client.ToList();
                if (clients.Count == 0)
                {
                    throw new Exception("数据库中无用户信息");
                }
            }

            return clients;
        }

        /// <summary>
        /// 根据id查找用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>用户信息</returns>
        public static Client GetClientById(int id)
        {
            Client client = new Client();
            using (var context = new car_sales_dbContext())
            {
                client = context.Client.Where(x => x.Id == id).FirstOrDefault();
                if (client == null)
                {
                    throw new Exception("数据库中无此用户");
                }
            }

            return client;
        }

        /// <summary>
        /// 根据用户姓名查找用户（不唯一）
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <returns>用户列表</returns>
        public static List<Client> GetClientsByName(string name)
        {
            List<Client> clients = new List<Client>();
            using (var context = new car_sales_dbContext())
            {
                clients = context.Client.Where(x => x.Name == name).ToList();
                if (clients.Count == 0)
                {
                    throw new Exception("数据库中无此用户");
                }
            }

            return clients;
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <param name="sex">用户性别</param>
        /// <param name="tel">用户电话</param>
        public static void NewClient(string name, string sex, string tel)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = new Client()
                {
                    Name = name,
                    Sex = sex,
                    Tel = tel,
                    Active = true
                };
                data.Id = context.Client.ToList().Count == 0 ? 1 : context.Client.ToList().Max(x => x.Id) + 1;//自动编号
                context.Client.Add(data);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="name">用户姓名</param>
        /// <param name="sex">用户性别</param>
        /// <param name="tel">用户电话</param>
        /// <param name="active">账户状态</param>
        public static void ChangeClient(int id, string name, string sex, string tel,bool active)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Client.First(x => x.Id == id);
                if (data == null)
                    throw new Exception("无此用户，请检查用户id");
                data.Name = name;
                data.Sex = sex;
                data.Tel = tel;
                data.Active = active;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除用户
        /// 如果用户已购车则仅禁用账户，否则删除账户
        /// </summary>
        /// <param name="id">用户编号</param>
        public static void DeleteClient(int id)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Client.First(x => x.Id == id);
                if (data == null)
                    throw new Exception("无此用户，请检查用户id");
                else if (data.Market != null)
                    data.Active = false;
                else
                    context.Client.Remove(data);

                context.SaveChanges();
            }
        }
    }
}