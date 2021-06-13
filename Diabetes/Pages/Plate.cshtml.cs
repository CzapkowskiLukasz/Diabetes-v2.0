using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Diabetes.Models;
using Diabetes.DAL;

namespace Diabetes.Pages
{
    public class PlateModel : PageModel
    {
        public int settings = 1;
        public string session;

        public List<Food> foodlist = new List<Food>();
        InterfaceDB interfaceDB;
        public PlateModel(InterfaceDB _interfaceDB)
        {
            interfaceDB = _interfaceDB;
        }
        public void OnGet()
        {
           session = HttpContext.Session.GetString("Plate");
            if (session == null)
                settings = 0;
            else
                foodlist = Tranfsorm(session);
            
        }
        public List<Food> Tranfsorm(string session)
        {
            string[] strings = session.Split('/');
            List<string> liststring = new List<string>();
            List<Food> listfood = new List<Food>();
            foreach(string x in strings)
            {
                liststring.Add(x);
            }
            for(int i=0; i < liststring.Count -1 ; i++)
            {
                string[] strings2 = liststring[i].Split(';');
                Food food = new Food();
                food = interfaceDB.GetById(int.Parse(strings2[0]));
                food.weight = int.Parse(strings2[1]);
                food.insulin = int.Parse(strings2[2]);
                listfood.Add(food);
            }
            return listfood;
        }
    }
}
