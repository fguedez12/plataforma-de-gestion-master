using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int perPage = 8;
        private readonly int maxPerPage = 40;
        public int PerPage 
        { 
            get => perPage;
            set 
            {
                perPage = (value > maxPerPage) ? maxPerPage : value;
            }
        }
    }
}
