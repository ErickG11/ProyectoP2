using ProyectoP2.Data;
using ProyectoP2.Models;
using System;

namespace ProyectoP2.Paginas
{
    public partial class CarritoPage : ContentPage
    {

        public double TotalConDescuento { get; set; }

        public CarritoPage()
        {
            InitializeComponent();
            BindingContext = new CarritoPageViewModel();
        }

        private void OnAplicarDescuentoClicked(object sender, EventArgs e)
        {
            string codigo = CodigoDescuentoEntry.Text;

            if (string.IsNullOrEmpty(codigo))
            {
                DisplayAlert("Error", "Por favor ingrese un c�digo de descuento.", "OK");
                return;
            }


            var descuento = App.Datos.DescuentoDataTable.ObtenerDescuentoPorCodigo(codigo);

            if (descuento != null)
            {

                double total = ((CarritoPageViewModel)BindingContext).Total;


                double descuentoAplicado = total * (descuento.Porcentaje / 100);
                TotalConDescuento = total - descuentoAplicado;

                TotalLabel.Text = $"Total con descuento: {TotalConDescuento:C}";

                DisplayAlert("Descuento Aplicado", $"�Se ha aplicado un descuento del {descuento.Porcentaje}%.", "OK");
            }
            else
            {
                DisplayAlert("Error", "El c�digo de descuento ingresado no es v�lido.", "OK");
            }
        }
        private async void OnCartButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage());
        }


        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
