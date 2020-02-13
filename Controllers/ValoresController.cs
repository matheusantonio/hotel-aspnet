using Microsoft.AspNetCore.Mvc;
using Hotel.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Controllers
{
    public class ValoresController : Controller
    {
        private readonly HotelContext _context;
        private readonly ValoresService _pricing;

        public ValoresController(HotelContext context, ValoresService valores)
        {
            _context = context;
            _pricing = valores;
        }

        public async Task<IActionResult> Index()
        {
            var valores = await _pricing.getValues();

            return View(valores);
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(Valores valores)
        {
            await _pricing.setValues(valores);
            
            return RedirectToAction(nameof(Index), "Hotel");
        }
    }
}