using ProyectoP2.Models;

using ProyectoP2.Paginas;

namespace ProyectoP2;

public partial class CarritoPage : ContentPage
{
    public List<CarritoItemViewModel> CarritoItems { get; set; }
    public decimal TotalCarrito => CarritoItems.Sum(item => item.PrecioTotal);

    public CarritoPage()
    {
        InitializeComponent();
        CargarDatosCarrito();
        BindingContext = this;
    }

    private void CargarDatosCarrito()
    {
        // Simulación de datos iniciales
        CarritoItems = new List<CarritoItemViewModel>
        {
            new CarritoItemViewModel { IdCarrito = 1, NombreProducto = "Producto A", DescripcionProducto = "Descripción A", Cantidad = 2, PrecioUnitario = 10.5m },
            new CarritoItemViewModel { IdCarrito = 2, NombreProducto = "Producto B", DescripcionProducto = "Descripción B", Cantidad = 1, PrecioUnitario = 20.0m }
        };
    }

    private async void OnEliminarProductoClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        int idCarrito = (int)button.CommandParameter;

        // Lógica para eliminar el producto
        var item = CarritoItems.FirstOrDefault(i => i.IdCarrito == idCarrito);
        if (item != null)
        {
            CarritoItems.Remove(item);
            OnPropertyChanged(nameof(CarritoItems));
            OnPropertyChanged(nameof(TotalCarrito));
        }
    }

    private void OnAgregarProductoClicked(object sender, EventArgs e)
    {
        // Validar entradas
        if (string.IsNullOrWhiteSpace(NombreProductoEntry.Text) ||
            string.IsNullOrWhiteSpace(CantidadEntry.Text) ||
            string.IsNullOrWhiteSpace(PrecioUnitarioEntry.Text) ||
            !int.TryParse(CantidadEntry.Text, out int cantidad) ||
            !decimal.TryParse(PrecioUnitarioEntry.Text, out decimal precio))
        {
            DisplayAlert("Error", "Por favor, llena todos los campos correctamente.", "OK");
            return;
        }

        // Agregar nuevo producto
        var nuevoProducto = new CarritoItemViewModel
        {
            IdCarrito = CarritoItems.Count + 1,
            NombreProducto = NombreProductoEntry.Text,
            DescripcionProducto = DescripcionProductoEntry.Text,
            Cantidad = cantidad,
            PrecioUnitario = precio
        };

        CarritoItems.Add(nuevoProducto);
        OnPropertyChanged(nameof(CarritoItems));
        OnPropertyChanged(nameof(TotalCarrito));

        // Limpiar campos
        NombreProductoEntry.Text = string.Empty;
        DescripcionProductoEntry.Text = string.Empty;
        CantidadEntry.Text = string.Empty;
        PrecioUnitarioEntry.Text = string.Empty;
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



public class CarritoItemViewModel
{
    public int IdCarrito { get; set; }
    public string NombreProducto { get; set; }
    public string DescripcionProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal PrecioTotal => Cantidad * PrecioUnitario;
}
