using ProyectoP2.Paginas;

namespace ProyectoP2
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            ActualizarVisibilidadAdmin();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            ActualizarVisibilidadAdmin();
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
