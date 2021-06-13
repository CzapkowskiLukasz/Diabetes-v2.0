using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Diabetes.Models;
using Diabetes.DAL;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.Pages
{
    public class ListModel : PageModel
    {
        public List<Food> foodlist = new List<Food>();
        InterfaceDB interfaceDB;
        [Range(1,99999,ErrorMessage = "Wartość musi byc z przedziału 1-99999 g")]
        public double weight { get; set; }
        public ListModel(InterfaceDB _interfaceDB)
        {
            interfaceDB = _interfaceDB;
        }
        public void OnGet()
        {
            foodlist = interfaceDB.List();
        }
    }
}
