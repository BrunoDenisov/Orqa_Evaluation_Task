using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OrqaDB.WPF.Helpers
{
    public static class SecurePasswordHelper
    {
        public static void ClearSecureString(SecureString secureString)
        {
            if (secureString != null)
            {
                secureString.Dispose();
            }
        }
    }

}
