using SQLite;

public class Descuento
{
    [PrimaryKey, AutoIncrement]
    public int IdDescuento { get; set; }
    public string Codigo { get; set; }
    public double Porcentaje { get; set; }  
    public DateTime FechaVencimiento { get; set; }
}
