using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class ValidationController : Controller 
    {
        private readonly ValoresService _valores;
        private readonly HotelContext _context;

        public ValidationController(HotelContext context, ValoresService valoresService)
        {
            _context = context;
            _valores = valoresService;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerificarValorPago(decimal valor, string Id)
        {
            Reserva reserva = await _context.Reservas.FindAsync(Id);

            Valores valores = await _valores.getValues();

            if((valor + reserva.ValorPago)/reserva.ValorTotal < valores.PorcentagemPagamento)
            {
                return Json($"O valor pago total deve ser pelo menos superior a {valores.PorcentagemPagamento * 100}% do valor total.");
            }

            if((valor + reserva.ValorPago) > reserva.ValorTotal)
            {
                return Json("O valor pago total não pode exceder o valor total da reserva.");
            }

            return Json(true);

            
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarDataEntrada(DateTime DataEntrada)
        {
            if(DataEntrada < DateTime.Today)
            {
                return Json("A data de entrada deve ser a partir de hoje.");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarDatas(DateTime DataSaida, DateTime DataEntrada)
        {
            if(DataSaida < DataEntrada)
            {
                return Json("A data de saída deve ser após a data de entrada.");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarDisponibilidadeQuarto(int NumeroQuarto, DateTime DataEntrada, DateTime DataSaida)
        {
            var reservas = from r in _context.Reservas
                            where r.quarto.Numero == NumeroQuarto
                            && ((r.DataEntrada < DataEntrada && r.DataSaida > DataEntrada)
                            || (r.DataEntrada > DataEntrada && r.DataSaida < DataSaida)
                            || (r.DataEntrada < DataSaida && r.DataSaida > DataSaida))
                            select r;
            
            if(reservas.Any())
            {
                return Json("O quarto está indisponível para o período selecionado.");
            }

            return Json(true);

        }
    }
}