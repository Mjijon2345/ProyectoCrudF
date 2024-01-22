using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using ProyectoCrudF.DataAccess;
using ProyectoCrudF.DTOs;
using ProyectoCrudF.Utilidades;
using ProyectoCrudF.Modelos;
using System.Collections.ObjectModel;
using ProyectoCrudF.Views;

namespace ProyectoCrudF.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly UsuarioDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<UsuarioDTO> listaUsuario = new ObservableCollection<UsuarioDTO>();

        public MainViewModel(UsuarioDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<UsuarioMensajeria>(this, (r, m) =>
            {
                UsuarioMensajeRecibido(m.Value);
            });
        }

        public async Task Obtener()
        {
            var lista = await _dbContext.Usuario.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaUsuario.Add(new UsuarioDTO
                    {
                        IdUsuario = item.IdUsuario,
                        NombreCompleto = item.NombreCompleto,
                        Correo = item.Correo,
                        Telefono = item.Telefono,
                        Fecha = item.Fecha,
                    });
                }
            }
        }

        private void UsuarioMensajeRecibido(UsuarioMensaje usuarioMensaje)
        {
            var usuarioDto = usuarioMensaje.UsuarioDto;

            if (usuarioMensaje.EsCrear)
            {
                ListaUsuario.Add(usuarioDto);
            }
            else
            {
                var encontrado = ListaUsuario
                    .First(e => e.IdUsuario == usuarioDto.IdUsuario);

                encontrado.NombreCompleto = usuarioDto.NombreCompleto;
                encontrado.Correo = usuarioDto.Correo;
                encontrado.Telefono = usuarioDto.Telefono;
                encontrado.Fecha = usuarioDto.Fecha;

            }

        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(UsuarioPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(UsuarioDTO usuarioDto)
        {
            var uri = $"{nameof(Usuario)}?id={usuarioDto.IdUsuario}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(UsuarioDTO usuarioDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el Usuario?", "Si", "No");

            if (answer)
            {
                var encontrado = await _dbContext.Usuario
                    .FirstAsync(e => e.IdUsuario == usuarioDto.IdUsuario);

                _dbContext.Usuario.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaUsuario.Remove(usuarioDto);

            }

        }


    }
}
