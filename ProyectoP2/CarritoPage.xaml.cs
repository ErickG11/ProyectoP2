using ProyectoP2.Data;
using ProyectoP2.Models;
using System;

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

            
            TotalConDescuento = 0;

            // Actualizamos el total sin descuento al cargar la página
            var totalSinDescuento = ((CarritoPageViewModel)BindingContext).Total;
            TotalSinDescuentoLabel.Text = $"{totalSinDescuento:C}";

            // Actualizamos el total con descuento a cero al cargar la página
            TotalLabel.Text = $"Total con descuento: {TotalConDescuento:C}";
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
                
                double totalSinDescuento = ((CarritoPageViewModel)BindingContext).Total;

                
                double descuentoAplicado = totalSinDescuento * (descuento.Porcentaje / 100);
                TotalConDescuento = totalSinDescuento - descuentoAplicado;

              
                TotalLabel.Text = $"Total con descuento: {TotalConDescuento:C}";

                DisplayAlert("Descuento Aplicado", $"¡Se ha aplicado un descuento del {descuento.Porcentaje}%.", "OK");
            }
            else
            {
                DisplayAlert("Error", "El código de descuento ingresado no es válido.", "OK");
            }
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
