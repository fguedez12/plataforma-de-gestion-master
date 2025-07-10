using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MedidorModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property |
  AttributeTargets.Field, AllowMultiple = false)]
    sealed public class FileType : ValidationAttribute
    {
        private readonly string _mimeTypes;

        public FileType(string mimeTypes)
        {
            _mimeTypes = mimeTypes;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider().TryGetContentType(value.ToString(), out string mimeType);

            foreach (string item in _mimeTypes.Split(','))
            {
                if (mimeType == item)
                    return ValidationResult.Success;
            }

            
            return new ValidationResult("Tipo de archivo no valido");

            
        }
    }
}
