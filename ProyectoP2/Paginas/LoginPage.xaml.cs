namespace ProyectoP2.Paginas
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            // Si ya hay un usuario se muestra el correo y el botón de cerrar sesión
            if (App.Usuario != null)
            {
                MostrarSesionIniciada();
            }
        }

        // Metodo para que el usuario complete todos los campos
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

            // Si el usuario existe se guarda y se muestra la sesión
            App.Usuario = usuario;
            MostrarSesionIniciada();
            await Navigation.PushAsync(new HomePage()); 
        }

        //ocultar los campos de login y mostrar el correo
        private void MostrarSesionIniciada()
        {
            // Muestra el StackLayout de sesión y oculta el de login
            LoginStack.IsVisible = false;
            SessionStack.IsVisible = true;

            // Actualiza el correo en la interfaz 
            lblCorreo.Text = App.Usuario.Correo;

            //Oculta el campo de contraseña y el botón de ingresar
            txtContraseña.IsVisible = false;
            btnIngresar.IsVisible = false;
        }

        // Método para cerrar sesión
        private void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            // Elimina al usuario de la sesión
            App.Usuario = null;

            // Restaura la interfaz de usuario para permitir nuevo inicio de sesión
            LoginStack.IsVisible = true;
            SessionStack.IsVisible = false;

            // Vuelve a mostrar el campo de contraseña y el botón de ingresar
            txtContraseña.IsVisible = true;
            btnIngresar.IsVisible = true;

            
            Navigation.PopToRootAsync();
        }

        
        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
           
            Navigation.PushAsync(new EditarUsuarioPage());
        }
    }
}


