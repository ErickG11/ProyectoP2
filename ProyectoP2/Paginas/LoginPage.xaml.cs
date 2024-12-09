using ProyectoP2.Paginas;
using ProyectoP2.Models;

namespace ProyectoP2.Paginas
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            // Si ya hay un usuario se muestra la sesión iniciada
            if (App.Usuario != null)
            {
                MostrarSesionIniciada();
            }
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;
            string contraseña = txtContraseña.Text;

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contraseña))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "Ok");
                return;
            }

            // Verificar si existe un usuario específico
            var usuario = await App.Datos.UsuarioDataTable.ObtenerUsuario(correo, contraseña);

            if (usuario == null)
            {
                await DisplayAlert("Alerta", "Correo o contraseña incorrectos", "Ok");
                return;
            }

            // Establecer el usuario global y verificar si es administrador
            App.Usuario = usuario;
            if (correo == "admin@admin.com" && contraseña == "123")
            {
                App.EsAdministrador = true;
            }
            else
            {
                App.EsAdministrador = false;
            }

          
            await Navigation.PushAsync(new HomePage());
        }

        private void MostrarSesionIniciada()
        {
           
            LoginStack.IsVisible = false;

          
            SessionStack.IsVisible = true;

           
            lblCorreo.Text = App.Usuario.Correo;
        }

        private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            
            App.Usuario = null;
            App.EsAdministrador = false;

          
            LoginStack.IsVisible = true;
            SessionStack.IsVisible = false;

            await Navigation.PopToRootAsync();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new EditarUsuarioPage());
        }
    }
}
