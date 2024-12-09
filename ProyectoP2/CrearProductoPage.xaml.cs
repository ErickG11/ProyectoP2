using ProyectoP2.Models;
using SQLite;

namespace ProyectoP2.Paginas
{
    public partial class CrearProductoPage : ContentPage
    {
        public CrearProductoPage()
        {
            InitializeComponent();
        }

        private async void OnCrearProductoClicked(object sender, EventArgs e)
        {
            string nombre = NombreEntry.Text;
            string descripcion = DescripcionEditor.Text;
            string precioStr = PrecioEntry.Text;
            string imagenUrl = ImagenUrlEntry.Text; 

            // Validar que todos los campos sean correctos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(precioStr) || string.IsNullOrWhiteSpace(imagenUrl))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            
            if (!decimal.TryParse(precioStr, out decimal precio))
            {
                await DisplayAlert("Error", "El precio debe ser un número válido.", "OK");
                return;
            }

            var producto = new Producto
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                ImagenUrl = imagenUrl  
            };

            try
            {
                var database = new SQLiteConnection(Constantes.DatabasePath);
                database.CreateTable<Producto>(); 
                database.Insert(producto); // Insertar el producto en la base de datos

                await DisplayAlert("Éxito", "Producto creado exitosamente.", "OK");

                
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al guardar el producto: {ex.Message}", "OK");
            }
        }
    }
}
