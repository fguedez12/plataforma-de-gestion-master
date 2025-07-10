using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class Usuario : IdentityUser
    {
        public long? SexoId { get; set; }
        public long ComunaId { get; set; }
        public int? OldId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Nacionalidad { get; set; }
        public string Cargo { get; set; }
        public string Rut { get; set; }
        public bool? Certificado { get; set; }
        public bool? Validado { get; set; }
        public bool? Active { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string NumeroTelefonoOpcional { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }


        public virtual Comuna Comuna { get; set; }
        public Sexo Sexo { get; set; }

        public List<NotasCertificado> Notas { get; set; }
        public List<UsuarioUnidad> UsuarioUnidades { get; set; }
        public virtual ICollection<UsuarioInstitucion> UsuariosInstituciones { get; set; }
        public virtual ICollection<UsuarioServicio> UsuariosServicios { get; set; }
        public virtual ICollection<UsuarioDivision> UsuariosDivisiones { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; }

        public string DisplayAddress
        {
            get
            {
                string dspAddress =
                    string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity =
                    string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspPostalCode =
                    string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;

                return string
                    .Format("{0} {1} {2}", dspAddress, dspCity, dspPostalCode);
            }
        }
    }
}
