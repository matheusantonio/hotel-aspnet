using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Hotel.Models;

namespace Hotel.Models
{
    public class ValoresService
    {
        public IConfiguration Configuration {get;}

        public ValoresService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<Valores> getValues()
        {
            string filePath = Configuration.GetValue<string>("Valores");

            Valores valores;

            if(File.Exists(filePath))
            {
                using(FileStream fs = File.OpenRead(filePath))
                {
                    valores = await JsonSerializer.DeserializeAsync<Valores>(fs);
                }
                
            } else {

                valores = new Valores()
                {
                    ValorCamaCasal = 0,
                    ValorCamaSolteiro = 0,
                    ValorBanheiro = 0,
                    ValorCafe = 0,
                    ValorInternet = 0,
                    ValorTv = 0,
                    PorcentagemPagamento = 0,
                    ValorBase = 0
                };
            }

            return valores;
        }

        public async Task setValues(Valores valores)
        {
            string filePath = Configuration.GetValue<string>("Valores");

            using(FileStream fs = File.Create(filePath))
            {
                await JsonSerializer.SerializeAsync(fs, valores);
            }

        }
    }
}