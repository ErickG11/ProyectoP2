using ProyectoP2.Models;
using ProyectoP2.Paginas;
using ProyectoP2.Data;
using ProyectoP2.Services;
using System;
using System.Collections.ObjectModel;
using SQLite;

namespace ProyectoP2
{
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();

        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
            CargarProductos();
            ActualizarVisibilidadAdmin();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarProductos();
            ActualizarVisibilidadAdmin();
        }

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
                    Productos.Add(producto);
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

        private void OnCrearProductoClicked(object sender, EventArgs e)
        {
            if (App.EsAdministrador)
            {
                Navigation.PushAsync(new CrearProductoPage());
            }
            else
            {
                DisplayAlert("Acceso Denegado", "Solo los administradores pueden crear productos.", "OK");
            }
        }

        private void OnCrearCategoriaClicked(object sender, EventArgs e)
        {
            if (App.EsAdministrador)
            {
                Navigation.PushAsync(new CrearCategoriaPage());
            }
            else
            {
                DisplayAlert("Acceso Denegado", "Solo los administradores pueden crear categorías.", "OK");
            }
        }

        private void OnCrearDescuentoClicked(object sender, EventArgs e)
        {
            if (App.EsAdministrador)
            {
                Navigation.PushAsync(new CrearDescuentoPage());
            }
            else
            {
                DisplayAlert("Acceso Denegado", "Solo los administradores pueden crear descuentos.", "OK");
            }
        }

        private void OnAgregarAlCarritoClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var producto = button?.BindingContext as Producto;
            if (producto != null)
            {
                CarritoService.AgregarAlCarrito(producto);
                DisplayAlert("Éxito", "Producto agregado al carrito", "OK");
            }
        }

        private async void OnCartButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage());
        }
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
