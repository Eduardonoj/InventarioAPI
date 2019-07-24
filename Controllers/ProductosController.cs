using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public ProductosController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
        {
            var productos = await this.inventarioDBContext.Productos.Include("Categoria").Include("TipoEmpaque").ToListAsync();
            var productosDTO = this.mapper.Map<List<ProductoDTO>>(productos);
            return productosDTO;
        }

        [HttpGet("{id}", Name = "GetProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await this.inventarioDBContext.Productos
                .Include("Categoria").Include("TipoEmpaque")
                .FirstOrDefaultAsync(x => x.CodigoProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            var productoDTO = this.mapper.Map<ProductoDTO>(producto);
            return productoDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = this.mapper.Map<Producto>(productoCreacionDTO);
            this.inventarioDBContext.Add(producto);
            await this.inventarioDBContext.SaveChangesAsync();
            var productoDTO = this.mapper.Map<ProductoDTO>(producto);
            return new CreatedAtRouteResult("GetProducto", new { id = producto.CodigoProducto },
                productoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = this.mapper.Map<Producto>(productoCreacionDTO);
            producto.CodigoProducto = id;
            this.inventarioDBContext.Entry(producto).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductoDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.Productos.Select(x => x.CodigoProducto)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new Producto { CodigoProducto = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
