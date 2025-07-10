using AutoMapper;
using GobEfi.FV.Shared.DTOs;
using GobEfi.FV.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GobEfi.FV.Demon
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Iniciando Proceso ");
            Console.WriteLine("Consultando datos a la api... ");
            var data = await CargarData();
            Console.WriteLine("Guardando datos en la DB ");
            await GuardarData(data);
        }

        private static async Task<List<List<ModeloDTO>>> CargarData()
        {
            var list = new List<List<ModeloDTO>>();
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, true)
               .Build();
            var key = configuration["ElectroMobilidad:key"];
            var url = configuration["ElectroMobilidad:url"];
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("key", key);
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<List<ModeloDTO>>>();

            }
            return list;
        }

        private static async Task GuardarData(List<List<ModeloDTO>> data)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ModeloEm, ModeloDTO>().ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            var listTosave = new List<ModeloEm>();
            using (var context = new ApplicationDbContext())
            {
                foreach (var lista in data)
                {
                    foreach (var item in lista)
                    {
                        var modeloFromDb = await context.Modelos.FirstOrDefaultAsync(x => x.IdEm == item.Id);
                       
                        if (modeloFromDb == null)
                        {
                            var modeloToSave = mapper.Map<ModeloEm>(item);
                            modeloToSave.IdEm = item.Id;
                            modeloToSave.Id = 0;
                            listTosave.Add(modeloToSave);
                        }
                        else 
                        {
                            Console.WriteLine("Actualizando registro Id:"+ modeloFromDb.Id);
                            context.Entry(modeloFromDb).State = EntityState.Detached;
                            var modeloToUpdate = mapper.Map<ModeloEm>(item);
                            modeloToUpdate.IdEm = item.Id;
                            modeloToUpdate.Id = modeloFromDb.Id;
                            context.Entry(modeloToUpdate).State = EntityState.Modified;
                        }
                    }

                }
                await context.AddRangeAsync(listTosave);
                await context.SaveChangesAsync();
            }
        }
    }
}
