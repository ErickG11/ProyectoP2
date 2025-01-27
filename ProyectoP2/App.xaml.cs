using ProyectoP2.Data;
using ProyectoP2.Models;
using ProyectoP2.Paginas;
using System.IO;

namespace ProyectoP2
{
    public partial class App : Application
    {
        static SQLiteDatacs _datos;
        static DescuentoAPI _descuentoAPI;

        public static SQLiteDatacs Datos
        {
            get
            {
                if (_datos == null)
                {
                    _datos = new SQLiteDatacs(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Datos.db3"));
                }
                return _datos;
            }
        }

        public static DescuentoAPI DescuentoDB
        {
            get
            {
                if (_descuentoAPI == null)
                {
                    _descuentoAPI = new DescuentoAPI();
                }
                return _descuentoAPI;
            }
        }

        public static Usuario Usuario { get; set; }

        public static bool EsAdministrador { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
