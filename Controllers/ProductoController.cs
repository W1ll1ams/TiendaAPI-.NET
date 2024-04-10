using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaAPI.Datos;
using TiendaAPI.Modelo;

namespace TiendaAPI.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MProducto>>> obtenerProductos()
        {
            var funcion = new DProducto();
            var lstProductos = await funcion.MostrarProductos();
            return lstProductos;
        }

        [HttpPost]
        public async Task InsertarProducto([FromBody] MProducto objProducto)
        {
            var funcion = new DProducto();
            await funcion.AgregarProducto(objProducto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MProducto>> obtenerProducto(int id)
        {
            var funcion = new DProducto();
            var objProducto = await funcion.MostrarProductoXId(id);
            return objProducto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditarProducto(int id, [FromBody] MProducto objProducto)
        {
            var funcion = new DProducto();
            var objProductoActual = await funcion.MostrarProductoXId(id);
            objProducto.Nombre = objProducto.Nombre ?? objProductoActual.Nombre;
            objProducto.Descripcion = objProducto.Descripcion ?? objProductoActual.Descripcion;
            objProducto.Precio = objProducto.Precio != 0.0M ? objProducto.Precio : objProductoActual.Precio;
            objProducto.id = id;
            await funcion.ModificarProducto(objProducto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarProducto(int id)
        {
            var funcion = new DProducto();
            var objProducto = new MProducto();
            objProducto.id = id;
            await funcion.EliminarProducto(objProducto);
            return NoContent();
        }
    }
}
