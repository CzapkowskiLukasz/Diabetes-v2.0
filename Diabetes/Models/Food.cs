using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Diabetes.Models
{
    public class Food
    {
        public int id { get; set; }


        [Display(Name = "Nazwa")]
        [Required(ErrorMessage ="Pole obowiązkowe.")]
        public string name { get; set; }


        [Required(ErrorMessage = "Pole obowiązkowe.")]
        [Range(0,99999, ErrorMessage = "Wartość musi byc liczbą całkowitą z przedziału 0 - 99999" )]
        [Display(Name = "Kalorie")]
        public int calories { get; set; }


        public double carbohydrates { get; set; }


        public double proteins { get; set; }


        public double fats { get; set; }


        [Display(Name = "Indeks glikemiczny")]
        [Required(ErrorMessage = "Pole obowiązkowe.")]
        [Range(0, 99999, ErrorMessage = "Wartość musi byc liczbą całkowitą z przedziału 0 - 99999")]
        public int glycemicIndex { get; set; }


        [Display(Name = "Ładunek glikemiczny")]
        [Required(ErrorMessage = "Pole obowiązkowe.")]
        [Range(0, 99999, ErrorMessage = "Wartość musi byc liczbą całkowitą z przedziału 0 - 99999")]
        public int glycemicLoad { get; set; }



        public Category category1 = new Category();

        public Category category2 = new Category();



        public double insulin { get; set; }

        public double weight { get; set; }


        public bool IsNull(Category category)
        {
            if (category == null)
                return true;
            else
                return false;
        }
    }
}
