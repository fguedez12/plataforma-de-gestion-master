using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.Shared.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Services
{
    public class ElectroMobilidadService : IElectroMobilidadService
    {
        static HttpClient _client = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly string _key;
        private readonly string _url;


        public ElectroMobilidadService(IConfiguration configuration )
        {
            _configuration = configuration;
            _key = _configuration.GetValue<string>("ElectroMobilidad:key");
            _url = _configuration.GetValue<string>("ElectroMobilidad:url");
            _client.DefaultRequestHeaders.Remove("key");
        }
       

        public async Task<List<string>> GetMarca()
        {
            return await getData(_url + "/marca");
        }
        public async Task<List<string>> GetCarroceria()
        {
            return await getData(_url + "/carroceria");
        }
        public async Task<List<string>> GetPropulsion()
        {
            return await getData(_url + "/propulsion");
        }

        public async Task<List<ModeloDTO>> BuscarModelo(BuscarModeloDTO buscar)
        {
           
            //_client.DefaultRequestHeaders.Add("key", _key);
            HttpResponseMessage response = await _client.GetAsync($"{_url}/buscar/{buscar.Marca}/{buscar.Propulsion}/{buscar.Carroceria}");
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsAsync<List<ModeloDTO>>();

                return list.OrderBy(x=>x.Modelo).ToList();
            }

            return new List<ModeloDTO>();
        }

        private async Task<List<string>> getData(string url)
        {
            _client.DefaultRequestHeaders.Remove("key");
            _client.DefaultRequestHeaders.Add("key", _key);
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var Body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<string>>(Body);
                return result.OrderBy(x => x).ToList();
            }

            return new List<string>();
        }

    }
}
