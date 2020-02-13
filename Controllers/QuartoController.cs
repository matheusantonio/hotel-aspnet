using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class QuartoController : Controller
    {
        private readonly HotelContext _context;

        public QuartoController(HotelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var quartos = from q in _context.Quartos
                                 select q;
            
            return View(quartos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,CamasCasal,CamasSolteiro,PossuiBanheiro,PossuiInternet,PossuiTv")] Quarto quarto)
        {
            if(ModelState.IsValid)
            {
                _context.Add(quarto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quarto);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var quarto = await _context.Quartos.FindAsync(Id);

            return View(quarto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Numero,CamasCasal,CamasSolteiro,PossuiBanheiro,PossuiInternet,PossuiTv")] Quarto quarto)
        {
            if(ModelState.IsValid)
            {
                _context.Quartos.Update(quarto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(quarto);

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var quarto = await _context.Quartos.FindAsync(Id);

            return View(quarto);
        }

        [HttpPost, Route("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int numero)
        {
            Quarto quarto = await _context.Quartos.FindAsync(numero);

            _context.Quartos.Remove(quarto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}