using ProyectoCrudF.Modelos;
using ProyectoCrudF.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace ProyectoCrudF.DataAccess
{
    public class UsuarioDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("usuario.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(col => col.IdUsuario);
                entity.Property(col => col.IdUsuario).IsRequired().ValueGeneratedOnAdd();
            });
        }

    }

}
