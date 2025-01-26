using ProyectoP2.Models;
using SQLite;
using System;

namespace ProyectoP2.Paginas
{
    public partial class CrearDescuentoPage : ContentPage
    {
        public CrearDescuentoPage()
        {
            InitializeComponent();
        }

        private async void OnGuardarDescuentoClicked(object sender, EventArgs e)
        {
            try
            {
                var descuento = new Descuento
                {
                    Codigo = CodigoEntry.Text,
                    Porcentaje = Convert.ToInt32(PorcentajeEntry.Text),
                    FechaVencimiento = FechaVencimientoPicker.Date
                };

                var database = new SQLiteConnection(Constantes.DatabasePath);
                database.CreateTable<Descuento>();
                database.Insert(descuento);

                await DisplayAlert("Éxito", "Descuento creado correctamente", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar el descuento: {ex.Message}", "OK");
            }
        }
    }
}
