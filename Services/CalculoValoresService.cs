using System.Threading.Tasks;

namespace Hotel.Models
{
    public class CalculoValoresService
    {
        private readonly ValoresService _pricing;

        public CalculoValoresService(ValoresService pricing)
        {
            _pricing = pricing;
        }

        public async Task<decimal> calcular(Reserva reserva)
        {
            Quarto quarto = reserva.quarto;

            decimal valorParcial = 0;

            Valores valores = await _pricing.getValues();

            int dias = reserva.DataSaida.Day - reserva.DataEntrada.Day;

            valorParcial += reserva.IncluiCafe ? valores.ValorCafe : 0;
            valorParcial += dias*valores.ValorBase;

            valorParcial += quarto.CamasSolteiro * valores.ValorCamaSolteiro;
            valorParcial += quarto.CamasCasal * valores.ValorCamaCasal;
            valorParcial += quarto.PossuiBanheiro ? valores.ValorBanheiro : 0;
            valorParcial += quarto.PossuiInternet ? valores.ValorInternet : 0;
            valorParcial += quarto.PossuiTv ? valores.ValorTv : 0;
            
            return valorParcial;
        }

    }
}