using System;
using System.ComponentModel.DataAnnotations;

// Espacio de nombres que define la estructura de modelos de datos
namespace ProyectoCrudF.Modelos
{
    // Clase que representa el modelo de datos para un usuario
    public class Usuario
    {
        // Atributo clave primaria que representa el identificador único del usuario
        [Key]
        public int IdUsuario { get; set; }

        // Atributo que representa el nombre completo del usuario
        public string NombreCompleto { get; set; }

        // Atributo que representa la dirección de correo electrónico del usuario
        public string Correo { get; set; }

        // Atributo que representa el número de teléfono del usuario
        public int Telefono { get; set; }

        // Atributo que representa la fecha asociada al usuario
        public DateTime Fecha { get; set; }
    }
}
