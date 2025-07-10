using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models
{
    public class BaseResult
    {
        public string Status { get; set; }
        public ModelStateDictionary Errors { get; set; }
        public string Message { get; set; }

        public static BaseResult Error(string message, ModelStateDictionary errors)
        {
            return new BaseResult { Errors = errors, Status = "ERROR", Message = message };
        }

        public static BaseResult Error(string message)
        {
            return new BaseResult { Errors = new ModelStateDictionary(), Status = "ERROR", Message = message };
        }

        public static BaseResult Success(string message)
        {
            return new BaseResult { Errors = new ModelStateDictionary(), Status = "OK", Message = message };
        }
    }
}
