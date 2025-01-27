using ProyectoP2.Services;
using ProyectoP2.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ProyectoP2.Paginas
{
    public class CarritoPageViewModel : BindableObject
    {
        public ObservableCollection<Producto> ProductosEnCarrito { get; set; } = CarritoService.GetProductosEnCarrito();

        public double Total
        {
            get
            {
                // Sumar los precios de todos los productos en el carrito
                return ProductosEnCarrito.Sum(p => (double)p.Precio);
            }
        }

        private string _codigoDescuento;
        public string CodigoDescuento
        {
            get => _codigoDescuento;
            set
            {
                _codigoDescuento = value;
                OnPropertyChanged();
            }
        }

        // La propiedad TotalConDescuento aplica el descuento al total
        public double TotalConDescuento
        {
            get
            {
               
                return Total - (Total * (Descuento / 100));
            }
        }

        private double _descuento;
        public double Descuento
        {
            get => _descuento;
            set
            {
                _descuento = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalConDescuento)); 
            }
        }

        public ICommand OnFinalizarCompraClicked { get; }
        public ICommand AplicarDescuentoCommand { get; }

        public CarritoPageViewModel()
        {
            OnFinalizarCompraClicked = new Command(FinalizarCompra);
            AplicarDescuentoCommand = new Command(AplicarDescuento);
        }

        private void FinalizarCompra()
        {
            CarritoService.LimpiarCarrito();
            Application.Current.MainPage.DisplayAlert("Compra Realizada", "Su compra se ha realizado con éxito", "OK");
        }

        // Método para aplicar descuento
        private void AplicarDescuento()
        {
            if (!string.IsNullOrEmpty(CodigoDescuento))
            {
                
                double descuentoAplicado = DescuentoService.ObtenerDescuento(CodigoDescuento);

                if (descuentoAplicado > 0)
                {
                    // Aplicamos el descuento
                    Descuento = descuentoAplicado;

                    // Mostramos una alerta confirmando que se ha aplicado el descuento
                    Application.Current.MainPage.DisplayAlert("Descuento Aplicado", $"Se ha aplicado un descuento del {descuentoAplicado}%", "OK");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Código Inválido", "El código de descuento ingresado no es válido", "OK");
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Por favor ingrese un código de descuento", "OK");
            }
        }
    }
}
