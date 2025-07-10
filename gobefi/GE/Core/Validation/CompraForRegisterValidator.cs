using FluentValidation;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.CompraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Validation
{
    public class CompraForRegisterValidator : AbstractValidator<CompraForRegister>
    {
        private readonly IEnergeticoDivisionService _serviceEnergeticoDivision;
        public CompraForRegisterValidator(IEnergeticoDivisionService serviceEnergeticoDivision) {

            _serviceEnergeticoDivision = serviceEnergeticoDivision;

            //RuleFor(c=>c.Costo).NotNull().WithMessage("Debe ingresar el costo total");
            RuleFor(c => c.EnergeticoId).NotNull().WithMessage("Debe seleccionar un energético");

            RuleFor(c => c.FacturaId).NotNull().WithMessage("Debe ingresar un archivo");
            When(c => tieneNumeroCliente(c.DivisionId, c.EnergeticoId,c.SinMedidor), () => {
                RuleFor(c => c.NumeroClienteId).NotEqual(0).WithMessage("Debe selecionar un Numero de cliente");
                RuleFor(c => c.InicioLectura).NotNull().WithMessage("Campo obligatorio. (dd-mm-aaaa)");
                RuleFor(c => c.InicioLectura)
                    .Must((comp, o)=>ComparaFechas(comp.InicioLectura,comp.FinLectura))
                    .WithMessage("Fecha de inicio no puede ser mayor a la fecha de fin.");
                RuleFor(c => c.FinLectura).NotNull().WithMessage("Campo obligatorio. (dd-mm-aaaa)");
                RuleFor(c => c.FinLectura)
                    .Must((comp, o) => ComparaFechas(comp.InicioLectura, comp.FinLectura))
                    .WithMessage("Fecha de fin no puede ser mayor a la fecha de fin.");
                RuleFor(c => c.ListaMedidores).NotNull().WithMessage("Debe tener al menos 1 consumo registrado.");
            });

            Unless(c => tieneNumeroCliente(c.DivisionId, c.EnergeticoId, c.SinMedidor), () =>
            {
                //RuleFor(c => c.ConsumoCompra).NotNull().WithMessage("Debe ingresar el consumo");
                //RuleFor(c => c.FechaCompra).NotNull().WithMessage("Campo obligatorio. (dd-mm-aaaa)");
                //RuleFor(c => c.FechaCompra)
                //     .Must((comp, o) => ComparaFechaActual(comp.FechaCompra))
                //     .WithMessage("Fecha de fin no puede mayor a la fecha actual.");
            });

        }

        private bool tieneNumeroCliente(long divisionId,long? energeticoId, bool sinMedidor) {

            var result = false;

            if (energeticoId == 3 || energeticoId == 2)
            {
                result =  true;
            }
            else {

                if (energeticoId == 1 && sinMedidor)
                {
                    result = false;
                    return result;
                }

                result = _serviceEnergeticoDivision.TieneNumCliente(divisionId, energeticoId);
            }
            return result;
        }

        private bool ComparaFechas(string ini, string fin) {

            DateTime.TryParse(ini, out DateTime finicio);
            DateTime.TryParse(fin, out DateTime ffin);
              

            int result = DateTime.Compare(ffin, finicio);

            if (result < 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        private bool ComparaFechaActual(string fecha) {

            DateTime.TryParse(fecha, out DateTime fin);
               
            int result = DateTime.Compare(DateTime.Now, fin);

            if (result < 0)
            {
                return false;
            }
            else {
                return true;
            }
            
        }
    }
}
