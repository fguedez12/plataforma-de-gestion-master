using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
{
    public class Region
    {
        private string _nombre;
        private int _numero;
        private int _posicion;

        public long Id { get; set; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int Numero { get => _numero; set => _numero = value; }
        public int Posicion { get => _posicion; set => _posicion = value; }

    }
}