using System;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.Models
{
    public class User
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
