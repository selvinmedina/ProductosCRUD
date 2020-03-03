using ApiProductos.Models;
using ApiProductos.Models.ViewModels;
using ApiProductos.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static ApiProductos.Models.Reader.Reader;

namespace ApiProductos.Controllers
{
    public class ProductosController : ApiController
    {
        private ProductosDBContext db = new ProductosDBContext();

        // GET: api/Productos
        public IQueryable<Producto> GetProductos()
        {
            List<ProductosSelect> productos = null;

            using (SqlConnection connection = General.GetConnection())
            {
                //Comenzar la lectura
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT tps.* FROM Prod.tbProductoSelect tps", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        productos = GetFromReader(reader)
                            .Select(
                            x => new ProductosSelect(
                                (int)x["id"],
                                (string)x["nombre"],
                                (string)x["marca"],
                                (string)x["imagen"],
                                (int)x["categoria"],
                                (int)x["proveedor"],
                                (bool)x["estado"],
                                (bool)x["agotado"]
                                )
                            ).ToList();
                    }
                }
            }
            return db.Productos;
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> GetProducto(int id)
        {
            Producto producto = await db.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.prod_id)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Productos
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productos.Add(producto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = producto.prod_id }, producto);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> DeleteProducto(int id)
        {
            Producto producto = await db.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Productos.Remove(producto);
            await db.SaveChangesAsync();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Productos.Count(e => e.prod_id == id) > 0;
        }

        //public static IEnumerable<IDataRecord> Enumerate(this IDataReader reader)
        //{
        //    while (reader.Read())
        //    {
        //        yield return reader;
        //    }
        //}
    }
}