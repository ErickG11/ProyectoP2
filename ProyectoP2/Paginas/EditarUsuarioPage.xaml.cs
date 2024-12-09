using ProyectoP2.Models;

namespace ProyectoP2.Paginas
{
    public partial class EditarUsuarioPage : ContentPage 
    {
        // almacenar la instancia del usuario 
        Usuario _usuario;

        public EditarUsuarioPage()
        {
            InitializeComponent(); 

            _usuario = new Usuario(); // Crea una nueva instancia del modelo Usuario

            // permite que los controles XAML se enlacen a las propiedades del usuario
            this.BindingContext = _usuario;
        }

        private async void btnCrear_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_usuario.Correo) || string.IsNullOrWhiteSpace(_usuario.Contraseña))
            {
                await DisplayAlert("Alerta", "Por favor complete todos los campos", "Ok");
                return; 
            }

            var registro = await App.Datos.UsuarioDataTable.GuardarUsuario(_usuario);

            // el usuario se guardó exitosamente o no
            if (registro > 0)
            {
                await DisplayAlert("Éxito", "Usuario creado exitosamente", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el usuario", "Ok");
            }
        }
    }
}

