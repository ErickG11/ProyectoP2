using ProyectoP2.Services;
using ProyectoP2.Models;
using System.Collections.ObjectModel;

namespace ProyectoP2
{
    public partial class CarritoPage : ContentPage
    {
        public ObservableCollection<Producto> ProductosEnCarrito { get; set; }

        public CarritoPage()
        {
            InitializeComponent();
            ProductosEnCarrito = new ObservableCollection<Producto>(CarritoService.ObtenerCarrito());
            BindingContext = this;
        }

        private void OnVaciarCarritoClicked(object sender, EventArgs e)
        {
            CarritoService.VaciarCarrito();
            ProductosEnCarrito.Clear();
            DisplayAlert("Carrito Vacío", "El carrito ha sido vaciado.", "OK");
        }

        private void OnFinalizarCompraClicked(object sender, EventArgs e)
        {
            DisplayAlert("Compra Finalizada", "¡Gracias por tu compra!", "OK");
            CarritoService.VaciarCarrito();
            ProductosEnCarrito.Clear();
        }
    }
}
