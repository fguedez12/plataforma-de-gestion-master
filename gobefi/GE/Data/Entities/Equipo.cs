using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Equipo : BaseEntity
    {
        private long _tipoTecnologiaId;
        private long _sistemaId;
        private long _modoOperacionId;
        private long _energeticoIn;
        private long _energeticoOut;

        private int _anyoCompra;
        private double _horasUso;
        private string _marca;
        private string _modelo;
        private double _potencia;
        private int _cantidad;
        private int _inversion;
        private int _costoMantencion;

        private TipoTecnologia _tipoTecnologia;
        private Sistema _sistema;
        private ModoOperacion _modoOperacion;
        private Energetico _energeticIn;
        private Energetico _energeticOut;

        public long TipoTecnologiaId { get => _tipoTecnologiaId; set => _tipoTecnologiaId = value; }
        public long SistemaId { get => _sistemaId; set => _sistemaId = value; }
        public long ModoOperacionId { get => _modoOperacionId; set => _modoOperacionId = value; }
        public long EnergeticoIn { get => _energeticoIn; set => _energeticoIn = value; }
        public long EnergeticoOut { get => _energeticoOut; set => _energeticoOut = value; }
        public int AnyoCompra { get => _anyoCompra; set => _anyoCompra = value; }
        public double HorasUso { get => _horasUso; set => _horasUso = value; }
        public string Marca { get => _marca; set => _marca = value; }
        public string Modelo { get => _modelo; set => _modelo = value; }
        public double Potencia { get => _potencia; set => _potencia = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public int Inversion { get => _inversion; set => _inversion = value; }
        public int CostoMantencion { get => _costoMantencion; set => _costoMantencion = value; }
        public TipoTecnologia TipoTecnologia { get => _tipoTecnologia; set => _tipoTecnologia = value; }
        public Sistema Sistema { get => _sistema; set => _sistema = value; }
        public ModoOperacion ModoOperacion { get => _modoOperacion; set => _modoOperacion = value; }
        public Energetico EnergeticIn { get => _energeticIn; set => _energeticIn = value; }
        public Energetico EnergeticOut { get => _energeticOut; set => _energeticOut = value; }
    }
}
