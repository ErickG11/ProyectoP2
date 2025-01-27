using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProyectoP2.Services
{
    public static class DescuentoService
    {
        private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "descuentos.json");


        public static List<Descuento> Descuentos { get; private set; } = new List<Descuento>();

        // Método para inicializar la lista cargando los descuentos guardados
        public static void CargarDescuentos()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                Descuentos = JsonSerializer.Deserialize<List<Descuento>>(json) ?? new List<Descuento>();
            }
        }


        private static void GuardarDescuentos()
        {
            string json = JsonSerializer.Serialize(Descuentos);
            File.WriteAllText(FilePath, json);
        }


        public static void AgregarDescuento(string codigo, double porcentaje)
        {
            Descuentos.Add(new Descuento { Codigo = codigo, Porcentaje = porcentaje });
            GuardarDescuentos(); // Guardar cambios en el archivo
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