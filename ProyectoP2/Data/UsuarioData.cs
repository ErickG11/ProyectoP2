using ProyectoP2.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2.Data
{
    public class UsuarioData
    {
        private SQLiteAsyncConnection _connectionBD;

        public UsuarioData(SQLiteAsyncConnection connectionBD)
        {
            _connectionBD = connectionBD;
        }

        public Task<List<Usuario>> ListaUsuarios()
        {
            var lista = _connectionBD.Table<Usuario>().ToListAsync();
            return lista;
        }

        public Task<Usuario> ObtenerUsuario(string correo, string contraseña)
        {
            // Compara el correo y la contraseña cifrada
            var usuario = _connectionBD.Table<Usuario>().Where(x => x.Correo == correo).FirstOrDefaultAsync();
            return usuario;
        }

        public Task<Usuario> ObtenerUsuario(Guid id)
        {
            var usuario = _connectionBD.Table<Usuario>().Where(x => x.Id == id).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<int> GuardarUsuario(Usuario usuario)
        {
            // Cifra la contraseña antes de guardarla
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            var guardarUsuario = await ObtenerUsuario(usuario.Id);
            if (guardarUsuario == null)
            {
                return await _connectionBD.InsertAsync(usuario);
            }
            else
            {
                return await _connectionBD.UpdateAsync(usuario);
            }
        }

        public async Task<int> EliminarUsuario(Guid id)
        {
            return await _connectionBD.DeleteAsync(id);
        }
    }
}

