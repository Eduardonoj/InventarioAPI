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
    public class ComprasController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public ComprasController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDTO>>> Get()
        {
            var compras = await this.inventarioDBContext.Compras.Include("DetalleCompras").ToListAsync();
            var comprasDTO = this.mapper.Map<List<CompraDTO>>(compras);
            return comprasDTO;
        }

        [HttpGet("{id}", Name = "GetCompra")]
        public async Task<ActionResult<CompraDTO>> Get(int id)
        {
            var compra = await this.inventarioDBContext.Compras.Include("DetalleCompra")
                .FirstOrDefaultAsync(x => x.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }
            var compraDTO = this.mapper.Map<CompraDTO>(compra);
            return compraDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CompraCreacionDTO compraCreacionDTO)
        {
            var compra= this.mapper.Map<Compra>(compraCreacionDTO);
            this.inventarioDBContext.Add(compra);
            await this.inventarioDBContext.SaveChangesAsync();
            var compraDTO = this.mapper.Map<CompraDTO>(compra);
            return new CreatedAtRouteResult("GetCompra", new { id = compra.IdCompra },
                compraDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CompraCreacionDTO compraCreacionDTO)
        {
            var compra = this.mapper.Map<Compra>(compraCreacionDTO);
            compra.IdCompra = id;
            this.inventarioDBContext.Entry(compra).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CompraDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.Compras.Select(x => x.IdCompra)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new Compra { IdCompra = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
