using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightData;
using FlightData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace TightFlight.Controllers
{
    public class AuthUsersController : Controller
    {
        private readonly FlightContext _context;
        private SignInManager<AuthUser> _signManager;
        private UserManager<AuthUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public AuthUsersController(FlightContext context, UserManager<AuthUser> userManager, SignInManager<AuthUser> signManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
        }

        private bool AuthUserExists(string id)
        {
            return _context.AuthUsers.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new Login { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(Encryptor.Encrypt(model.Username),
                   Encryptor.Encrypt(model.Password), model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new AuthUser { UserName = Encryptor.Encrypt(model.Username) };
                var result = await _userManager.CreateAsync(user, Encryptor.Encrypt(model.Password));

                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await _roleManager.FindByNameAsync("User"); // make him an user
                    if (applicationRole != null)
                    {
                        await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
    }
}