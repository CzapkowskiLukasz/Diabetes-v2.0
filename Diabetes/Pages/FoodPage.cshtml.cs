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
    public class FoodPageModel : PageModel
    {
        [BindProperty]
        public double insu { get; set; }

        [Range(1, 99999, ErrorMessage = "Wartość musi byc z przedziału 1-99999 g")]
        [Required(ErrorMessage ="Pole obowiązkowe")]
        [BindProperty]
        public double weight { get; set; }


        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        public Food foodweight = new Food();

        InterfaceDB interfaceDB;
        public FoodPageModel(InterfaceDB _interfaceDB)
        {
            interfaceDB = _interfaceDB;
        }
        public void OnGet()
        {
            foodweight = interfaceDB.GetById(id);
        }
        public IActionResult OnPost()
        {
            foodweight = interfaceDB.GetById(id);
            foodweight.id = id;
            foodweight.insulin = insu;
            foodweight.weight = weight;
            return RedirectToPage("FoodPage2", foodweight);
        }
    }
}
