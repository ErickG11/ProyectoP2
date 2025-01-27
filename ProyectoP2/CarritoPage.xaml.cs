using ProyectoP2.Models;
using ProyectoP2.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoP2.Paginas
{
    public partial class CarritoPage : ContentPage
    {
        public List<Producto> ProductosEnCarrito { get; set; }
        public decimal Total { get; set; }

        public CarritoPage()
        {
            InitializeComponent();
            ProductosEnCarrito = CarritoService.GetProductosEnCarrito().ToList();
            Total = CarritoService.CalcularTotal();
            BindingContext = this;
        }

        private void OnFinalizarCompraClicked(object sender, EventArgs e)
        {
            // Limpiar el carrito
            CarritoService.LimpiarCarrito();

            // Mostrar alerta de éxito
            DisplayAlert("Compra Realizada", "Su compra ha sido realizada exitosamente.", "OK");

            // Regresar a la página principal o de inicio
            Navigation.PopToRootAsync();
        }
    }
}
