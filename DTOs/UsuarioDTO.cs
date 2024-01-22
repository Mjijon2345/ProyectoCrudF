using CommunityToolkit.Mvvm.ComponentModel;

namespace ProyectoCrudF.DTOs
{
    public partial class UsuarioDTO : ObservableObject
    {
        [ObservableProperty]
        public int idUsuario;
        [ObservableProperty]
        public string nombreCompleto;
        [ObservableProperty]
        public string correo;
        [ObservableProperty]
        public int telefono;
        [ObservableProperty]
        public DateTime fecha;
    }
}
