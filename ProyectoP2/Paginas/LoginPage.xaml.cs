namespace ProyectoP2.Paginas
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            // Si ya hay un usuario se muestra el correo y el bot�n de cerrar sesi�n
            if (App.Usuario != null)
            {
                MostrarSesionIniciada();
            }
        }

        // Metodo para que el usuario complete todos los campos
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

            // Si el usuario existe se guarda y se muestra la sesi�n
            App.Usuario = usuario;
            MostrarSesionIniciada();
            await Navigation.PushAsync(new HomePage()); 
        }

        //ocultar los campos de login y mostrar el correo
        private void MostrarSesionIniciada()
        {
            // Muestra el StackLayout de sesi�n y oculta el de login
            LoginStack.IsVisible = false;
            SessionStack.IsVisible = true;

            // Actualiza el correo en la interfaz 
            lblCorreo.Text = App.Usuario.Correo;

            //Oculta el campo de contrase�a y el bot�n de ingresar
            txtContrase�a.IsVisible = false;
            btnIngresar.IsVisible = false;
        }

        // M�todo para cerrar sesi�n
        private void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            // Elimina al usuario de la sesi�n
            App.Usuario = null;

            // Restaura la interfaz de usuario para permitir nuevo inicio de sesi�n
            LoginStack.IsVisible = true;
            SessionStack.IsVisible = false;

            // Vuelve a mostrar el campo de contrase�a y el bot�n de ingresar
            txtContrase�a.IsVisible = true;
            btnIngresar.IsVisible = true;

            
            Navigation.PopToRootAsync();
        }

        
        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
           
            Navigation.PushAsync(new EditarUsuarioPage());
        }
    }
}


