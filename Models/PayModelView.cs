using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Models
{
    public class PayModelView
    {
        public Reserva reserva {get; set;}

        [Display(Name="Id")]
        public string Id {get; set;}

        [DataType(DataType.Currency)]
        [Remote(action: "VerificarValorPago", controller: "Validation", AdditionalFields = nameof(Id))]
        public decimal valor {get; set;}
    }
}