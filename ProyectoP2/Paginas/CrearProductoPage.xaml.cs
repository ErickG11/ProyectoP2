using ProyectoP2.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace ProyectoP2.Paginas
{
    public partial class CrearProductoPage : ContentPage
    {
        public ObservableCollection<Categoria> Categorias { get; set; } = new ObservableCollection<Categoria>();

        public CrearProductoPage()
        {
            InitializeComponent();

            // Cargar las categor�as desde la base de datos
            CargarCategorias();

            // Suscribirse al mensaje para recargar las categor�as cuando se cree una nueva
            MessagingCenter.Subscribe<CrearCategoriaPage>(this, "RecargarCategorias", (sender) =>
            {
                CargarCategorias();
            });
        }

        private void CargarCategorias()
        {
            try
            {
                var database = new SQLiteConnection(Constantes.DatabasePath);
                database.CreateTable<Categoria>(); // Asegura que la tabla de categor�as est� creada
                var categorias = database.Table<Categoria>().ToList();

                Categorias = new ObservableCollection<Categoria>(categorias);
                CategoriaPicker.ItemsSource = Categorias; // Actualizar el Picker con las categor�as
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

            // Validar que todos los campos sean correctos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(precioStr) || string.IsNullOrWhiteSpace(imagenUrl) || categoriaSeleccionada == null)
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

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
                CategoriaId = categoriaSeleccionada.Id // Asociar la categor�a seleccionada
            };

            try
            {
                var database = new SQLiteConnection(Constantes.DatabasePath);
                database.CreateTable<Producto>(); // Asegura que la tabla de productos est� creada
                database.Insert(producto); // Insertar el producto en la base de datos

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

            // Desuscribirse del mensaje cuando la p�gina desaparece
            MessagingCenter.Unsubscribe<CrearCategoriaPage>(this, "RecargarCategorias");
        }
    }
}

