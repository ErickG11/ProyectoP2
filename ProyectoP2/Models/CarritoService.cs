using ProyectoP2.Models;
using System.Collections.Generic;

namespace ProyectoP2.Services
{
    public static class CarritoService
    {
        private static List<Producto> carrito = new List<Producto>();

        // Método para agregar un producto al carrito
        public static void AgregarAlCarrito(Producto producto)
        {
            carrito.Add(producto);
        }

        // Método para obtener todos los productos en el carrito
        public static List<Producto> ObtenerCarrito()
        {
            return carrito;
        }

        // Método para aplicar un descuento a todos los productos en el carrito
        public static void AplicarDescuento(decimal porcentaje)
        {
            foreach (var producto in carrito)
            {
                producto.Precio -= producto.Precio * (porcentaje / 100);
            }
        }

        // Método para vaciar el carrito
        public static void VaciarCarrito()
        {
            carrito.Clear();
        }
    }
}

