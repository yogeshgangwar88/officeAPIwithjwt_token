using Microsoft.AspNetCore.Mvc;
using Models.Models;
using RepoLibrary.DBContext;

namespace officeapi.Controllers
{
    public class NonAPIController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NonAPIController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        [Route("/NonAPIHome")]
        public IActionResult NonAPIHome()
        {
            ViewData["Title"] = "Add";
            return View(new Users());
        }
        [HttpPost]
        [Route("/NonAPIHome")]
        public async Task< IActionResult> NonAPIHome(Users user)
        {
            if (ModelState.IsValid) {
                _context.Users.Add(user);
                await   _context.SaveChangesAsync();
                TempData["success"] = "Add Successfully";
                return RedirectToAction("NonAPIHome");
            }
            else
            {
                TempData["error"] = "Add Failed";
                return View(user);
            }

        }
    }
}
