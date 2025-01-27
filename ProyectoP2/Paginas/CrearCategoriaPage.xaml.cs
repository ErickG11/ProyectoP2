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
                
                await DisplayAlert("Error", "El nombre de la categor�a no puede estar vac�o.", "OK");
                return;
            }

           
            var nuevaCategoria = new Categoria { Nombre = categoriaNombre };

            try
            {
                
                var database = new SQLiteConnection(Constantes.DatabasePath);

              
                database.CreateTable<Categoria>();

                database.Insert(nuevaCategoria);

            
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


