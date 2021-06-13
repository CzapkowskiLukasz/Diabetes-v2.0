using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Diabetes.Models;
namespace Diabetes.Pages
{
    public class AddMealModel : PageModel
    {
        public List<Food> lista = new List<Food>();
        [BindProperty]
        public string name { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Food food = new Food();
            food.name = name;
            
            return Page();
        }
    }
}
