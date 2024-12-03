using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class Usuario : IdentityUser
{
    [StringLength(50)]
    public string Nombre { get; set; }

    [StringLength(50)]
    public string Apellido { get; set; }
}
