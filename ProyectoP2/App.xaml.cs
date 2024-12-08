using Microsoft.Maui.Controls;
using ProyectoP2.Data;
using ProyectoP2.Models;
using ProyectoP2.Paginas;

namespace ProyectoP2
{
    public partial class App : Application
    {
        static SQLiteDatacs _datos;

        public static SQLiteDatacs Datos
        {
            get
            {
                if(_datos == null)
                {
                    _datos = new SQLiteDatacs(Path.Combine(Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData), "Datos.db3"));
                }
                return _datos;
            }
        }

        public static Usuario Usuario { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }
    }
}
