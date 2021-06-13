using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Diabetes.Models;
using System.ComponentModel.DataAnnotations;
using Diabetes.DAL;
namespace Diabetes.Pages
{
    public class SearchModel : PageModel
    {
        public IConfiguration _configuration { get; }

        InterfaceDB interfaceDB;
        public SearchModel(InterfaceDB _interfaceDB)
        {
            interfaceDB= _interfaceDB;
        }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [BindProperty]
        public string name { get; set; }
        public Food food = new Food();

        [BindProperty]
        public double insu { get; set; }

        [Range(1,99999, ErrorMessage ="Wartość musi byc z przedziału 1-99999 g")]
        [BindProperty]
        public double weight { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            food = interfaceDB.GetByName(name);
            food.insulin = food.carbohydrates/10*insu;
            food.weight = weight;
            return RedirectToPage("FoodPage", food);
        }
    }
}
