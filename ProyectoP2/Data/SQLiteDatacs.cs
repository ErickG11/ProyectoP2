using System;
using ProyectoP2.Models;
using SQLite;

namespace ProyectoP2.Data
{
    public class SQLiteDatacs
    {
        // Manejar la conexión a la base de datos
        readonly SQLiteAsyncConnection _connectionBD;

        public UsuarioData UsuarioDataTable { get; set; }
        public DescuentoAPI DescuentoDataTable { get; set; }

        // Constructor
        public SQLiteDatacs(string path)
        {
            _connectionBD = new SQLiteAsyncConnection(path);

            // Asegura que las tablas existan
            _connectionBD.CreateTableAsync<Usuario>().Wait();
            _connectionBD.CreateTableAsync<Descuento>().Wait();

            UsuarioDataTable = new UsuarioData(_connectionBD);
            DescuentoDataTable = new DescuentoAPI();
        }
    }
}

