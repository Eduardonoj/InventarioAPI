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
    public class InventariosController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public InventariosController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDTO>>> Get()
        {
            var inventarios = await this.inventarioDBContext.Inventarios.ToListAsync();
            var inventariosDTO = this.mapper.Map<List<InventarioDTO>>(inventarios);
            return inventariosDTO;
        }
        [HttpGet("{id}", Name = "GetInventario")]
        public async Task<ActionResult<InventarioDTO>> Get(int id)
        {
            var inventario = await this.inventarioDBContext.Inventarios.FirstOrDefaultAsync(x => x.CodigoInventario.Equals(id));
            if (inventario == null)
            {
                return NotFound();
            }
            var inventarioDTO = this.mapper.Map<InventarioDTO>(inventario);
            return inventarioDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InventarioCreacionDTO inventarioCreacionDTO)
        {
            var inventario = this.mapper.Map<Inventario>(inventarioCreacionDTO);
            this.inventarioDBContext.Add(inventario);
            await this.inventarioDBContext.SaveChangesAsync();
            var inventarioDTO = this.mapper.Map<InventarioDTO>(inventario);
            return new CreatedAtRouteResult("GetInventario", new { id = inventario.CodigoInventario },
                inventarioDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] InventarioCreacionDTO inventarioCreacionDTO)
        {
            var inventario = this.mapper.Map<Inventario>(inventarioCreacionDTO);
            inventario.CodigoInventario = id;
            this.inventarioDBContext.Entry(inventario).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InventarioDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.Inventarios.Select(x => x.CodigoInventario)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new Inventario { CodigoInventario = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
