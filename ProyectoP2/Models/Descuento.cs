using SQLite;

namespace ProyectoP2.Models
{
    public class Descuento
    {
        [PrimaryKey, AutoIncrement]
        public int IdDescuento { get; set; }
        public string Codigo { get; set; }
        public int Porcentaje { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
