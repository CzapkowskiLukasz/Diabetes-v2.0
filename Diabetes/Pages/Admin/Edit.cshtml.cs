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
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }

        [BindProperty]
        public Food food { get; set; }

        [BindProperty]
        public Food foodold { get; set; }

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


        public List<Category> categories;


        [BindProperty]
        public int maincat { get; set; }

        [BindProperty]
        public int seccat { get; set; }

        InterfaceDB interfaceDB;
        public EditModel(InterfaceDB _interfaceDB)
        {
            interfaceDB = _interfaceDB;
        }
        public void OnGet()
        {
            foodold = interfaceDB.GetById(id);
            categories = interfaceDB.Categories();
        }
        public IActionResult OnPost()
        {
            foodold.carbohydrates = Convert.ToDouble(carbohydrates);
            foodold.proteins = Convert.ToDouble(proteins);
            foodold.fats = Convert.ToDouble(fats);
            foodold.glycemicLoad = (int)(foodold.glycemicIndex * foodold.carbohydrates / 100);
            foodold.id = id;
            foodold.category1.id = maincat;
            foodold.category2.id = seccat;
            interfaceDB.Edit(foodold);
            return RedirectToPage("../List");
        }
    }
}
