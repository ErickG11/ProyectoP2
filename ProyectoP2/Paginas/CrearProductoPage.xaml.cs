using ProyectoP2.Models; 
using SQLite;
using System.Collections.ObjectModel; 

namespace ProyectoP2.Paginas
{
    public partial class CrearProductoPage : ContentPage 
    {
        // propiedad para almacenar la lista de categor�as Utilizamos ObservableCollection para notificar cambios din�micos en la lista
        public ObservableCollection<Categoria> Categorias { get; set; } = new ObservableCollection<Categoria>();

        public CrearProductoPage()
        {
            InitializeComponent(); 

            
            CargarCategorias();

            
            MessagingCenter.Subscribe<CrearCategoriaPage>(this, "RecargarCategorias", (sender) =>
            {
                CargarCategorias(); // recargar las categor�as cuando se reciba el mensaje
            });
        }

        // cargar las categor�as desde la base de datos
        private void CargarCategorias()
        {
            try
            {
                // conexi�n con la base de datos SQLite.
                var database = new SQLiteConnection(Constantes.DatabasePath);

                database.CreateTable<Categoria>();

                // convierte las categorias en una lista
                var categorias = database.Table<Categoria>().ToList();

                Categorias = new ObservableCollection<Categoria>(categorias);

                CategoriaPicker.ItemsSource = Categorias;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudieron cargar las categor�as: {ex.Message}", "OK");
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
                await DisplayAlert("Error", "El precio debe ser un n�mero v�lido.", "OK");
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
                // conexi�n con la base de datos para ingresar el producto
                var database = new SQLiteConnection(Constantes.DatabasePath);

                // crea la tabla de productos 
                database.CreateTable<Producto>();

                database.Insert(producto);

                await DisplayAlert("�xito", "Producto creado exitosamente.", "OK");

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


