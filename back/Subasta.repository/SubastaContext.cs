using Microsoft.EntityFrameworkCore;
using Subasta.repository.models;
using System.Linq;

namespace Subasta.repository
{
    public class SubastaContext: DbContext
    {

        public DbSet<Animal> Animales { get; set; }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Lote> Lotes { get; set; }

        public DbSet<Municipio> Municipios { get; set; }

        public DbSet<Puja> Pujas { get; set; }

        public DbSet<Pujador> Pujadores { get; set; }

        public DbSet<Raza> Razas { get; set; }

        public DbSet<Sexo> Sexos { get; set; }

        public DbSet<TipoDocumento> TipoDocumentos { get; set; }

        public SubastaContext(DbContextOptions<SubastaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pujador>()
                .HasKey(c => new { c.ClienteId, c.SubastaId });
        }

    }
}
