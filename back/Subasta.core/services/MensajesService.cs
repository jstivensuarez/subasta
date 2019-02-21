using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Subasta.core.dtos;
using Subasta.core.exceptions;
using Subasta.core.helpers;
using Subasta.core.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.services
{
    public class MensajesService: IMensajesService
    {
        readonly IHubContext<SubastaHub> hub;
        public MensajesService(IHubContext<SubastaHub> hub)
        {
            this.hub = hub;
        }

        public void EnviarMensaje(string mensaje)
        {
            try
            {
                hub.Clients.All.SendAsync("enviarATodos", mensaje);
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al enviar el mensaje a los clientes", ex);
            }
        }

        public string ObtenerMenajeFinal(object json, string tipo)
        {
            try
            {
                MensajeCliente mensaje = new MensajeCliente();
                mensaje.Mensaje = JsonConvert.SerializeObject(json);
                mensaje.Tipo = tipo;

                return JsonConvert.SerializeObject(mensaje);
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al enviar el mensaje a los clientes", ex);
            }
        }
    }
}
