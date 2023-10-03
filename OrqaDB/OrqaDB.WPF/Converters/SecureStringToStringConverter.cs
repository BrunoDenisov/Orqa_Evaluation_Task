using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Data;

namespace OrqaDB.WPF.Converters
{
    public class SecureStringToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SecureString secureString)
            {
                IntPtr ptr = Marshal.SecureStringToBSTR(secureString);
                try
                {
                    return Marshal.PtrToStringBSTR(ptr);
                }
                finally
                {
                    Marshal.ZeroFreeBSTR(ptr);
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string password)
            {
                var securePassword = new SecureString();
                foreach (char c in password)
                {
                    securePassword.AppendChar(c);
                }
                return securePassword;
            }

            return null;
        }

    }

}
