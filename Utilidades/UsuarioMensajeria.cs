using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProyectoCrudF.Utilidades
{
    internal class UsuarioMensajeria : ValueChangedMessage<UsuarioMensaje>
    {
        public UsuarioMensajeria(UsuarioMensaje value) : base(value)
        {

        }
    }
}
