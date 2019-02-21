using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Subasta.core.helpers
{
        public class SubastaHub : Hub
        {
            public void SendToAll(string message)
            {
                Clients.All.SendAsync("enviarATodos", message);
            }
        }
}
