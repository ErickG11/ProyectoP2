using Microsoft.Maui.Controls;

namespace ProyectoP2.Paginas
{
    public partial class CarritoPage : ContentPage
    {
        public CarritoPage()
        {
            InitializeComponent();
            BindingContext = new CarritoPageViewModel(); // Vincula el ViewModel con la vista
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

