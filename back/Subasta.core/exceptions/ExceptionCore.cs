using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.exceptions
{
    public class ExceptionCore: Exception
    {
        public ExceptionCore()
        {

        }

        public ExceptionCore(string message) : base(message)
        {

        }

        public ExceptionCore(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
