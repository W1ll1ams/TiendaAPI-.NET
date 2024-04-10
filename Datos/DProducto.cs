using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TiendaAPI.Conexion;
using TiendaAPI.Modelo;

namespace TiendaAPI.Datos
{
    public class DProducto
    {
        Conexionbd cn = new Conexionbd();
        public async Task<List<MProducto>> MostrarProductos()
        {
            try
            {
                var lstProductos = new List<MProducto>();
                using (var sql = new SqlConnection(cn.cadenaSQL()))
                {
                    using (var cmd = new SqlCommand("mostrarProductos", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var item = await cmd.ExecuteReaderAsync())
                        {
                            while (await item.ReadAsync())
                            {
                                var mProducto = new MProducto();
                                mProducto.id = (int)item["id"];
                                mProducto.Nombre = (string)item["Nombre"];
                                mProducto.Descripcion = (string)item["Descripcion"];
                                mProducto.Precio = (decimal)item["Precio"];
                                lstProductos.Add(mProducto);
                            }
                        }
                    }
                }
                return lstProductos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task AgregarProducto(MProducto prmProducto)
        {
            try
            {
                using (var sql = new SqlConnection(cn.cadenaSQL()))
                {
                    using (var cmd = new SqlCommand("agregarProducto", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", prmProducto.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", prmProducto.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", prmProducto.Precio);

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<MProducto> MostrarProductoXId(int id)
        {
            try
            {
                var mProducto = new MProducto();
                using (var sql = new SqlConnection(cn.cadenaSQL()))
                {
                    using (var cmd = new SqlCommand("mostrarProductos", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var item = await cmd.ExecuteReaderAsync())
                        {
                            while (await item.ReadAsync())
                            {
                                mProducto.id = (int)item["id"];
                                mProducto.Nombre = (string)item["Nombre"];
                                mProducto.Descripcion = (string)item["Descripcion"];
                                mProducto.Precio = (decimal)item["Precio"];
                            }
                        }
                    }
                }
                return mProducto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task ModificarProducto(MProducto prmProducto)
        {
            try
            {
                using (var sql = new SqlConnection(cn.cadenaSQL()))
                {
                    using (var cmd = new SqlCommand("modificarProducto", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", prmProducto.id);
                        cmd.Parameters.AddWithValue("@Nombre", prmProducto.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", prmProducto.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", prmProducto.Precio);

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task EliminarProducto(MProducto prmProducto)
        {
            try
            {
                using (var sql = new SqlConnection(cn.cadenaSQL()))
                {
                    using (var cmd = new SqlCommand("eliminarProducto", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", prmProducto.id);

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
