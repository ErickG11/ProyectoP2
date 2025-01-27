using ProyectoP2.Models;
using ProyectoP2.Services;
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
                return ProductosEnCarrito.Sum(p => (double)p.Precio); 
            }
        }

        
        public ICommand OnFinalizarCompraClicked { get; }

       
        public CarritoPageViewModel()
        {
            
            OnFinalizarCompraClicked = new Command(FinalizarCompra);
        }

       
        private void FinalizarCompra()
        {
            
            CarritoService.LimpiarCarrito();

            
            Application.Current.MainPage.DisplayAlert("Compra Realizada", "Su compra se ha realizado con éxito", "OK");
        }
    }
}
