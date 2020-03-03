using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiProductos.Models
{
    public class ProductosDBContext: DbContext
    {
        public ProductosDBContext() : base("Productos") { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos{ get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Proveedor>()
                .HasIndex(u => u.prov_nombre)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
                .HasIndex(u => u.cat_nombre)
                .IsUnique();

        }
    }
}