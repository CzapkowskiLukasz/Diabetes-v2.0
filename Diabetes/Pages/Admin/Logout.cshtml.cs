using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diabetes.Pages.Admin
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {

            await HttpContext.SignOutAsync("CookieAuthentication");
            return this.RedirectToPage("/index");
        }
    }
}
