using Microsoft.AspNetCore.Mvc;

namespace FrituurOpDeHoekMVC.Controllers
{
    public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OwnerLogin()
        {
            return View();
        }
        public IActionResult OwnerLoginCheck( string password, string username)
        {
            if(username == "Admin" &&  password == "Admin")
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("OwnerLogin");
        }
    }
}
