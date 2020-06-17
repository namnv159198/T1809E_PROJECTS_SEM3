using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationDbContext context = new ApplicationDbContext();
        public RolesController()
        {

        }
        public RolesController(ApplicationRoleManager roleManage, ApplicationUserManager userManager)
        {
            RoleManager = roleManage;
            UserManager = userManager;
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }

            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }

            private set
            {
                _userManager = value;
            }
        }
        // GET: Roles
        public ActionResult Index()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole() { Name = model.Name };
                await RoleManager.CreateAsync(role);
                TempData["message"] = "Create";
                return RedirectToAction("Index");
            }
            else TempData["message"] = "Fail";
            return View(model);
        }
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string id, string name)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (ModelState.IsValid)
            {
                if (role != null)
                {
                    role.Name = name;
                    await RoleManager.UpdateAsync(role);
                    TempData["message"] = "Edit";
                    return RedirectToAction("Index");
                }
                else { TempData["message"] = "Fail"; }
            }
            return View();
        }

    }
}