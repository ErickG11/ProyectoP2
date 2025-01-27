using System.Collections.Generic;

namespace ProyectoP2.Services
{
    public static class DescuentoService
    {
        public static List<Descuento> Descuentos { get; set; } = new List<Descuento>();

        // agregar un descuento
        public static void AgregarDescuento(string codigo, double porcentaje)
        {
            Descuentos.Add(new Descuento { Codigo = codigo, Porcentaje = porcentaje });
        }

        public static double ObtenerDescuento(string codigo)
        {
            var descuento = Descuentos.Find(d => d.Codigo == codigo);
            return descuento != null ? descuento.Porcentaje : 0;
        }
    }

    public class Descuento
    {
        public string Codigo { get; set; }
        public double Porcentaje { get; set; }
    }
}
