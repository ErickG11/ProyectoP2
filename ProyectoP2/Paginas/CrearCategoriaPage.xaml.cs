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

            // Valida que el campo no esté vacío -----------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(categoriaNombre))
            {
                
                await DisplayAlert("Error", "El nombre de la categoría no puede estar vacío.", "OK");
                return;
            }

            // Crea una nueva instancia del modelo `Categoria` con el nombre dado 
            var nuevaCategoria = new Categoria { Nombre = categoriaNombre };

            try
            {
                // Establece la conexión con la base de datos
                var database = new SQLiteConnection(Constantes.DatabasePath);

                // Existencia de la tabla de categorías en la base de datos
                database.CreateTable<Categoria>();

                database.Insert(nuevaCategoria);

                // Envía un mensaje para notificar a otras páginas que deben recargar las categorías disponibles 
                MessagingCenter.Send(this, "RecargarCategorias");

                await DisplayAlert("Éxito", "Categoría creada exitosamente.", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al crear la categoría: {ex.Message}", "OK");
            }
        }
    }
}


