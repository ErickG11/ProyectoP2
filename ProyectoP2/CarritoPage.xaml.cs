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
    }
}
