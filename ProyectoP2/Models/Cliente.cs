using System.ComponentModel.DataAnnotations;

namespace ProyectoP2.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        [EmailAddress]
        public string Correo { get; set; }
        [StringLength(100)]
        public string Contraseña { get; set; }

    }
}
