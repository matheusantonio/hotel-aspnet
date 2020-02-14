using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel.Models
{
    public class CreateReservaModelView
    {
        public SelectList TodosQuartos {get; set;}
        
        [Required]
        [Remote(action:"VerificarDisponibilidadeQuarto", controller: "Validation", AdditionalFields = nameof(DataEntrada) + "," + nameof(DataSaida))]
        public int NumeroQuarto {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [Remote(action:"VerificarDataEntrada", controller: "Validation")]
        public DateTime DataEntrada {get; set;} = DateTime.Now;
        
        [Required]
        [DataType(DataType.Date)]
        [Remote(action:"VerificarDatas", controller:"Validation", AdditionalFields = nameof(DataEntrada))]
        public DateTime DataSaida {get; set;} = DateTime.Now;
        
        [Required]
        public bool IncluiCafe {get; set;}
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal ValorPago {get; set;}
    }
}