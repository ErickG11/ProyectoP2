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

            // Valida que el campo no est� vac�o -----------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(categoriaNombre))
            {
                
                await DisplayAlert("Error", "El nombre de la categor�a no puede estar vac�o.", "OK");
                return;
            }

            // Crea una nueva instancia del modelo `Categoria` con el nombre dado 
            var nuevaCategoria = new Categoria { Nombre = categoriaNombre };

            try
            {
                // Establece la conexi�n con la base de datos
                var database = new SQLiteConnection(Constantes.DatabasePath);

                // Existencia de la tabla de categor�as en la base de datos
                database.CreateTable<Categoria>();

                database.Insert(nuevaCategoria);

                // Env�a un mensaje para notificar a otras p�ginas que deben recargar las categor�as disponibles 
                MessagingCenter.Send(this, "RecargarCategorias");

                await DisplayAlert("�xito", "Categor�a creada exitosamente.", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al crear la categor�a: {ex.Message}", "OK");
            }
        }
    }
}


