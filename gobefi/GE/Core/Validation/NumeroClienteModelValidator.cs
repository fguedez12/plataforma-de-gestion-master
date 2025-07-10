using FluentValidation;
using GobEfi.Web.Models.NumeroClienteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Validation
{
    public class NumeroClienteModelValidator : AbstractValidator<NumeroClienteModel>
    {
        public NumeroClienteModelValidator() {
            //long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));
            When(n=> n.EnergeticoId==3 || n.EnergeticoId==2, () => {
                RuleFor(n => n.EmpresaDistribuidoraId).NotNull().WithMessage("Debe seleccionar una empresa distribuidora");
            });
        }
    }
}
