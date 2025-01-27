using System;
using ProyectoP2.Models;
using SQLite;

namespace ProyectoP2.Data
{
    public class SQLiteDatacs
    {
        //manejar la conexión a la base de datos
        readonly SQLiteAsyncConnection _connectionBD;

        public UsuarioData UsuarioDataTable { get; set; }

        public SQLiteDatacs(string path)
        {
            _connectionBD = new SQLiteAsyncConnection(path);

            // asegura que la tabla de usuarios exista al iniciar la conexión
            _connectionBD.CreateTableAsync<Usuario>().Wait();

            // inicializa la propiedad UsuarioDataTable con la conexión
            UsuarioDataTable = new UsuarioData(_connectionBD);
        }
    }
}


