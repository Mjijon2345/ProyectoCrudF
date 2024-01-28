using ProyectoCrudF.Modelos;
using ProyectoCrudF.Utilidades;
using Microsoft.EntityFrameworkCore;

// Espacio de nombres que define el contexto de la base de datos para la aplicación
namespace ProyectoCrudF.DataAccess
{
    // Clase que representa el contexto de la base de datos para la aplicación
    public class UsuarioDbContext : DbContext
    {
        // DbSet que representa la tabla de usuarios en la base de datos
        public DbSet<Usuario> Usuarios { get; set; }

        // Método que configura las opciones de conexión a la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Obtener la ruta de la base de datos utilizando la clase de utilidad ConexionDB
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("usuarios.db")}";

            // Configurar el contexto para usar SQLite con la ruta obtenida
            optionsBuilder.UseSqlite(conexionDB);
        }

        // Método que configura el modelo de datos de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la entidad Usuario en el modelo de datos
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Definir la clave primaria como IdUsuario
                entity.HasKey(col => col.IdUsuario);

                // Configurar la propiedad IdUsuario como requerida y generada automáticamente al agregar
                entity.Property(col => col.IdUsuario).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
