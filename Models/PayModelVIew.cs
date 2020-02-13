using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class PayModelView
    {
        public Reserva reserva {get; set;}

        [DataType(DataType.Currency)]
        public decimal valor {get; set;}
    }
}