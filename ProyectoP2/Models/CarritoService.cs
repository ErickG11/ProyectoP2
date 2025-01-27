using ProyectoP2.Models;
using System.Collections.ObjectModel;

namespace ProyectoP2.Services
{
    public static class CarritoService
    {
        private static ObservableCollection<Producto> _productosEnCarrito = new ObservableCollection<Producto>();

        // Agregar un producto al carrito
        public static void AgregarAlCarrito(Producto producto)
        {
            _productosEnCarrito.Add(producto);
        }

        // Obtener los productos en el carrito
        public static ObservableCollection<Producto> GetProductosEnCarrito()
        {
            return _productosEnCarrito;
        }

        // limpiar el carrito después de la compra
        public static void LimpiarCarrito()
        {
            _productosEnCarrito.Clear();
        }

        // Calcular el total del carrito
        public static decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in _productosEnCarrito)
            {
                total += producto.Precio;
            }
            return total;
        }
    }
}