using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface ICorreoHelper
    {
        void enviarDesdeSubasta(string mensaje, string asunto, string destino);
    }
}
