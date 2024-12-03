using System.ComponentModel.DataAnnotations;

namespace ProyectoP2.Models
{
    public class Administrador
    {
        [Key]
        public int IdAdministrador { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        [StringLength(50)]
        [EmailAddress]
        public string Correo { get; set; }
        [StringLength(100)]
        public string Contraseña { get; set; }
    }
}
