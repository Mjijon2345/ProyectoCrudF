using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using ProyectoCrudF.DataAccess;
using ProyectoCrudF.DTOs;
using ProyectoCrudF.Utilidades;
using ProyectoCrudF.Modelos;

namespace ProyectoCrudF.ViewModels
{
    public partial class UsuarioViewModel : ObservableObject, IQueryAttributable
    {
        private readonly UsuarioDbContext _dbContext;

        [ObservableProperty]
        private UsuarioDTO usuarioDto = new UsuarioDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IdUsuario;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public UsuarioViewModel(UsuarioDbContext context)
        {
            _dbContext = context;
            usuarioDto.Fecha = DateTime.Now;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdUsuario = id;

            if (IdUsuario == 0)
            {
                TituloPagina = "Nuevo Usuario";
            }
            else
            {
                TituloPagina = "Editar Usuario";
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Usuarios.FirstAsync(e => e.IdUsuario == IdUsuario);
                    UsuarioDto.IdUsuario = encontrado.IdUsuario;
                    UsuarioDto.NombreCompleto = encontrado.NombreCompleto;
                    UsuarioDto.Correo = encontrado.Correo;
                    UsuarioDto.Telefono = encontrado.Telefono;
                    UsuarioDto.Fecha = encontrado.Fecha;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });
                });
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            UsuarioMensaje mensaje = new UsuarioMensaje();

            await Task.Run(async () =>
            {
                if (IdUsuario == 0)
                {
                    var tbUsuario = new Usuario
                    {
                        NombreCompleto = UsuarioDto.NombreCompleto,
                        Correo = UsuarioDto.Correo,
                        Telefono = UsuarioDto.Telefono,
                        Fecha = UsuarioDto.Fecha,
                    };

                    _dbContext.Usuarios.Add(tbUsuario);
                    await _dbContext.SaveChangesAsync();

                    UsuarioDto.IdUsuario = tbUsuario.IdUsuario;
                    mensaje = new UsuarioMensaje()
                    {
                        EsCrear = true,
                        UsuarioDto = UsuarioDto
                    };

                }
                else
                {
                    var encontrado = await _dbContext.Usuarios.FirstAsync(e => e.IdUsuario == IdUsuario);
                    encontrado.NombreCompleto = UsuarioDto.NombreCompleto;
                    encontrado.Correo = UsuarioDto.Correo;
                    encontrado.Telefono = UsuarioDto.Telefono;
                    encontrado.Fecha = UsuarioDto.Fecha;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new UsuarioMensaje()
                    {
                        EsCrear = false,
                        UsuarioDto = UsuarioDto
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new UsuarioMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });

            });
        }


    }
}
