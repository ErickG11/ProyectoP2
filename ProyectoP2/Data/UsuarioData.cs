using ProyectoP2.Models;
using SQLite;

namespace ProyectoP2.Data
{
    public class UsuarioData
    {
        // conexión asincrónica a la base de datos 
        private SQLiteAsyncConnection _connectionBD;

        public UsuarioData(SQLiteAsyncConnection connectionBD)
        {
            _connectionBD = connectionBD;
        }

        // retorna todos los usuarios
        public Task<List<Usuario>> ListaUsuarios()
        {
            // todos los registros de la tabla usuario
            var lista = _connectionBD.Table<Usuario>().ToListAsync();
            return lista;
        }

        // retorna un único usuario basado en el correo y la contraseña
        public Task<Usuario> ObtenerUsuario(string correo, string contraseña)
        {
            // filtra por el correo proporcionado
            var usuario = _connectionBD.Table<Usuario>().Where(x => x.Correo == correo).FirstOrDefaultAsync();
            return usuario;
        }

        // retorna un usuario basado por su id
        public Task<Usuario> ObtenerUsuario(Guid id)
        {
            // Consulta la tabla de usuarios y filtra por el ID
            var usuario = _connectionBD.Table<Usuario>().Where(x => x.Id == id).FirstOrDefaultAsync();
            return usuario;
        }

        // guarda o actualiza un usuario en la base de datos
        public async Task<int> GuardarUsuario(Usuario usuario)
        {
            // cifra la contraseña del usuario antes de guardarla
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            // verifica si el usuario ya existe en la base de datos
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
            return await _connectionBD.DeleteAsync<Usuario>(id);
        }
    }
}

