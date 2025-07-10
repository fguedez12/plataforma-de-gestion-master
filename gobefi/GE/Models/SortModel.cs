using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SortModel
    {
        /// <summary>
        /// 
        /// </summary>
        public SortModel()
        {
            this.Items = new List<SelectListItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<SelectListItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Ordenar por")]
        public string FieldName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SortType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        public void AddSortField(string title, string value)
        {
            this.Items.Add(new SelectListItem { Text = title, Value = value });
        }
    }
}
