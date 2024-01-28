using System.Threading.Tasks;

public async Task Obtener()
{
    // Obtener la lista de usuarios desde la base de datos de manera asíncrona
    var lista = await _dbContext.Usuarios.ToListAsync();

    // Verificar si la lista de usuarios no está vacía
    if (lista.Any())
    {
        // Iterar a través de cada usuario en la lista y agregarlo a la colección ListaUsuario
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

    // Verificar si se está creando un usuario
    if (usuarioMensaje.EsCrear)
    {
        // Agregar el nuevo usuario a la colección ListaUsuario
        ListaUsuario.Add(usuarioDto);
    }
    else
    {
        // Encontrar el usuario existente en ListaUsuario por Id y actualizar sus propiedades
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
    // Crear un nuevo usuario, navegando a la página de usuario con ID 0
    var uri = $"{nameof(UsuarioPage)}?id=0";
    await Shell.Current.GoToAsync(uri);
}

[RelayCommand]
private async Task Editar(UsuarioDTO usuarioDto)
{
    // Editar un usuario existente, navegando a la página de usuario con el ID correspondiente
    var uri = $"{nameof(UsuarioPage)}?id={usuarioDto.IdUsuario}";
    await Shell.Current.GoToAsync(uri);
}

[RelayCommand]
private async Task Eliminar(UsuarioDTO usuarioDto)
{
    // Mostrar un mensaje de confirmación antes de eliminar un usuario
    bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el Usuario?", "Si", "No");

    // Verificar si el usuario confirmó la eliminación
    if (answer)
    {
        // Encontrar el usuario en la base de datos y eliminarlo
        var encontrado = await _dbContext.Usuarios
            .FirstAsync(e => e.IdUsuario == usuarioDto.IdUsuario);

        _dbContext.Usuarios.Remove(encontrado);
        await _dbContext.SaveChangesAsync();

        // Eliminar el usuario de la colección ListaUsuario
        ListaUsuario.Remove(usuarioDto);
    }
}
