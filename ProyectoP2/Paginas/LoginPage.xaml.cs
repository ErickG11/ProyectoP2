
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
            string contrase�a = txtContrase�a.Text; 


            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrase�a))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "Ok");
                return;
            }

            // llama al m�todo para buscar al usuario 
            var usuario = await App.Datos.UsuarioDataTable.ObtenerUsuario(correo, contrase�a);

            if (usuario == null)
            {
                await DisplayAlert("Alerta", "Correo o contrase�a incorrectos", "Ok");
                return;
            }

            // si las credenciales son v�lidas guarda el usuario en variable global
            App.Usuario = usuario;

            // comprobar si es administrador
            if (correo == "admin@admin.com" && contrase�a == "123")
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
            // Limpia la informaci�n del usuario 
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
