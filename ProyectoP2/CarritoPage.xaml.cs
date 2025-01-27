using ProyectoP2.Data;
using ProyectoP2.Models;
using System;
using System.Linq;

namespace ProyectoP2.Paginas
{
    public partial class CarritoPage : ContentPage
    {
        public double TotalSinDescuento { get; set; }
        public double TotalConDescuento { get; set; }

        public CarritoPage()
        {
            InitializeComponent();
            BindingContext = new CarritoPageViewModel(); 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           
            var totalSinDescuento = ((CarritoPageViewModel)BindingContext).Total;

            
            TotalSinDescuentoLabel.Text = $"{totalSinDescuento:C}";
            TotalConDescuento = totalSinDescuento;
            TotalLabel.Text = $"{TotalConDescuento:C}";
        }

        private void OnAplicarDescuentoClicked(object sender, EventArgs e)
        {
            string codigo = CodigoDescuentoEntry.Text;

            if (string.IsNullOrEmpty(codigo))
            {
                DisplayAlert("Error", "Por favor ingrese un código de descuento.", "OK");
                return;
            }

            var descuento = App.Datos.DescuentoDataTable.ObtenerDescuentoPorCodigo(codigo);

            if (descuento != null)
            {
                // Obtener el total sin descuento
                double totalSinDescuento = ((CarritoPageViewModel)BindingContext).Total;

                // Calcular el descuento y el total con descuento
                double descuentoAplicado = totalSinDescuento * (descuento.Porcentaje / 100);
                TotalConDescuento = totalSinDescuento - descuentoAplicado;

                // Actualizar el total con descuento en la interfaz
                TotalLabel.Text = $"Total con descuento: {TotalConDescuento:C}";

                DisplayAlert("Descuento Aplicado", $"¡Se ha aplicado un descuento del {descuento.Porcentaje}%.", "OK");
            }
            else
            {
                DisplayAlert("Error", "El código de descuento ingresado no es válido.", "OK");
            }
        }

        private void OnEliminarProductoClicked(object sender, EventArgs e)
        {
            // Obtener el botón que fue presionado
            Button boton = (Button)sender;
            var producto = (Producto)boton.BindingContext;

            // Eliminar el producto de la lista de carrito en el ViewModel
            var viewModel = (CarritoPageViewModel)BindingContext;
            viewModel.ProductosEnCarrito.Remove(producto);

            // Actualizar el total
            TotalSinDescuentoLabel.Text = $"{viewModel.Total:C}";
            TotalConDescuento = viewModel.Total; // Si no hay descuento, mostrar el total sin descuento
            TotalLabel.Text = $"{TotalConDescuento:C}";
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnCartButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage());
        }
    }
}

