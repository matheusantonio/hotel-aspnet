using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {get; set;}
        
        [Display(Name="Data de Entrada")]
        [DataType(DataType.Date)]
        public DateTime DataEntrada {get; set;}
        
        [Display(Name="Data de Saída")]
        [DataType(DataType.Date)]
        public DateTime DataSaida {get; set;}
        [Display(Name="Café da manhã")]
        public bool IncluiCafe {get; set;}
        
        [ForeignKey("NumeroQuarto")]
        public virtual Quarto quarto {get; set;}
        
        [Display(Name="Checkin")]
        public bool FezCheckin {get; set;}
        
        [Display(Name="Valor Total")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal {get; set;}
        
        [Display(Name="Valor Pago")]
        [DataType(DataType.Currency)]
        public decimal ValorPago {get; set;}

        public bool realizarPagamento(decimal valor, decimal porcentagem)
        {
            if((valor + ValorPago)/ValorTotal >= porcentagem)
            {
                this.ValorPago += valor;
                return true;
            }
            return false;
        }
    }
}