﻿using Microsoft.EntityFrameworkCore;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        readonly SubastaContext context;

        public UsuarioRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public Usuario AutenticarUsuario(string usuario, string correo, string pass)
        {
            try
            {
                var entity = context.Usuarios.AsNoTracking()
                    .Include(c => c.Rol)
                    .SingleOrDefault(c => (c.Nombre == usuario || c.Correo == correo) && c.Clave == pass);
                return entity;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }
    }
}
