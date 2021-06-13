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
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Food food { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Pole obowiązkowe.")]
        [RegularExpression("([0-9]*[,])?[0-9]+", ErrorMessage = "Wartość musi byc z przedziału 0 - 99999 ")]
        public string carbohydrates { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Pole obowiązkowe.")]
        [RegularExpression("([0-9]*[,])?[0-9]+", ErrorMessage = "Wartość musi byc z przedziału 0 - 99999 ")]
        public string proteins { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Pole obowiązkowe.")]
        [RegularExpression("([0-9]*[,])?[0-9]+", ErrorMessage = "Wartość musi byc z przedziału 0 - 99999 ")]
        public string fats { get; set; }

        InterfaceDB interfaceDB;

        public List<Category> categories;

        [BindProperty]
        public int maincat { get; set; }

        [BindProperty]
        public int seccat { get; set; }

        public CreateModel(InterfaceDB _interfaceDB)
        {
            interfaceDB = _interfaceDB;
        }
        public void OnGet()
        {
            categories = interfaceDB.Categories();
        }
        public IActionResult OnPost()
        {
            food.carbohydrates = Convert.ToDouble(carbohydrates);
            food.proteins = Convert.ToDouble(proteins);
            food.fats = Convert.ToDouble(fats);
            food.glycemicLoad = (int)(food.glycemicIndex * food.carbohydrates / 100);
            food.category1.id = maincat;
            food.category2.id = seccat;
            interfaceDB.Add(food);
            return RedirectToPage("../List");
        }
    }
}
