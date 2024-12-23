using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeEeTDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        UserManager um = new UserManager(new EfUserRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User p)
        {
            UserValidator uv = new UserValidator();
            ValidationResult results = uv.Validate(p);
            if (results.IsValid)
            {
                p.CreatedAt = DateTime.Now;
                um.TAdd(p);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }
            return View();
        }
    }
}
