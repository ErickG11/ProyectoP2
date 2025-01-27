using ProyectoP2.Data;
using ProyectoP2.Models;
using System;

namespace ProyectoP2.Paginas
{
    public partial class CrearDescuentoPage : ContentPage
    {
        public CrearDescuentoPage()
        {
            InitializeComponent();
        }

        private void OnCrearDescuentoClicked(object sender, EventArgs e)
        {
            string codigo = CodigoEntry.Text;
            double porcentaje;

            if (string.IsNullOrEmpty(codigo) || !double.TryParse(PorcentajeEntry.Text, out porcentaje))
            {
                DisplayAlert("Error", "Por favor ingrese un código y un porcentaje válidos.", "OK");
                return;
            }

            var descuento = new Descuento
            {
                Codigo = codigo,
                Porcentaje = porcentaje
            };

            App.Datos.DescuentoDataTable.AgregarDescuento(descuento);

            DisplayAlert("Éxito", "Descuento creado exitosamente.", "OK");

            CodigoEntry.Text = "";
            PorcentajeEntry.Text = "";
        }
    }
}