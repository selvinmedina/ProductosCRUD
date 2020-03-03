using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ApiProductos.Models
{
    [Table("tbProveedores", Schema = "Prod")]
    public class Proveedor
    {
        [Key]
        public int prov_id { get; set; }

        [StringLength(100), MinLength(3), Required, Index(IsUnique = true)]
        public string prov_nombre { get; set; }
    }
}