using Microsoft.EntityFrameworkCore;
using ServicioCorreo.Dal.Modelo;


namespace ServicioCorreo.Dal.Datos.Context
{
    public class ContextCorreo : DbContext
    {
        public ContextCorreo(DbContextOptions<ContextCorreo> options)
        : base(options)
        {
        }

        public DbSet<Log> Logs{ get; set; } 
        public DbSet<Plantilla> Plantillas { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de LogCorreo
            modelBuilder.Entity<Log>(entity =>
            {
              
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Destinatario).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Asunto).HasMaxLength(255);
                entity.Property(e => e.FechaEnvio).IsRequired();
            });

           
            modelBuilder.Entity<Plantilla>(entity =>
            {
            
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ContenidoHtml).IsRequired();
                entity.Property(e => e.FechaCreacion).IsRequired();
            });
        }
    }
}

