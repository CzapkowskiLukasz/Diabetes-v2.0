using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Diabetes.Models;
using Diabetes.DAL;

namespace Diabetes.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        InterfaceDB interfaceDB;
        public DeleteModel(InterfaceDB _interfaceDB)
        {
            interfaceDB = _interfaceDB;
        }
        public IActionResult OnGet()
        {
            interfaceDB.Delete(id);
            return RedirectToPage("../List");
        }
    }
}
