using AutoMapper;
using GobEfi.FV.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReportesController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IReporteService _service;

        public ReportesController(ApplicationDbContext context, IMapper mapper, IReporteService service ) : base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("getreportevehiculos")]
        public async Task<IActionResult> GetReporteVehiculos() 
        {
            var _currentUser = GetCurrenUser();
            var isAdmin = _currentUser.Role == "ADMINISTRADOR";
            MemoryStream memoryStream = null;
            memoryStream = await _service.ReporteVehiculos(_currentUser.ServicioId, isAdmin);
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "vehiculos.xlsx");
        }
    }
}
