// Espacio de nombres que define utilidades relacionadas con la conexión a la base de datos
using System.IO;
using System;

namespace ProyectoCrudF.Utilidades
{
    // Clase estática que proporciona métodos relacionados con la conexión a la base de datos
    public static class ConexionDB
    {
        // Método que devuelve la ruta de almacenamiento para la base de datos en función de la plataforma
        public static string DevolverRuta(string nombreBaseDatos)
        {
            // Inicializar la ruta de la base de datos como cadena vacía
            string rutaBaseDatos = string.Empty;

            // Verificar la plataforma actual (Android)
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Obtener la ruta del directorio de aplicación local en Android
                rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Combinar la ruta con el nombre de la base de datos
                rutaBaseDatos = Path.Combine(rutaBaseDatos, nombreBaseDatos);
            }
            // Verificar la plataforma actual (iOS)
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Obtener la ruta del directorio MyDocuments en iOS
                rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Combinar la ruta con "..", "Library" y el nombre de la base de datos en iOS
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "..", "Library", nombreBaseDatos);
            }

            // Devolver la ruta de la base de datos
            return rutaBaseDatos;
        }
    }
}
