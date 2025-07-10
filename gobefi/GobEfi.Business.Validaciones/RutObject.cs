using System;

namespace GobEfi.Business.Validaciones
{
    public class RutObject
    {
        /// <summary>
        /// Código real del RUT
        /// </summary>
        public int rut = 0;

        /// <summary>
        /// Dígito verificador del RUT
        /// </summary>
        public string verificador = String.Empty;

        /// <summary>
        /// Rut compuesto (ej:11.111.111-1)
        /// </summary>
        public string compuesto = String.Empty;

        /// <summary>
        /// Define si el rut validado es correcto o no
        /// </summary>
        public bool valido = false;

        /// <summary>
        /// Objeto de tipo RUT utilizado para la devolución de la función de validación de este tipo de valor.
        /// </summary>
        public RutObject()
        {

        }

        /// <summary>
        /// Objeto de tipo RUT utilizado para la devolución de la función de validación de este tipo de valor.
        /// </summary>
        /// <param name="rutArg">Código real del RUT</param>
        /// <param name="verificadorArg">Dígito verificador del RUT</param>
        /// <param name="compuestoArg">Rut compuesto (ej:11.111.111-1)</param>
        public RutObject(int rutArg, string verificadorArg, string compuestoArg)
        {
            this.rut = rutArg;
            this.verificador = verificadorArg;
            this.compuesto = compuestoArg;
            this.valido = true;
        }
    }
}
