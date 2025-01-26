using SQLite;
using ProyectoP2.Models;

namespace ProyectoP2.Data
{
    public class DescuentoAPI
    {
        private readonly SQLiteConnection _database;

        public DescuentoAPI()
        {
            _database = new SQLiteConnection(Constantes.DatabasePath);
            _database.CreateTable<Descuento>();
        }

        public void AgregarDescuento(Descuento descuento)
        {
            _database.Insert(descuento);
        }

        public void ActualizarDescuento(Descuento descuento)
        {
            _database.Update(descuento);
        }

        public void EliminarDescuento(int idDescuento)
        {
            _database.Delete<Descuento>(idDescuento);
        }

        public List<Descuento> ObtenerDescuentos()
        {
            return _database.Table<Descuento>().ToList();
        }

        public Descuento ObtenerDescuentoPorCodigo(string codigo)
        {
            return _database.Table<Descuento>().FirstOrDefault(d => d.Codigo == codigo);
        }
    }
}

