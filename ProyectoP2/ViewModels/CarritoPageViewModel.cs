using ProyectoP2.Services;
using ProyectoP2.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using ProyectoP2.Data; // Asegúrate de importar esto para acceder a la base de datos

namespace ProyectoP2.Paginas
{
    public class CarritoPageViewModel : BindableObject
    {
        public ObservableCollection<Producto> ProductosEnCarrito { get; set; } = CarritoService.GetProductosEnCarrito();

        public double Total
        {
            get
            {
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

        private double _descuento;
        public double Descuento
        {
            get => _descuento;
            set
            {
                _descuento = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalConDescuento)); // Actualizar TotalConDescuento
            }
        }

        public double TotalConDescuento
        {
            get
            {
                return Total - (Total * (Descuento / 100));
            }
        }

        public ICommand OnFinalizarCompraClicked { get; }
        public ICommand AplicarDescuentoCommand { get; }

        public CarritoPageViewModel()
        {
            CargarDescuentoGuardado(); // Cargar descuento guardado al iniciar

            OnFinalizarCompraClicked = new Command(FinalizarCompra);
            AplicarDescuentoCommand = new Command(AplicarDescuento);
        }

        private void FinalizarCompra()
        {
            CarritoService.LimpiarCarrito();
            Application.Current.MainPage.DisplayAlert("Compra Realizada", "Su compra se ha realizado con éxito", "OK");
        }

        private void AplicarDescuento()
        {
            if (!string.IsNullOrEmpty(CodigoDescuento))
            {
                // Obtener descuento de la base de datos
                var descuentoAplicado = App.Datos.DescuentoDataTable.ObtenerDescuentoPorCodigo(CodigoDescuento);

                if (descuentoAplicado != null)
                {
                    // Aplicar descuento y guardar preferencias
                    Descuento = descuentoAplicado.Porcentaje;

                    Preferences.Set("CodigoDescuento", CodigoDescuento);
                    Preferences.Set("Descuento", descuentoAplicado.Porcentaje);

                    Application.Current.MainPage.DisplayAlert("Descuento Aplicado", $"Se ha aplicado un descuento del {descuentoAplicado.Porcentaje}%", "OK");
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

        private void CargarDescuentoGuardado()
        {
            // Cargar preferencias guardadas, si existen
            if (Preferences.ContainsKey("CodigoDescuento"))
            {
                CodigoDescuento = Preferences.Get("CodigoDescuento", string.Empty);
                Descuento = Preferences.Get("Descuento", 0.0);
            }
        }

        public void EliminarProducto(Producto producto)
        {
            ProductosEnCarrito.Remove(producto);
            OnPropertyChanged(nameof(Total));  // Esto actualiza el total después de la eliminación
            OnPropertyChanged(nameof(TotalConDescuento)); // También actualiza el total con descuento
        }

    }

}