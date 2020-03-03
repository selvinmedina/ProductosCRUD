using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiProductos.Models
{
    [Table("tbProductos", Schema = "Prod")]
    public class Producto
    {
        [Key]
        public int prod_id { get; set; }

        [StringLength(150), MinLength(3), Required, Index(IsUnique = true)]
        public string prod_nombre { get; set; }

        [StringLength(100), MinLength(3), Required]
        public string prod_marca { get; set; }

        [Required]
        public int categoria_id { get; set; }

        [Required]
        public int proveedor_id { get; set; }

        [Required, StringLength(500)]
        public string prod_image { get; set; }

        public bool prod_activo { get; set; }

        public bool prod_agotado { get; set; }

        [ForeignKey("categoria_id")]
        public virtual Categoria Categoria { get; set; }
        [ForeignKey("proveedor_id")]
        public virtual Proveedor Proveedor { get; set; }
    }
}