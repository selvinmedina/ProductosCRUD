using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProductos.Models.ViewModels
{
    public class ProductosSelect
    {
        public ProductosSelect()
        {
        }

        public ProductosSelect(int id, string nombre, string marca, string imagen, string categoria, string proveedor, bool estado, bool agotado)
        {
            this.id = id;
            this.nombre = nombre;
            this.marca = marca;
            this.imagen = imagen;
            this.categoria = categoria;
            this.proveedor = proveedor;
            this.estado = estado;
            this.agotado = agotado;
        }

        public int id { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string imagen { get; set; }
        public string categoria { get; set; }
        public string proveedor { get; set; }
        public bool estado { get; set; }
        public bool agotado { get; set; }
    }
}