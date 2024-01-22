using System.ComponentModel.DataAnnotations;

namespace ProyectoCrudF.Modelos
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        public DateTime Fecha { get; set; }

    }
}
