using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.Utilities
{
    public class ExceptionHelper
    {
        public static bool IsSecurityOrCriticalException(Exception ex)
        {
            return ((ex is SecurityException) || IsCriticalException(ex));
        }

        public static bool IsCriticalException(Exception ex)
        {
            return (ex is StackOverflowException) || (ex is OutOfMemoryException) || (ex is ThreadAbortException) || (ex is AccessViolationException);
        }
    }
}
