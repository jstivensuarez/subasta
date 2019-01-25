using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class EventoRepository: GenericRepository<Evento>, IEventoRepository
    {
        readonly SubastaContext context;

        public EventoRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
