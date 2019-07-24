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
    public class TelefonoProveedoresController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public TelefonoProveedoresController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoProveedorDTO>>> Get()
        {
            var telefonoProveedores = await this.inventarioDBContext.TelefonoProveedores.ToListAsync();
            var telefonoProveedoresDTO = this.mapper.Map<List<TelefonoProveedorDTO>>(telefonoProveedores);
            return telefonoProveedoresDTO;
        }
        [HttpGet("{id}", Name = "GetTelefonoProveedor")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Get(int id)
        {
            var telefonoProveedor = await this.inventarioDBContext.TelefonoProveedores.FirstOrDefaultAsync(x => x.CodigoTelefono.Equals(id));
            if (telefonoProveedor == null)
            {
                return NotFound();
            }
            var telefonoProveedorDTO = this.mapper.Map<TelefonoProveedorDTO>(telefonoProveedor);
            return telefonoProveedorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoProveedorCreacionDTO telefonoProveedorCreacionDTO)
        {
            var telefonoProveedor = this.mapper.Map<TelefonoProveedor>(telefonoProveedorCreacionDTO);
            this.inventarioDBContext.Add(telefonoProveedor);
            await this.inventarioDBContext.SaveChangesAsync();
            var telefonoProveedorDTO = this.mapper.Map<TelefonoProveedorDTO>(telefonoProveedor);
            return new CreatedAtRouteResult("GetTelefonoProveedor", new { id = telefonoProveedor.CodigoTelefono },
                telefonoProveedorDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoProveedorCreacionDTO telefonoProveedorCreacionDTO)
        {
            var telefonoProveedor = this.mapper.Map<TelefonoProveedor>(telefonoProveedorCreacionDTO);
            telefonoProveedor.CodigoTelefono = id;
            this.inventarioDBContext.Entry(telefonoProveedor).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.TelefonoProveedores.Select(x => x.CodigoTelefono)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new TelefonoProveedor { CodigoTelefono = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
