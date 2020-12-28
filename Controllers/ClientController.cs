using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomobileSalesSystem.Entitys;
using AutomobileSalesSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomobileSalesSystem.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult client_search()
        {
           
            return View();

        }
        public IActionResult client_information([FromQuery] int id = 0, string name = null, string sex = null, string tel = null, int active = 0)
        {
            if (id != 0 && name != null)
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

        public IActionResult order_search( )
        {
            return View();
        }
        public IActionResult order_information([FromQuery] int id = 0)
        {
            if (id != 0)
            {
                this.ViewData["orders_inf"] = MarketModel.GetMarketsById(id);
            }
            else
            {
                this.ViewData["orders_inf"] = MarketModel.GetAllMarkets();
            }

            return View();
        }
        public IActionResult test_drive( )
        {
           
            return View();
        }
        public IActionResult appointment([FromQuery] int id)
        {
            this.ViewData["appoint_inf"] = DrivetestModel.GetDrivetestsByClient(id);
            return View();
        }
       

    }
}
