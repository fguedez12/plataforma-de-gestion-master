using GobEfi.Web.Models.SexoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required( ErrorMessage = "Este campo es oblogatorio.")]
        [EmailAddress(ErrorMessage = "El email no es valido.")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Número de teléfono")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Display(Name = "Región")]
        public string Region { get; set; }

        [Display(Name = "Código Postal")]
        public string PostalCode { get; set; }

        public string StatusMessage { get; set; }

        [Display(Name = "Sexo")]
        public long? SexoId { get; set; }

        public string Rut { get; set; }

        [Required(ErrorMessage = "El campo comuna es obligatorio")]
        [Display(Name = "Comuna")]
        public long ComunaId { get; set; }
        [Display(Name = "Región")]
        public long? RegionId { get; set; }
        [Display(Name = "Provincia")]
        public long? ProvinciaId { get; set; }

        public bool Certificado { get; set; }
        public bool Validado { get; set; }
    }
}
