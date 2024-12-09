using ProyectoP2.Models;
using SQLite;
using System.Collections.ObjectModel;
using ProyectoP2.Paginas;

namespace ProyectoP2
{
    public partial class HomePage : ContentPage
    {
        //lista de productos
        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();

        public HomePage()
        {
            InitializeComponent();
            BindingContext = this; // Establece el contexto de datos
            CargarProductos();
            ActualizarVisibilidadAdmin();
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarProductos();
            ActualizarVisibilidadAdmin(); 
        }

        //  cargar los productos de la base de datos
        private void CargarProductos()
        {
            try
            {
                var database = new SQLiteConnection(Constantes.DatabasePath);
                database.CreateTable<Producto>(); 
                var productos = database.Table<Producto>().ToList();

               
                Productos.Clear();
                foreach (var producto in productos)
                {
                    Productos.Add(producto); // Agregar cada producto 
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudieron cargar los productos: {ex.Message}", "OK");
            }
        }

        private void ActualizarVisibilidadAdmin()
        {
            AdminStack.IsVisible = App.EsAdministrador;
        }

        private async void OnCrearProductoClicked(object sender, EventArgs e)
        {
            if (App.EsAdministrador) 
            {
                await Navigation.PushAsync(new CrearProductoPage());
            }
            else
            {
                await DisplayAlert("Acceso Denegado", "Solo los administradores pueden crear productos.", "OK");
            }
        }

        
        private async void OnCartButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage());
        }

        
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
