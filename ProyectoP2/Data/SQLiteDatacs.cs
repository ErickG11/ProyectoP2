
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoP2.Models;
using SQLite;

namespace ProyectoP2.Data
{
    public class SQLiteDatacs
    {
        readonly SQLiteAsyncConnection _connectionBD;

        public UsuarioData UsuarioDataTable { get; set; }

        public SQLiteDatacs(string path)
        {
            _connectionBD = new SQLiteAsyncConnection(path);

            _connectionBD.CreateTableAsync<Usuario>().Wait();

            UsuarioDataTable = new UsuarioData(_connectionBD);
        }
    }
}

