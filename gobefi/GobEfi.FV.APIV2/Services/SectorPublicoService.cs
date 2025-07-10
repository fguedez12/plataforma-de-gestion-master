using GobEfi.FV.APIV2.Models.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Services
{
    public class SectorPublicoService : ISectorPublicoService
    {
        static HttpClient _client = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly string _key;
        private readonly string _url;

        public SectorPublicoService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration.GetValue<string>("SectorPublico:url");
            _client.DefaultRequestHeaders.Remove("key");
        }

        public async Task<UserInfoDTO> GetUser(UserDTO userDTO)
        {
            var dataObj = new UserDTO { Email = userDTO.Email, Password = userDTO.Password }; 
            var json = JsonConvert.SerializeObject(dataObj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_url}/usuarios/validar", data);
            if (response.IsSuccessStatusCode)
            {
                var Body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UserInfoDTO>(Body);
                return result;
            }

            return new UserInfoDTO();
        }
    }
}
