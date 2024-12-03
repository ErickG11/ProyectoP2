using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2.Models
{
    internal class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Categoria? categoria { get; set; }

        [ForeignKey("categoriaId")]
        public int CategoriaId { get; set; }

        public float Precio { get; set; }
        public string ImagenUrl { get; set; }

    }
}
