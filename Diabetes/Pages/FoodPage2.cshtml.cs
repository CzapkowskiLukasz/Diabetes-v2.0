using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Diabetes.Models;
using Diabetes.DAL;
using Microsoft.AspNetCore.Http;

namespace Diabetes.Pages
{
    public class FoodPage2Model : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public Food foodweight { get; set; }

        public Food food = new Food();

        InterfaceDB interfaceDB;
        public FoodPage2Model(InterfaceDB _interfaceDB)
        {
            interfaceDB = _interfaceDB;
        }
        public void OnGet()
        {
            food = interfaceDB.GetById(foodweight.id);
            food.calories = (int)(food.calories * foodweight.weight / 100);
            food.carbohydrates = Math.Round((food.carbohydrates * foodweight.weight) / 100);
            food.proteins = Math.Round((food.proteins * foodweight.weight) / 100);
            food.fats = Math.Round((food.fats * foodweight.weight) / 100);
            food.insulin = Math.Round(food.carbohydrates / 10 * foodweight.insulin, 2);
        }
        public IActionResult OnPost()
        {
            string old = HttpContext.Session.GetString("Plate");
            string result = foodweight.id.ToString() + ";" + foodweight.weight.ToString() + ";" + foodweight.insulin.ToString() + "/";
            HttpContext.Session.SetString("Plate", old + result);
            return RedirectToPage("List");
        }
    }
}
