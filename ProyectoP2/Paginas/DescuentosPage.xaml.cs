using ProyectoP2.Data;
using ProyectoP2.Models;

namespace ProyectoP2.Paginas
{
    public partial class DescuentosPage : ContentPage
    {
        public List<Descuento> Descuentos { get; set; }

        private readonly DescuentoAPI _descuentoAPI;

        public DescuentosPage()
        {
            InitializeComponent();
            _descuentoAPI = new DescuentoAPI();
            BindingContext = this;
            CargarDescuentos();
        }

        private void CargarDescuentos()
        {
            Descuentos = _descuentoAPI.ObtenerDescuentos();
            OnPropertyChanged(nameof(Descuentos));
        }

        private async void OnAgregarDescuentoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearDescuentoPage());
        }

        private void OnEliminarDescuentoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var idDescuento = (int)button.CommandParameter;

            _descuentoAPI.EliminarDescuento(idDescuento);
            CargarDescuentos();
        }
    }
}
