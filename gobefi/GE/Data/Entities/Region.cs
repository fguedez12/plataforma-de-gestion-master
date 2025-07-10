using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Region
    {
        private string _nombre;
        private int _numero;
        private int _posicion;

        private ICollection<Provincia> _provincias;
        private ICollection<Comuna> _comunas;

        public long Id { get; set; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int Numero { get => _numero; set => _numero = value; }
        public int Posicion { get => _posicion; set => _posicion = value; }
        public ICollection<Provincia> Provincias { get => _provincias; set => _provincias = value; }
        public ICollection<Comuna> Comunas { get => _comunas; set => _comunas = value; }
        public virtual ICollection<Division> Inmuebles { get; set; }
        public virtual List<Direccion> Direcciones { get; set; }

        public Region()
        {
            this.Provincias = new HashSet<Provincia>();
            this.Comunas = new HashSet<Comuna>();
        }
    }
}