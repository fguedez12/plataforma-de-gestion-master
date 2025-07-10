using AutoMapper;
using GobEfi.FV.API.Filters;
using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropulsionesController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PropulsionesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<List<PropulsionDTO>> Get()
        {
            var list = await Get<Propulsion, PropulsionDTO>();
            return list;
        }

    }
}
