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
    public class TelefonoClientesController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public TelefonoClientesController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoClienteDTO>>> Get()
        {
            var telefonoClientes = await this.inventarioDBContext.TelefonoClientes.ToListAsync();
            var telefonoClientesDTO = this.mapper.Map<List<TelefonoClienteDTO>>(telefonoClientes);
            return telefonoClientesDTO;
        }
        [HttpGet("{id}", Name = "GetTelefonoCliente")]
        public async Task<ActionResult<TelefonoClienteDTO>> Get(int id)
        {
            var telefonoCliente = await this.inventarioDBContext.TelefonoClientes.FirstOrDefaultAsync(x => x.Nit.Equals(id));
            if (telefonoCliente == null)
            {
                return NotFound();
            }
            var telefonoClienteDTO = this.mapper.Map<TelefonoClienteDTO>(telefonoCliente);
            return telefonoClienteDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoClienteCreacionDTO telefonoClienteCreacionDTO)
        {
            var telefonoCliente = this.mapper.Map<TelefonoCliente>(telefonoClienteCreacionDTO);
            this.inventarioDBContext.Add(telefonoCliente);
            await this.inventarioDBContext.SaveChangesAsync();
            var telefonoClienteDTO = this.mapper.Map<TelefonoClienteDTO>(telefonoCliente);
            return new CreatedAtRouteResult("GetTelefonoCliente", new { id = telefonoCliente.CodigoTelefono },
                telefonoClienteDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoClienteCreacionDTO telefonoClienteCreacionDTO)
        {
            var telefonoCliente = this.mapper.Map<TelefonoCliente>(telefonoClienteCreacionDTO);
            telefonoCliente.CodigoTelefono = id;
            this.inventarioDBContext.Entry(telefonoCliente).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoClienteDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.TelefonoClientes.Select(x => x.CodigoTelefono)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new TelefonoCliente { CodigoTelefono = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
