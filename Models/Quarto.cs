using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Quarto
    {
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int Numero {get; set;}
        
        [Display(Name="Camas de Solteiro")]
        [Required]
        [Range(0, 4)]
        public int CamasSolteiro {get; set;}

        [Display(Name="Camas de Casal")]
        [Required]
        [Range(0, 4)]
        public int CamasCasal {get; set;}

        [Display(Name="Banheiro")]
        [Required]
        public bool PossuiBanheiro {get; set;}

        [Display(Name="Internet")]
        [Required]
        public bool PossuiInternet {get; set;}

        [Display(Name="Televisão")]
        [Required]
        public bool PossuiTv {get; set;}

    }
}