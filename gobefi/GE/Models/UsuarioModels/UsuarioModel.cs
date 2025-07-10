using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Models.RolModels;
using GobEfi.Web.Models.ServicioModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.UsuarioModels
{
    public class UsuarioModel : BaseModel<string>
    {
        public int? OldId { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [DisplayName("Nombres (*)")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos es obligatorio")]
        [DisplayName("Apellidos (*)")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Favor ingresar un email válido.")]
        [DisplayName("Email (*)")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Nacionalidad es obligatorio")]
        [DisplayName("Nacionalidad (*)")]
        public string Nacionalidad { get; set; }

        [Required(ErrorMessage = "Cargo es obligatorio")]
        [DisplayName("Cargo (*)")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Rut es obligatorio")]
        [StringLength(12, ErrorMessage = "Maximo de 12 caracteres")]
        [DisplayName("Rut (*)")]
        public string Rut { get; set; }

        public bool Certificado { get; set; }
        public bool Validado { get; set; }

        [Required(ErrorMessage = "Comuna es obligatorio")]
        [DisplayName("Comuna (*)")]
        public long? ComunaId { get; set; }

        [Required(ErrorMessage = "Región es obligatorio")]
        [DisplayName("Región (*)")]
        public long? RegionId { get; set; }

        [Required(ErrorMessage = "Provincia es obligatorio")]
        [DisplayName("Provincia (*)")]
        public long? ProvinciaId { get; set; }

        [Required(ErrorMessage = "Sexo es obligatorio")]
        [DisplayName("Sexo (*)")]
        public long? SexoId { get; set; }

        [Required(ErrorMessage = "Telefono fijo es obligatorio")]
        [DisplayName("Telefono fijo (*)")]
        public string NumeroTelefono { get; set; }
        [DisplayName("Telefono opcional")]
        public string NumeroTelefonoOpcional { get; set; }

        public virtual ComunaModel Comuna { get; set; }
        public ICollection<RolModel> Roles { get; set; }

        [DisplayName("Instituciones (*)")]
        public ICollection<InstitucionModel> Instituciones { get; set; }

        [DisplayName("Servicios (*)")]
        public ICollection<ServicioModel> Servicios { get; set; }

        [DisplayName("Unidades")]
        public ICollection<DivisionModel> Divisiones { get; set; }
        public ICollection<EdificioModel> Edificios { get; set; }

        [Required(ErrorMessage = "Usuario debe tener al menos un tipo de gestor asignado")]
        [DisplayName("Tipo de Gestores (*)")]
        public ICollection<string> RolesId { get; set; }

        [Required(ErrorMessage = "Usuario debe tener al menos una institución asignada")]
        public ICollection<long> InstitucionesId { get; set; }
        public ICollection<long> InstitucionesNoAsociados { get; set; }

        [Required(ErrorMessage = "Usuario debe tener al menos un servicio asignado.")]
        public ICollection<long> ServiciosId { get; set; }
        public ICollection<long> ServiciosNoAsociados { get; set; }
        public ICollection<long> DivisionesId { get; set; }
        public ICollection<long> EdificiosId { get; set; }
    }
}
