using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly HotelContext _context;

        public RelatorioController(HotelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<decimal> atualMensal = from v in _context.Reservas
                                                where v.DataEntrada.Month == DateTime.Today.Month
                                                select v.ValorPago;

            IEnumerable<decimal> futuroMensal = from v in _context.Reservas
                                                where v.DataEntrada.Month == DateTime.Today.Month
                                                select v.ValorTotal;
            
            IEnumerable<decimal> atualAnual = from v in _context.Reservas
                                                where v.DataEntrada.Year == DateTime.Today.Year
                                                select v.ValorPago;

            IEnumerable<decimal> futuroAnual = from v in _context.Reservas
                                                where v.DataEntrada.Year == DateTime.Today.Year
                                                select v.ValorTotal;

            RelatorioModelView relatorio = new RelatorioModelView()
            {
                ValorAtualMensal = atualMensal.Sum(),
                ValorFuturoMensal = futuroMensal.Sum(),
                ValorAtualAnual = atualAnual.Sum(),
                ValorFuturoAnual = futuroAnual.Sum()
            };

            return View(relatorio);

        }
    }

}