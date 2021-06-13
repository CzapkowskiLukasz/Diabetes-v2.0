using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Diabetes.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Runtime;

namespace Diabetes.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public User user { get; set; }
        string login;
        string password;
        public string URL;
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
            login = _configuration.GetValue<string>("AppSettings:Login");
            password = _configuration.GetValue<string>("AppSettings:Passwd");
        }
        public void OnGet()
        {
        }

        private bool validation(User user)
        {
            if (login == user.userName)
            {
                var passwordHasher = new PasswordHasher<string>();
                if (passwordHasher.VerifyHashedPassword(login, password, user.password) == PasswordVerificationResult.Success)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (validation(user))
            {
                var claims = new List<Claim>()
                {
                    new Claim (ClaimTypes.Name, user.userName),
                    new Claim (ClaimTypes.Role, "RealAdmin")
                };


                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");

                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));

                return RedirectToPage("/Index");
            }

            return Page();
        }
        
    }
}
