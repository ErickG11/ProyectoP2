using System.Collections.Generic;

namespace ProyectoP2.Services
{
    public static class DescuentoService
    {
        // Lista de descuentos creados
        public static List<Descuento> Descuentos { get; set; } = new List<Descuento>();

        // Método para agregar un descuento
        public static void AgregarDescuento(string codigo, double porcentaje)
        {
            Descuentos.Add(new Descuento { Codigo = codigo, Porcentaje = porcentaje });
        }

        // Método para obtener un descuento por código
        public static double ObtenerDescuento(string codigo)
        {
            var descuento = Descuentos.Find(d => d.Codigo == codigo);
            return descuento != null ? descuento.Porcentaje : 0;
        }
    }

    // Clase para representar un descuento
    public class Descuento
    {
        public string Codigo { get; set; }
        public double Porcentaje { get; set; }
    }
}
