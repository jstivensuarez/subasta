using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.states
{
    public class Estados
    {
        public const string CREADO = "CREADO";

        public const string BORRADO = "BORRADO";

        public const string PENDIENTE_APROBAR = "PENDIENTE_POR_APROBAR";

        public const string NO_AUTORIZADO = "NO_AUTORIZADO";

        public const string AUTORIZADO = "AUTORIZADO";

        public const string RECHAZADO = "RECHAZADO";

        public const string PENDIENTE_PAGAR = "PENDIENTE_POR_CONFIRMAR";

        public const string CONFIRMADO = "CONFIRMADO";
    }
}
