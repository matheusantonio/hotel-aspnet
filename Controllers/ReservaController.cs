using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class ReservaController : Controller
    {
        private readonly HotelContext _context;
        private readonly CalculoValoresService _pricing;
        private readonly ValoresService _values;

        public ReservaController(HotelContext context,
                                 CalculoValoresService valoresService,
                                 ValoresService valores)
        {
            _context = context;
            _pricing = valoresService;
            _values = valores;
        }

        public IActionResult Index()
        {
            var reservas = from r in _context.Reservas
                                select r;

            return View(reservas);
        }

        public async Task<IActionResult> Create()
        {
            IQueryable<int> quartosQuery = from q in _context.Quartos
                            select q.Numero;
            
            var reservasViewModel = new CreateReservaModelView{
                TodosQuartos = new SelectList(await quartosQuery.ToListAsync())
            };

            return View(reservasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroQuarto,DataEntrada,DataSaida,IncluiCafe,ValorPago")] CreateReservaModelView reservaModelView)
        {
            if(ModelState.IsValid &&
                validarPeriodoReserva(reservaModelView.DataEntrada,
                                      reservaModelView.DataSaida,
                                      reservaModelView.NumeroQuarto)
              )
            {
                Quarto QuartoReservado = await _context.Quartos.FirstOrDefaultAsync(q => q.Numero == reservaModelView.NumeroQuarto);
                
                if(QuartoReservado != null)
                {
                    Reserva reserva = new Reserva(){
                        DataEntrada = reservaModelView.DataEntrada,
                        DataSaida = reservaModelView.DataSaida,
                        quarto = QuartoReservado,
                        IncluiCafe = reservaModelView.IncluiCafe,
                        ValorPago = reservaModelView.ValorPago
                    };
                    reserva.ValorTotal = await _pricing.calcular(reserva);

                    _context.Add(reserva);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(reservaModelView);
        }

        private bool validarPeriodoReserva(DateTime dataEntrada, DateTime dataSaida, int numeroQuarto)
        {
            return true;
        }

        public async Task<IActionResult> Pay(string Id)
        {
            var reserva = await _context.Reservas.FindAsync(Id);

            PayModelView payment = new PayModelView{
                reserva = reserva,
                valor = 0
            };

            return View(payment);
        }

        [HttpPost, ActionName("Pay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmPay(PayModelView pagamento)
        {
            Reserva reserva = await _context.Reservas.FindAsync(pagamento.reserva.Id);
            Valores valores = await _values.getValues();

            if(reserva.realizarPagamento(pagamento.valor, valores.PorcentagemPagamento))
            {
                _context.Reservas.Update(reserva);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Pay), "Reserva", pagamento.reserva.Id);
        }
    
        public async Task<IActionResult> Cancel(string Id)
        {
            var reserva = await _context.Reservas.FindAsync(Id);

            return View(reserva);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCancel(string Id)
        {
            var reserva = await _context.Reservas.FindAsync(Id);
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    
    }
}