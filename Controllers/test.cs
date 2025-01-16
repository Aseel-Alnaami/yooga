using Microsoft.AspNetCore.Mvc;

namespace yogago.Controllers
{
    public class test : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
