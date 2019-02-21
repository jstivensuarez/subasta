using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IMensajesService
    {
        void EnviarMensaje(string mensaje);

        string ObtenerMenajeFinal(object json, string tipo);
    }
}
