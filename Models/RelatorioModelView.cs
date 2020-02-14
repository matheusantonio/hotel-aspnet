using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class RelatorioModelView
    {
        [Display(Name="Valor atual anual")]
        public decimal ValorAtualAnual {get; set;}

        [Display(Name="Valor atual mensal")]
        public decimal ValorAtualMensal {get; set;}

        [Display(Name="Valor futuro anual")]
        public decimal ValorFuturoAnual {get; set;}

        [Display(Name="Valor futuro mensal")]
        public decimal ValorFuturoMensal {get; set;}
    }
}