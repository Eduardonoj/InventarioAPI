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
    public class DetalleFacturasController : ControllerBase
    {

        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public DetalleFacturasController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturaDTO>>> Get()
        {
            var detalleFacturas = await this.inventarioDBContext.DetalleFacturas.ToListAsync();
            var detalleFacturasDTO = this.mapper.Map<List<DetalleFacturaDTO>>(detalleFacturas);
            return detalleFacturasDTO;
        }

        [HttpGet("{id}", Name = "GetDetalleFactura")]
        public async Task<ActionResult<FacturaDTO>> Get(int id)
        {
            var factura = await this.inventarioDBContext.Facturas
                .FirstOrDefaultAsync(x => x.NumeroFactura == id);
            if (factura == null)
            {
                return NotFound();
            }
            var facturaDTO = this.mapper.Map<FacturaDTO>(factura);
            return facturaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleFacturaCreacionDTO detalleFacturaCreacionDTO)
        {
            var detalleFactura = this.mapper.Map<DetalleFactura>(detalleFacturaCreacionDTO);
            this.inventarioDBContext.Add(detalleFactura);
            await this.inventarioDBContext.SaveChangesAsync();
            var detalleFacturaDTO = this.mapper.Map<DetalleFacturaDTO>(detalleFactura);
            return new CreatedAtRouteResult("GetDetalleFactura", new { id = detalleFactura.CodigoDetalle },
                detalleFacturaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleFacturaCreacionDTO detalleFacturaCreacionDTO)
        {
            var detalleFactura = this.mapper.Map<DetalleFactura>(detalleFacturaCreacionDTO);
            detalleFactura.CodigoDetalle = id;
            this.inventarioDBContext.Entry(detalleFactura).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleFacturaDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.DetalleFacturas.Select(x => x.CodigoDetalle)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new DetalleFactura { CodigoDetalle = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
