using ProyectoP2.Models;

namespace ProyectoP2.Paginas
{
    public partial class EditarUsuarioPage : ContentPage
    {
        Usuario _usuario;

        public EditarUsuarioPage()
        {
            InitializeComponent();
            _usuario = new Usuario();
            this.BindingContext = _usuario;
        }

        private async void btnCrear_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_usuario.Correo) || string.IsNullOrWhiteSpace(_usuario.Contraseña))
            {
                await DisplayAlert("Alerta", "Por favor complete todos los campos", "Ok");
                return;
            }

            // Cifra la contraseña antes de guardarla
            var registro = await App.Datos.UsuarioDataTable.GuardarUsuario(_usuario);

            if (registro > 0)
            {
                await DisplayAlert("Exito", "Usuario creado exitosamente", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el usuario", "Ok");
            }
        }
    }
}
