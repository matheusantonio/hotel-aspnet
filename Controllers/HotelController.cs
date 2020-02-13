using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}