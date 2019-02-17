using Microsoft.EntityFrameworkCore;
using Subasta.repository.models;
using System.Linq;

namespace Subasta.repository
{
    public class SubastaContext : DbContext
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

        public DbSet<TipoDocumento> TipoDocumentos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Subasta.repository.models.Subasta> Subastas { get; set; }

        public DbSet<SolicitudSubasta> SolicitudSubastas { get; set; }

        public DbSet<Clasificacion> Clasificaciones { get; set; }

        public SubastaContext(DbContextOptions<SubastaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .Property(b => b.Peso)
                .HasColumnType("decimal(15, 4)")
                .HasDefaultValue(0);
          

            modelBuilder.Entity<Lote>()
                .Property(b => b.PesoPromedio)
                .HasColumnType("decimal(10, 4)")
                .HasDefaultValue(0);

            modelBuilder.Entity<Lote>()
               .Property(b => b.PesoTotal)
               .HasColumnType("decimal(15, 4)")
               .HasDefaultValue(0);

            modelBuilder.Entity<Lote>()
             .Property(b => b.PrecioBase)
             .HasColumnType("decimal(15, 4)")
             .HasDefaultValue(0);

            modelBuilder.Entity<Lote>()
             .Property(b => b.PrecioInicial)
             .HasColumnType("decimal(15, 4)")
             .HasDefaultValue(0);

            modelBuilder.Entity<Puja>()
             .Property(b => b.Valor)
             .HasColumnType("decimal(15, 4)")
             .HasDefaultValue(0);

            modelBuilder.Entity<Pujador>()
            .Property(b => b.ValorConsignacion)
            .HasColumnType("decimal(15, 4)")
            .HasDefaultValue(0);

            modelBuilder.Entity<models.Subasta>()
            .Property(b => b.ValorAnticipo)
            .HasColumnType("decimal(15, 4)")
            .HasDefaultValue(0);

            modelBuilder.Entity<Cliente>().ToTable("TBL_CLIENTES");
        }

    }
}
