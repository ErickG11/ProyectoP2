using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2
{
    public static class Constantes
    {
        private const string DBFileName = "Datos.db3";

        // abrir la base de datos
        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        // devuelve la ruta completa del archivo de la base de datos
        public static string DatabasePath
        {
            get
            {
                // combina el directorio de la aplicación con el nombre del archivo de la base de datos
                return Path.Combine(FileSystem.AppDataDirectory, DBFileName);
            }
        }
    }
}

