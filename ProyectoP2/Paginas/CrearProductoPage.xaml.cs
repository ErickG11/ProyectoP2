using ProyectoP2.Models; 
using SQLite;
using System.Collections.ObjectModel; 

namespace ProyectoP2.Paginas
{
    public partial class CrearProductoPage : ContentPage 
    {
        // almacenar la lista de categorías Utilizamos ObservableCollection para notificar cambios dinámicos en la lista
        public ObservableCollection<Categoria> Categorias { get; set; } = new ObservableCollection<Categoria>();

        public CrearProductoPage()
        {
            InitializeComponent(); 

            
            CargarCategorias();

            
            MessagingCenter.Subscribe<CrearCategoriaPage>(this, "RecargarCategorias", (sender) =>
            {
                CargarCategorias(); 
            });
        }

        // categorías cargadas desde la base de datos
        private void CargarCategorias()
        {
            try
            {
                var database = new SQLiteConnection(Constantes.DatabasePath);

                database.CreateTable<Categoria>();

                var categorias = database.Table<Categoria>().ToList();

                Categorias = new ObservableCollection<Categoria>(categorias);

                CategoriaPicker.ItemsSource = Categorias;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudieron cargar las categorías: {ex.Message}", "OK");
            }
        }

        private async void OnCrearProductoClicked(object sender, EventArgs e)
        {
            string nombre = NombreEntry.Text;
            string descripcion = DescripcionEditor.Text;
            string precioStr = PrecioEntry.Text;
            string imagenUrl = ImagenUrlEntry.Text;
            Categoria categoriaSeleccionada = CategoriaPicker.SelectedItem as Categoria;

            // validar que los campos esten complejos
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(descripcion) ||
                string.IsNullOrWhiteSpace(precioStr) ||
                string.IsNullOrWhiteSpace(imagenUrl) ||
                categoriaSeleccionada == null)
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            // verificar que el precio sea valido
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
                ImagenUrl = imagenUrl,
                CategoriaId = categoriaSeleccionada.Id 
            };

            try
            {
                // conexión con la base de datos para ingresar el producto
                var database = new SQLiteConnection(Constantes.DatabasePath);

                // crea la tabla de productos 
                database.CreateTable<Producto>();

                database.Insert(producto);

                await DisplayAlert("Éxito", "Producto creado exitosamente.", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al guardar el producto: {ex.Message}", "OK");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // evitar posibles fugas de memoria.
            MessagingCenter.Unsubscribe<CrearCategoriaPage>(this, "RecargarCategorias");
        }
    }
}


