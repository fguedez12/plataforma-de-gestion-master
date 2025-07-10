using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace api_gestiona.Security
{
    public class Impersonate : IDisposable
    {
       
        #region Declaraciones

        private bool _disposed = false;

        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;
        private readonly IConfiguration _configuration;

        //private WindowsImpersonationContext impersonationContext;

        //[DllImport("advapi32.dll")]
        //public static extern int LogonUserA(String lpszUserName,
        //    String lpszDomain,
        //    String lpszPassword,
        //    int dwLogonType,
        //    int dwLogonProvider,
        //    ref IntPtr phToken);

        public Impersonate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
       int dwLogonType, int dwLogonProvider, out SafeAccessTokenHandle phToken);

        // [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        // public static extern int DuplicateToken(IntPtr hToken,
        //     int impersonationLevel,
        //     ref IntPtr hNewToken);

        // [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        // public static extern bool RevertToSelf();

        // [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        // public static extern bool CloseHandle(IntPtr handle);

        public bool Impersonalizar
        {
            get
            {
                try
                {
                    return _configuration.GetValue<bool>("ImpersonalizacionSettings:Impersonalizar");
                }
                catch
                {
                    return false;
                }
            }
        }

        public string Usuario
        {
            get
            {
                try
                {
                    return _configuration.GetValue<string>("ImpersonalizacionSettings:Usuario");
                }
                catch
                {
                    throw new Exception("Debe especificar Usuario en config.");
                }
            }
        }

        public string Clave
        {
            get
            {
                try
                {
                    return _configuration.GetValue<string>("ImpersonalizacionSettings:Clave");
                }
                catch
                {
                    throw new Exception("Debe especificar Contraseña en config.");
                }
            }
        }

        public string Dominio
        {
            get
            {
                try
                {
                    return _configuration.GetValue<string>("ImpersonalizacionSettings:Dominio");
                }
                catch
                {
                    throw new Exception("Debe especificar Dominio en config.");
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //if (impersonationContext != null)
                    //    this.impersonationContext.Undo();
                }

                this._disposed = true;
            }
        }

        ~Impersonate()
        {
            this.Dispose(false);
        }

        #endregion

        public Impersonate()
        {
            if (this.Impersonalizar)
            {
                try
                {
                    // Call LogonUser to obtain a handle to an access token.   
                    SafeAccessTokenHandle safeAccessTokenHandle;
                    bool returnValue = LogonUser(Usuario, Dominio, Clave,
                        LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                        out safeAccessTokenHandle);

                    if (false == returnValue)
                    {
                        int ret = Marshal.GetLastWin32Error();
                        //Console.WriteLine("LogonUser failed with error code : {0}", ret);
                        throw new System.ComponentModel.Win32Exception(ret);
                    }

                    // Console.WriteLine("Did LogonUser Succeed? " + (returnValue ? "Yes" : "No"));
                    // Check the identity.  
                    // Console.WriteLine("Before impersonation: " + WindowsIdentity.GetCurrent().Name);

                    // Note: if you want to run as unimpersonated, pass  
                    //       'SafeAccessTokenHandle.InvalidHandle' instead of variable 'safeAccessTokenHandle'  
                    WindowsIdentity.RunImpersonated(
                        safeAccessTokenHandle,
                        // User action  
                        () =>
                        {
                            // Check the identity.  
                            Console.WriteLine("During impersonation: " + WindowsIdentity.GetCurrent().Name);
                            return;
                        }
                        );

                    // Check the identity again.  
                    //Console.WriteLine("After impersonation: " + WindowsIdentity.GetCurrent().Name);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
