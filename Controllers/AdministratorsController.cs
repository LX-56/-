using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomobileSalesSystem.Models;
using AutomobileSalesSystem.Entitys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace AutomobileSalesSystem.Controllers
{
    public class AdministratorsController : Controller
    {
     
        // GET: Administrators
        public IActionResult salesman_search()
        {
            return View();
        }

        public IActionResult salesman_information([FromQuery] int id = 0, string name = null, string sex = null, string tel = null,int active = 0)
        {
            if (id == 0 && name != null)
            {
                SalesmanModel.NewSalesman(name, sex, tel);
            }
            else if(id != 0 && name != null)
            {
                SalesmanModel.ChangeSalesman(id, name, sex, tel, active == 1 ? true:false);
            }

            this.ViewData["salesman_inf"] = SalesmanModel.GetAllSalesman();

            if (id != 0 && name == null)
            {
                var list = new List<Salesman>();
                list.Add(SalesmanModel.GetSalesmanById(id));
                this.ViewData["salesman_inf"] = list;
            }

            return View();
        }

        public IActionResult clients_search()
        {
            return View();
        }
        public IActionResult clients_information([FromQuery] int id = 0, string name = null, string sex = null, string tel = null, int active = 0)
        {
            if (id == 0 && name != null)
            {
                ClientModel.NewClient(name, sex, tel);
            }
            else if (id != 0 && name != null)
            {
                ClientModel.ChangeClient(id, name, sex, tel, active == 1 ? true : false);
            }

            this.ViewData["client_inf"] = ClientModel.GetAllClients();

            if (id != 0 && name == null)
            {
                var list = new List<Client>();
                list.Add(ClientModel.GetClientById(id));
                this.ViewData["client_inf"] = list;
            }

            return View();
        }

        public IActionResult sales_information()
        {
            this.ViewData["sales_inf"] = MarketModel.GetAllMarkets();
            return View();
        }
      
        public IActionResult appointment_drive()
        {
            this.ViewData["appoint_inf"] = DrivetestModel.GetAllDrivetests();

            return View();
        }
        public IActionResult information_import()
        {
            return View();
        }

        public IActionResult car_search()
        {
            return View();
        }
        public IActionResult car_information()
        {
            this.ViewData["car_inf"] = CarModel.GetAllCars();
            return View();
        }
        public IActionResult statistics()
        {
            List<Market> market = new List<Market>();
            int[] month = new int[12];
            for (int i = 0; i < 12; i++)
            {
                month[i] = 0;
            }
            int car = 0;
            int suv = 0;
            using (var context = new car_sales_dbContext())
            {
                market = context.Market.ToList();
                if (market.Count == 0)
                {
                    throw new Exception("数据库中无销售信息");
                }
                foreach (var item in market)
                {
                    if (context.Car.Where(x => x.Id == item.Cid).First().Type == "SUV")
                    {
                        suv += 1;
                    }
                    else
                    {
                        car += 1;
                    }
                    month[item.Saledatatime.Month - 1] += 1;
                }
            }
            this.ViewData["car"] = car;
            this.ViewData["suv"] = suv;
            this.ViewData["month"] = month;
            return View();
        }

        public IActionResult orders_search()
        {
            return View();
        }
        public IActionResult orders_information([FromQuery]int id = 0, int cid = 0,int aid = 0,int sid = 0,DateTime time =  default(DateTime))
        {
            if (id == 0 && cid != 0)
            {
                MarketModel.Sell(cid, sid, aid);
            }
            else if (id != 0 && cid != 0)
            {
                MarketModel.Change(id, cid, sid, aid, time);
            }

            this.ViewData["orders_inf"] = MarketModel.GetAllMarkets();

            if (id != 0 && cid == 0)
            {
                this.ViewData["orders_inf"] = MarketModel.GetMarketsById(id);
            }

            return View();
        }



    }
}