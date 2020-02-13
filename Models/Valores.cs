using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class Valores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {get; set;}
        public decimal ValorCamaSolteiro {get; set;}
        public decimal ValorCamaCasal {get; set;}
        public decimal ValorBanheiro {get; set;}
        public decimal ValorInternet {get; set;}
        public decimal ValorTv {get; set;}
        public decimal ValorCafe {get; set;}
        public decimal PorcentagemPagamento {get; set;}
        public decimal ValorBase {get; set;}
    }
}