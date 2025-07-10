using api_gestiona.DTOs.MedicionInteligente;
using api_gestiona.DTOs.MedicionInteligente.ApiDTO;
using api_gestiona.DTOs.MedicionInteligente.ResponseDTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;

namespace api_gestiona.Services
{
    public class MedicionInteligenteService : IMedicionInteligenteService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private string url;
        private string key;

        public MedicionInteligenteService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
            url = _configuration["apichilemedido:url"];
            key = _configuration["apichilemedido:key"];
        }
        public async Task<List<MedidoresDTO>> GetMedidores(long id)
        {
            long unidadId = await GetAsociateId(id);
            var listMedidores = new List<MedidoresDTO>();
            var urlstr = $"{url}/devices?unidadId={unidadId}";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", key);
                var response = await httpClient.GetAsync(urlstr);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    listMedidores = System.Text.Json.JsonSerializer.Deserialize<List<MedidoresDTO>>(stringResponse,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            return listMedidores;
        }

        public async Task<ApiResponseDTO> GetMediciones(long id,DateTime startTime,DateTime endTime,string frecuencia,List<long> medidoresId = null,int variableId=3,string aggregationType="SUM_DIFF",string aggregationResul = "")
        {
            //long unidadId = await GetAsociateId(id);
            if (startTime == endTime)
            {
                startTime = startTime.AddDays(-1);
            }

            startTime = setDateFormat(startTime, frecuencia);
            endTime = setDateFormat(endTime, frecuencia);
            long lStartTime = DtToTs(startTime);
            long lEndTime = DtToTs(endTime);
            var responseList = new ApiResponseDTO();

            var medidores = medidoresId == null ? await GetMedidoresString(id) : string.Join(",",medidoresId);
            var urlstr = $"{url}/measurements?deviceIds={medidores}&variableIds={variableId}&startDate={lStartTime}&endDate={lEndTime}&aggregationType={aggregationType}&frequency={frecuencia}{aggregationResul}";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", key);
                var response = await httpClient.GetAsync(urlstr);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    var responseObj = JsonConvert.DeserializeObject<Dictionary<string,DeviceDTO>>(stringResponse);
                    if (responseObj != null)
                    {
                        responseList = await MapApiObj(id, responseObj,frecuencia);
                    }
                }
            }
            return responseList;
        }

        private DateTime setDateFormat(DateTime date, string frecuency)
        {

            switch (frecuency)
            {
                case "1M":
                   return date = new DateTime(date.Year, date.Month, 1);
                case "1d":
                    return date = new DateTime(date.Year, date.Month, date.Day);
                case "1h":
                    return date = new DateTime(date.Year, date.Month, date.Day,date.Hour,0,0);
                case "15m":
                    return date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
                default:
                    return date;
            }

           
        }

        public async Task<ApiResponseDTO> GetMedicionesDefault(long id)
        {
            var response = await GetMediciones(id,DateTime.Now.AddYears(-1),DateTime.Now,"1M");
            return response;
            var x = "";
        }

        public async Task<ApiResponseDTO> GetMedicionesDefaultPot(long id)
        {
            var response = await GetMediciones(id, DateTime.Now.AddYears(-1), DateTime.Now, "1M",null,1,"AVERAGE", "&aggregationResult=DEVICE");
            return response;
        }


        public async Task<ApiResponseDTO> GetMedicionesMensual(long id)
        {
            var response = await GetMediciones(id,DateTime.Now.AddMonths(-1), DateTime.Now, "1d");
            return response;
        }

        public async Task<ApiResponseDTO> GetMedicionesMensualPot(long id)
        {
            var response = await GetMediciones(id, DateTime.Now.AddMonths(-1), DateTime.Now, "1d",null,1, "AVERAGE", "&aggregationResult=DEVICE");
            return response;
        }

        public async Task<ApiResponseDTO> GetMedicionesSemanal(long id)
        {
            var response = await GetMediciones(id,DateTime.Now.AddDays(-7), DateTime.Now, "1d");
            return response;
        }

        public async Task<ApiResponseDTO> GetMedicionesSemanalPot(long id)
        {
            var response = await GetMediciones(id, DateTime.Now.AddDays(-7), DateTime.Now, "1d", null, 1, "AVERAGE", "&aggregationResult=DEVICE");
            return response;
        }

        public async Task<ApiResponseDTO> GetMedicionesDiaria(long id)
        {
            var response = await GetMediciones(id,DateTime.Now.AddDays(-1), DateTime.Now, "1h");
            return response;
        }

        public async Task<ApiResponseDTO> GetMedicionesDiariaPot(long id)
        {
            var response = await GetMediciones(id, DateTime.Now.AddDays(-1), DateTime.Now, "1h", null, 1, "AVERAGE", "&aggregationResult=DEVICE");
            return response;
        }

        public async Task<ApiResponseDTO> GetMedicionAvanzada(RequestConsultaAvanzadaDTO request)
        {
            var response = await GetMediciones(request.Id, request.Desde, request.Hasta, request.Frecuencia, request.Medidores);
            return response;
        }

        public async Task<ApiResponseDTO> GetMedicionAvanzadaPot(RequestConsultaAvanzadaDTO request)
        {
            var response = await GetMediciones(request.Id, request.Desde, request.Hasta, request.Frecuencia, request.Medidores,1, "AVERAGE", "&aggregationResult=DEVICE");
            return response;
        }

        private async Task<long> GetAsociateId(long id)
        {
            long response = 0;
            //var xxx = await _context.Unidades.FirstOrDefaultAsync();
            var unidad = await _context.Unidades.FirstOrDefaultAsync(x => x.OldId == id);
            if (unidad != null)
            {
                response = unidad.Id;
            }

            return response;
        }

        private async Task<ApiResponseDTO> MapApiObj(long id,Dictionary<string, DeviceDTO> dictionary,string frequency)
        {
            var medidores = await GetMedidores(id);
            var response = new ApiResponseDTO();
            var datasetList = new List<DataSetDTO>();
            var labels = new List<string>();
            var index = 0;
            foreach (var item in dictionary)
            {
                
                var newDataset = new DataSetDTO();
                var newData = new List<double>();
                var color = new List<string>();
                var border = new List<string>();
                
                newDataset.Label =  GetMedidorName(medidores,item.Key);
                
                foreach (var medicion in item.Value.Measures)
                {
                    var label = GetLabelByFrequency(medicion.Value.TimeStamp, frequency);
                    labels.Add(label);
                    newData.Add(medicion.Value.SensorValues.FirstOrDefault().SensorValue);
                    color.Add(SetRgbaColor(index));
                    border.Add(SetRgbaBorder(index));
                    
                }
                datasetList.Add(newDataset);
                newDataset.Data = newData;
                newDataset.BackgroundColor = color;
                newDataset.BorderColor = border;
                newDataset.BorderWidth = "1";
                index++;
            }
           

            labels = labels.Distinct().ToList();
            response.Datasets = datasetList;
            response.Labels = labels;
            return response;
        }

        private async Task<string> GetMedidoresString(long id)
        {
            var medidores = await GetMedidores(id);
            List<string> listId = new List<string>();
            foreach (var medidor in medidores)
            {
                listId.Add(medidor.Id.ToString());
            }

            var response = string.Join(",",listId);
            return response;
        }

        

        private string GetLabelByFrequency(int timeStamp, string frequency)
        {

            switch (frequency)
            {
                case "1M":
                    return TsToDt(timeStamp).ToString("MMMM/yyyy");
                    break;
                case "1d":
                    return TsToDt(timeStamp).ToString("dd/MM/yy");
                    break;
                case "1h":
                    return TsToDt(timeStamp).ToString("dd/MM/yy HH:00");
                    break;
                case "15m":
                    return TsToDt(timeStamp).ToString("dd/MM/yy HH:mm");
                    break;
                default:
                    return null;
                   
            }
        }

        private string GetMedidorName(List<MedidoresDTO> medidores, string key)
        {
            var medidor = medidores.Where(x => x.Id == Convert.ToInt32(key)).FirstOrDefault();
            return  medidor.Name;
        }

        private DateTime TsToDt(long timeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timeStamp).ToLocalTime();
            return dateTime;
            //var date = DateTimeOffset.FromUnixTimeSeconds(timeStamp).UtcDateTime;
            //return date;
        }

        private long DtToTs(DateTime date)
        {
            long unixTime = ((DateTimeOffset)date).ToUnixTimeSeconds();
            return unixTime;
        }
        private string SetRgbaColor(int index)
        {
            List<string> colores = new List<string>();
            colores.Add("rgba(255, 99, 132, 0.2)");//rojo
            colores.Add("rgba(54, 162, 235, 0.2)");//azul
            colores.Add("rgba(54, 235, 141, 0.2)");//verde
            colores.Add("rgba(226, 235, 54, 0.2)");//amarillo
            colores.Add("rgba(235, 181, 54, 0.2)");//naranjo
            colores.Add("rgba(184, 54, 235, 0.2)");//Lila
            colores.Add("rgba(54, 235, 232, 0.2)");//Cyan

            while (index>6)
            {
                index -= 6;
            }

            return colores[index];
        }

        private string SetRgbaBorder(int index)
        {
            List<string> colores = new List<string>();
            colores.Add("rgba(255, 99, 132, 1)");//rojo
            colores.Add("rgba(54, 162, 235, 1)");//azul
            colores.Add("rgba(54, 235, 141, 1)");//verde
            colores.Add("rgba(226, 235, 54, 1)");//amarillo
            colores.Add("rgba(235, 181, 54, 1)");//naranjo
            colores.Add("rgba(184, 54, 235, 1)");//Lila
            colores.Add("rgba(54, 235, 232, 1)");//Cyan
            while (index > 6)
            {
                index -= 6;
            }

            return colores[index];
        }


    }
}


