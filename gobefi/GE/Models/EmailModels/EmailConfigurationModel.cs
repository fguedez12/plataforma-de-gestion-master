using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EmailModels
{
    public class EmailConfigurationModel
    {
        public string Email { set; get; }
        public string Host { set; get; }
        public int Port { set; get; }
        public string DisplayName { set; get; }
        public string User { set; get; }
        public string Password { set; get; }
        public bool UseSsl { set; get; }
        public bool UseCredentials { set; get; }
    }
}
