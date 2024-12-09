using ProyectoP2.Models;
using SQLite;

namespace ProyectoP2.Paginas
{
    public partial class CrearCategoriaPage : ContentPage
    {
        public CrearCategoriaPage()
        {
            InitializeComponent();
        }

        private async void OnCrearCategoriaClicked(object sender, EventArgs e)
        {
            string categoriaNombre = CategoriaEntry.Text;

            if (string.IsNullOrWhiteSpace(categoriaNombre))
            {
                await DisplayAlert("Error", "El nombre de la categoría no puede estar vacío.", "OK");
                return;
            }

            var nuevaCategoria = new Categoria { Nombre = categoriaNombre };

            try
            {
                var database = new SQLiteConnection(Constantes.DatabasePath);
                database.CreateTable<Categoria>(); // Asegura que la tabla de categorías esté creada
                database.Insert(nuevaCategoria); // Insertar la nueva categoría en la base de datos

                // Enviar mensaje para que CrearProductoPage recargue las categorías
                MessagingCenter.Send(this, "RecargarCategorias");

                await DisplayAlert("Éxito", "Categoría creada exitosamente.", "OK");

                // Volver a la página anterior
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al crear la categoría: {ex.Message}", "OK");
            }
        }
    }
}


