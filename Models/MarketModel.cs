using AutomobileSalesSystem.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomobileSalesSystem.Models
{
    /// <summary>
    /// 车辆销售业务模型
    /// </summary>
    public class MarketModel
    {
        /// <summary>
        /// 获得所有销售记录
        /// </summary>
        /// <returns>销售记录列表</returns>
        public static List<Market> GetAllMarkets()
        {
            List<Market> markets = new List<Market>();

            using(var context = new car_sales_dbContext())
            {
                markets = context.Market.ToList();
                if (markets.Count == 0)
                    throw new Exception("数据库中无销售记录");
            }

            return markets;
        }

        /// <summary>
        /// 根据订单号查找销售记录
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <returns>销售列表</returns>
        public static List<Market> GetMarketsById(int id)
        {
            List<Market> markets = new List<Market>();

            using (var context = new car_sales_dbContext())
            {
                markets = context.Market.Where(x => x.Id == id).ToList();
                if (markets.Count == 0)
                    throw new Exception("数据库中无此销售记录");
            }

            return markets;

        }

        /// <summary>
        /// 根据用户查找销售记录
        /// </summary>
        /// <param name="cid">用户编号</param>
        /// <returns>销售列表</returns>
        public static List<Market> GetMarketsByClient(int cid)
        {
            List<Market> markets = new List<Market>();

            using (var context = new car_sales_dbContext())
            {
                markets = context.Market.Where(x=>x.Cid == cid).ToList();
                if (markets.Count == 0)
                    throw new Exception("数据库中无有关此用户的销售记录");
            }

            return markets;

        }

        /// <summary>
        /// 根据销售员查找销售记录
        /// </summary>
        /// <param name="sid">销售员编号</param>
        /// <returns>销售列表</returns>
        public static List<Market> GetMarketsBySalesman(int sid)
        {
            List<Market> markets = new List<Market>();

            using (var context = new car_sales_dbContext())
            {
                markets = context.Market.Where(x => x.Sid == sid).ToList();
                if (markets.Count == 0)
                    throw new Exception("数据库中无有关此销售员的销售记录");
            }

            return markets;

        }

        /// <summary>
        /// 根据车辆查找销售记录
        /// </summary>
        /// <param name="aid">车辆编号</param>
        /// <returns>销售信息</returns>
        public static Market GetMarketsByCar(int aid)
        {
            Market market = new Market();

            using (var context = new car_sales_dbContext())
            {
                market = context.Market.First(x => x.Aid == aid);
                if (market == null)
                    throw new Exception("数据库中无有关此车辆的销售记录");
            }

            return market;

        }

        /// <summary>
        /// 车辆销售
        /// </summary>
        /// <param name="cid">用户编号</param>
        /// <param name="sid">销售员编号</param>
        /// <param name="aid">车辆编号</param>
        public static void Sell(int cid,int sid,int aid)
        {
            using(var context = new car_sales_dbContext())
            {
                if (context.Market.Where(x => x.Aid == aid).ToList().Count == 0)
                    throw new Exception("该车辆已经被出售，无法再次销售");
                if (context.Car.Where(x => x.Id == aid).ToList().Count == 0)
                    throw new Exception("该车辆不存在，无法销售");
                if (context.Client.Where(x => x.Id == cid).ToList().Count == 0)
                    throw new Exception("该用户不存在，无法销售");
                if (context.Salesman.Where(x => x.Id == sid).ToList().Count == 0)
                    throw new Exception("该销售员不存在，无法销售");

                var data = new Market()
                {
                    Cid = cid,
                    Sid = sid,
                    Aid = aid
                };
                data.Id = context.Market.ToList().Count == 0 ? 1 : context.Market.ToList().Max(x => x.Id) + 1;//自动编号
                data.Saledatatime = DateTime.Now;
                context.Market.Add(data);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 修改订单信息
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <param name="cid">用户编号</param>
        /// <param name="sid">销售员编号</param>
        /// <param name="aid">车辆编号</param>
        /// <param name="time">订单时间</param>
        public static void Change(int id, int cid, int sid, int aid, DateTime time)
        {
            using (var context = new car_sales_dbContext())
            {
                if (context.Market.Where(x => x.Id == id).ToList().Count == 0)
                    throw new Exception("不存在该订单");
                if (context.Market.Where(x => x.Aid == aid).ToList().Count == 0)
                    throw new Exception("该车辆已经被出售，无法再次销售");
                if (context.Car.Where(x => x.Id == aid).ToList().Count == 0)
                    throw new Exception("该车辆不存在，无法销售");
                if (context.Client.Where(x => x.Id == cid).ToList().Count == 0)
                    throw new Exception("该用户不存在，无法销售");
                if (context.Salesman.Where(x => x.Id == sid).ToList().Count == 0)
                    throw new Exception("该销售员不存在，无法销售");

                var data = context.Market.Where(x => x.Id == id).FirstOrDefault();
                data.Cid = cid;
                data.Sid = sid;
                data.Aid = aid;
                data.Saledatatime = time;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// 退货
        /// </summary>
        /// <param name="id">订单id</param>
        public static void Refund(int id)
        {
            using (var context = new car_sales_dbContext())
            {
                var data = context.Market.First(x => x.Id == id);
                if (data == null)
                    throw new Exception("无此订单，请检查订单id");
                else
                    context.Market.Remove(data);

                context.SaveChanges();
            }
        }
    }
}
