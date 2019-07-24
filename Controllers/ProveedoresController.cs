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
    public class ProveedoresController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;

        public ProveedoresController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDTO>>> Get()
        {
            var proveedores = await inventarioDBContext.Proveedores.ToListAsync();
            var proveedoresDTO = mapper.Map<List<ProveedorDTO>>(proveedores);
            return proveedoresDTO;

        }
        [HttpGet("{id}", Name = "GetProveedor")]
        public async Task<ActionResult<ProveedorDTO>> Get(int id)
        {
            var proveedor = await this.inventarioDBContext.Proveedores.FirstOrDefaultAsync(x => x.CodigoProveedor.Equals(id));
            if (proveedor == null)
            {
                return NotFound();
            }
            var proveedorDTO = this.mapper.Map<ProveedorDTO>(proveedor);
            return proveedorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProveedorCreacionDTO proveedorCreacion)
        {
            var proveedor = mapper.Map<Proveedor>(proveedorCreacion);
            inventarioDBContext.Add(proveedor);
            await inventarioDBContext.SaveChangesAsync();
            var proveedorDTO = mapper.Map<ProveedorDTO>(proveedor);
            return new CreatedAtRouteResult("GetProveedor", new { id = proveedor.CodigoProveedor },
                proveedorDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProveedorCreacionDTO proveedorCreacionDTO)
        {
            var proveedor = this.mapper.Map<Proveedor>(proveedorCreacionDTO);
            proveedor.CodigoProveedor = id;
            this.inventarioDBContext.Entry(proveedor).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProveedorDTO>> Delete(int id)
        {
            var codigoProveedor = await inventarioDBContext.Proveedores.Select(x => x.CodigoProveedor)
                .FirstOrDefaultAsync(x => x == id);
            if (codigoProveedor == default(int))
            {
                return NotFound();
            }
            inventarioDBContext.Remove(new Proveedor { CodigoProveedor = id });
            await inventarioDBContext.SaveChangesAsync();
            return NoContent();
                
        }
        
    }
}
