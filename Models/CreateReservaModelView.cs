using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel.Models
{
    public class CreateReservaModelView
    {
        public SelectList TodosQuartos {get; set;}
        
        [Required]
        public int NumeroQuarto {get; set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEntrada {get; set;}
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataSaida {get; set;}
        
        [Required]
        public bool IncluiCafe {get; set;}
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal ValorPago {get; set;}
    }
}