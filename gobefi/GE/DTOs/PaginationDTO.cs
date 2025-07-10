using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int perPage = 5;
        private readonly int MaxPerPage = 20;

        public int PerPage
        {
            get => perPage;
            set
            {

                perPage = (value > MaxPerPage ? MaxPerPage : value);
            }
        }
    }
}
