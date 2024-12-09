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
            // Aseguramos que la visibilidad se actualice cuando la página aparezca
            ActualizarVisibilidadAdmin();
        }

        private void ActualizarVisibilidadAdmin()
        {
            // Si es administrador, mostramos el stack de administrador
            AdminStack.IsVisible = App.EsAdministrador;
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

