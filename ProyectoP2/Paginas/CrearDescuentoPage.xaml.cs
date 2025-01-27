using ProyectoP2.Services;
using System;
using Microsoft.Maui.Controls;

namespace ProyectoP2.Paginas
{
    public partial class CrearDescuentoPage : ContentPage
    {
        public CrearDescuentoPage()
        {
            InitializeComponent();
        }

        // crear el descuento
        private void OnCrearDescuentoClicked(object sender, EventArgs e)
        {
            string codigoDescuento = CodigoDescuentoEntry.Text;
            double porcentajeDescuento = 0;

            if (string.IsNullOrEmpty(codigoDescuento) || !double.TryParse(DescuentoEntry.Text, out porcentajeDescuento))
            {
                DisplayAlert("Error", "Por favor ingrese un código válido y un porcentaje.", "OK");
                return;
            }

            // llamar al servicio para agregar el descuento
            DescuentoService.AgregarDescuento(codigoDescuento, porcentajeDescuento);

            DisplayAlert("Descuento Creado", "El descuento ha sido creado con éxito.", "OK");
            Navigation.PopAsync();
        }
    }
}
