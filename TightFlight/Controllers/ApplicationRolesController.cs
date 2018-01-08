using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightData;
using FlightData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace TightFlight.Controllers
{
    public class ApplicationRolesController : Controller
    {
        private readonly FlightContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRolesController(FlightContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            List<ApplicationRoleListViewModel> model = new List<ApplicationRoleListViewModel>();
            model = _roleManager.Roles.Select(r => new ApplicationRoleListViewModel
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.Description = applicationRole.Description;
                }
            }
            return View("AddEditApplicationRole", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, ApplicationRoleViewModel model)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);
                ApplicationRole applicationRole = isExist ? await _roleManager.FindByIdAsync(id) :
               new ApplicationRole
               {
                   CreatedDate = DateTime.UtcNow
               };
                applicationRole.Name = model.RoleName;
                applicationRole.Description = model.Description;
                applicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                IdentityResult roleRuslt = isExist ? await _roleManager.UpdateAsync(applicationRole)
                                                    : await _roleManager.CreateAsync(applicationRole);
                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    name = applicationRole.Name;
                }
            }
            return View("DeleteApplicationRole", name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApplicationRole(string id, IFormCollection form)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null && applicationRole.Name != "Admin" && applicationRole.Name != "User")
                {
                    IdentityResult roleRuslt = _roleManager.DeleteAsync(applicationRole).Result;
                    if (roleRuslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}
