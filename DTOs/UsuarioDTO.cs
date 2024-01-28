using CommunityToolkit.Mvvm.ComponentModel;
using System;

// Espacio de nombres que define la estructura de datos para transferencia de objetos de usuario (DTOs)
namespace ProyectoCrudF.DTOs
{
    // Clase parcial que representa un UsuarioDTO y hereda de ObservableObject para notificar cambios
    public partial class UsuarioDTO : ObservableObject
    {
        // Atributo que representa el identificador del usuario
        [ObservableProperty]
        public int idUsuario;

        // Atributo que representa el nombre completo del usuario
        [ObservableProperty]
        public string nombreCompleto;

        // Atributo que representa la dirección de correo electrónico del usuario
        [ObservableProperty]
        public string correo;

        // Atributo que representa el número de teléfono del usuario
        [ObservableProperty]
        public int telefono;

        // Atributo que representa la fecha asociada al usuario
        [ObservableProperty]
        public DateTime fecha;
    }
}
