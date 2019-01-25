using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.exceptions
{
    public class ExceptionData: Exception
    {
        public ExceptionData()
        {

        }

        public ExceptionData(string mesagge, Exception ex) : base(mesagge, ex)
        {

        }
    }
}
