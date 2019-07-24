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
    public class FacturasController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public FacturasController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDTO>>> Get()
        {
            var facturas = await this.inventarioDBContext.Facturas.Include("DetalleFacturas").ToListAsync();
            var facturasDTO = this.mapper.Map<List<FacturaDTO>>(facturas);
            return facturasDTO;
        }

        [HttpGet("{id}", Name = "GetFactura")]
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
        public async Task<ActionResult> Post([FromBody] FacturaCreacionDTO facturaCreacionDTO)
        {
            var factura = this.mapper.Map<Factura>(facturaCreacionDTO);
            this.inventarioDBContext.Add(factura);
            await this.inventarioDBContext.SaveChangesAsync();
            var facturaDTO = this.mapper.Map<FacturaDTO>(factura);
            return new CreatedAtRouteResult("GetFactura", new { id = factura.NumeroFactura },
                facturaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FacturaCreacionDTO facturaCreacionDTO)
        {
            var factura = this.mapper.Map<Factura>(facturaCreacionDTO);
            factura.NumeroFactura = id;
            this.inventarioDBContext.Entry(factura).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FacturaDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.Facturas.Select(x => x.NumeroFactura)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new Factura { NumeroFactura = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
