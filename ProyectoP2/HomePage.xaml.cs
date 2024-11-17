namespace ProyectoP2;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnCartButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CarritoPage());
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }


}