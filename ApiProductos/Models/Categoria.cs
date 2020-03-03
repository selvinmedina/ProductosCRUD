using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProductos.Models
{
    [Table("tbCategorias", Schema = "Prod")]
    public class Categoria
    {
        [Key]
        public int cat_id { get; set; }

        [StringLength(100), MinLength(3), Required, Index(IsUnique = true)]
        public string cat_nombre { get; set; }
    }
}