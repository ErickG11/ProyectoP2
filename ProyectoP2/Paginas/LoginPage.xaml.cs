
namespace ProyectoP2.Paginas
{
    public partial class LoginPage : ContentPage 
    {
        public LoginPage()
        {
            InitializeComponent(); 

            // si ya hay un usuario muestra sesion iniciada
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

            // llama al método para buscar al usuario 
            var usuario = await App.Datos.UsuarioDataTable.ObtenerUsuario(correo, contraseña);

            if (usuario == null)
            {
                await DisplayAlert("Alerta", "Correo o contraseña incorrectos", "Ok");
                return;
            }

            // si las credenciales son válidas guarda el usuario en variable global
            App.Usuario = usuario;

            // comprobar si es administrador
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
            // Limpia la información del usuario 
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
