using System;

namespace ApiProductos.Models.ViewModels
{
    public class Productos
    {
        public Productos(int id, string nombre, string marca, string imagen, int categoria, int proveedor, bool estado, bool agotado, decimal precio, string codigo)
        {
            this.id = id;
            this.nombre = nombre;
            this.marca = marca;
            this.imagen = imagen;
            this.categoria = categoria;
            this.proveedor = proveedor;
            this.estado = estado;
            this.agotado = agotado;
            this.codigo = codigo;
            this.precio = precio;
        }

        public int id { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string imagen { get; set; }
        public int categoria { get; set; }
        public int proveedor { get; set; }
        public bool estado { get; set; }
        public bool agotado { get; set; }
        public decimal precio { get; set; }
        public string codigo { get; set; }

    }

    public class ModelProductos
    {//Productos crear o actualizar
        public int prod_id { get; set; }

        public string prod_nombre { get; set; }

        public string prod_marca { get; set; }

        public int categoria_id { get; set; }

        public int proveedor_id { get; set; }

        public string prod_image { get; set; }

        public bool prod_agotado { get; set; }

        public decimal prod_precio { get; set; }

        public string prod_codigo { get; set; }

    }

}