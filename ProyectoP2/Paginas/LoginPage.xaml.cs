using ProyectoP2.Paginas;
using ProyectoP2.Models;

namespace ProyectoP2.Paginas
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            // Si ya hay un usuario se muestra la sesi�n iniciada
            if (App.Usuario != null)
            {
                MostrarSesionIniciada();
            }
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;
            string contrase�a = txtContrase�a.Text;

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrase�a))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "Ok");
                return;
            }

            // Verificar si existe un usuario espec�fico
            var usuario = await App.Datos.UsuarioDataTable.ObtenerUsuario(correo, contrase�a);

            if (usuario == null)
            {
                await DisplayAlert("Alerta", "Correo o contrase�a incorrectos", "Ok");
                return;
            }

            // Establecer el usuario global y verificar si es administrador
            App.Usuario = usuario;
            if (correo == "admin@admin.com" && contrase�a == "123")
            {
                App.EsAdministrador = true;
            }
            else
            {
                App.EsAdministrador = false;
            }

            // Redirigir al HomePage
            await Navigation.PushAsync(new HomePage());
        }

        private void MostrarSesionIniciada()
        {
            // Ocultar el formulario de login
            LoginStack.IsVisible = false;

            // Mostrar la informaci�n de la sesi�n
            SessionStack.IsVisible = true;

            // Mostrar el correo del usuario
            lblCorreo.Text = App.Usuario.Correo;
        }

        private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            // Eliminar la informaci�n del usuario actual
            App.Usuario = null;
            App.EsAdministrador = false;

            // Restaurar la interfaz de usuario para permitir un nuevo inicio de sesi�n
            LoginStack.IsVisible = true;
            SessionStack.IsVisible = false;

            await Navigation.PopToRootAsync();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            // Navegar a la p�gina de registro de usuario
            await Navigation.PushAsync(new EditarUsuarioPage());
        }
    }
}
