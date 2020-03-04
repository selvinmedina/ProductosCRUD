﻿using ApiProductos.App_Start;
using ApiProductos.Models;
using ApiProductos.Models.ViewModels;
using ApiProductos.Utilities;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.Http.Description;
using static ApiProductos.Models.Reader.Reader;

namespace ApiProductos.Controllers
{
    public class ProductosController : ApiController
    {
        private ProductosDBContext db = new ProductosDBContext();

        // GET: api/Productos
        public async Task<ResponseHelper<List<ProductosSelect>>> GetProductos()
        {
            ResponseHelper<List<ProductosSelect>> rs = new ResponseHelper<List<ProductosSelect>>();
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection connection = General.GetConnection())
                    {
                        //Comenzar la lectura
                        connection.Open();
                        rs.Result = new SqlCommand(
                            "SELECT tps.* FROM Prod.tbProductoSelect tps", //Consulta que quiero hacer a la base de datos
                            connection //Cadena de conexion
                            )
                            .ExecuteReader() //Ejecutar el SqlCommand
                            .Enumerate() //Iterar los resultados
                            .Select( //Crear instancias de objetos para el listado del tipo de retorno
                                   x => new ProductosSelect(
                                       (int)x["id"],
                                       (string)x["nombre"],
                                       (string)x["marca"],
                                       (string)x["imagen"],
                                       (string)x["categoria"],
                                       (string)x["proveedor"],
                                       (bool)x["estado"],
                                       (bool)x["agotado"]
                                       )
                                   ).ToList();

                        rs.SetResponse(true, Status.Success); //Poner como success la respuesta
                    }
                });
            }
            catch (Exception)
            {
                rs.SetResponse(false, Status.Error);
            }
            return rs;
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Producto))]
        public async Task<ResponseHelper<Producto>> GetProducto(int id)
        {
            #region Declaracion de variables
            ResponseHelper<Producto> rs = new ResponseHelper<Producto>();
            Producto producto = await db.Productos.FindAsync(id);
            #endregion

            //si es nulo que retorne Error y un status code not found
            if (producto == null)
                return rs.SetResponse(false, Status.Error, HttpStatusCode.NotFound);

            //Enviar el produto
            rs.SetResponse(true, Status.Success, HttpStatusCode.InternalServerError);
            rs.Result = producto;
            return rs;
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public async Task<ResponseHelper<Producto>> PutProducto(int id, Producto producto)
        {
            ResponseHelper<Producto> rs = new ResponseHelper<Producto>();
            if (!ModelState.IsValid)
            {
                rs.SetValidations(ModelState.GetErrors());
                return rs.SetResponse(false, Status.Success, HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry(producto).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (rs.SetResponse(false, Status.Error, HttpStatusCode.InternalServerError));
            }

            rs.SetResponse(true, Status.Success);
            rs.Result = producto;

            return rs;
        }

        // POST: api/Productos
        [ResponseType(typeof(ModelProductos))]
        public async Task<ResponseHelper<ModelProductos>> PostProducto(ModelProductos producto)
        {
            #region Variables
            ResponseHelper<ModelProductos> rs = new ResponseHelper<ModelProductos>();
            int result = 0;
            #endregion

            #region Invalido
            if (!ModelState.IsValid)
            {
                rs.SetValidations(ModelState.GetErrors());
                return rs.SetResponse(false, Status.Error, HttpStatusCode.BadRequest);
            }
            #endregion

            try
            {
                await Task.Run(() =>
               {
                   //SqlTransaction transaccion = null; //Usar la transaccion
                   using (SqlConnection connection = General.GetConnection())
                   {
                       connection.Open();
                       using (SqlCommand command = new SqlCommand("Prod.tbProducto_Insert", connection))
                       {
                           command.CommandType = CommandType.StoredProcedure; //Comando de tipo procedimiento
                           using (TransactionScope transaction = new TransactionScope())
                           {
                               command.Parameters.AddWithValue("@prod_nombre", SqlDbType.NVarChar).Value = producto.prod_nombre;
                               command.Parameters.AddWithValue("@prod_marca", SqlDbType.NVarChar).Value = producto.prod_marca;
                               command.Parameters.AddWithValue("@categoria_id", SqlDbType.Int).Value = producto.categoria_id;
                               command.Parameters.AddWithValue("@proveedor_id", SqlDbType.Int).Value = producto.proveedor_id;
                               command.Parameters.AddWithValue("@prod_image", SqlDbType.Bit).Value = producto.prod_image;
                               command.Parameters.AddWithValue("@prod_agotado", SqlDbType.Bit).Value = producto.prod_agotado;

                               Result resultado = command
                               .ExecuteReader()
                               .Enumerate()
                               .Select(
                                   x => new Result
                                   {
                                       Id = (int)x["Id"],
                                       MensajeError = (string)x["MensajeError"]
                                   })
                                   .FirstOrDefault();

                               if (resultado?.Id == -1)
                                   rs.SetResponse(false, Status.Error, HttpStatusCode.InternalServerError);
                               else
                               {
                                   producto.prod_id = resultado.Id;
                                   rs.Result = producto;
                                   rs.SetResponse(true, Status.Success, HttpStatusCode.Created);
                               }
                           }
                       }
                   }
               });
            }
            catch (Exception)
            {
                return rs.SetResponse(false, Status.Error, HttpStatusCode.InternalServerError);
            }

            return rs;
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Producto))]
        public async Task<ResponseHelper> DeleteProducto(int id)
        {
            ResponseHelper rs = new ResponseHelper();
            Producto producto = await db.Productos.FindAsync(id);
            if (producto == null)
            {
                return rs.SetResponse(false, Status.Error, HttpStatusCode.BadRequest);

            }

            try
            {
                db.Productos.Remove(producto);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return rs.SetResponse(false, Status.Error, HttpStatusCode.InternalServerError);
            }

            return rs.SetResponse(true, Status.Success, HttpStatusCode.OK); ;
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
    }
}