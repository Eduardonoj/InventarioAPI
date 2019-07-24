using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioAPI.Entities;
using System;
using System.Linq;


namespace InventarioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;

        public ClientesController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()//aqui en donde dice Get puede ir cualquier nombre que quiera
        { 
            var clientes = await inventarioDBContext.Clientes.ToListAsync();
            var clientesDTO = mapper.Map<List<ClienteDTO>>(clientes);
            return clientesDTO;
        }

        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await this.inventarioDBContext.Clientes.FirstOrDefaultAsync(x => x.Nit.Equals(id));
            if (cliente == null)
            {
                return NotFound();
            }
            var clienteDTO = this.mapper.Map<ClienteDTO>(cliente);
            return clienteDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteCreacionDTO clienteCreacion)
        {
            var cliente = mapper.Map<Cliente>(clienteCreacion);
            inventarioDBContext.Add(cliente);
            await inventarioDBContext.SaveChangesAsync();
            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            return new CreatedAtRouteResult("GetCliente", new { id = cliente.Nit },
                clienteDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ClienteCreacionDTO clienteCreacionDTO)
        {
            var cliente = this.mapper.Map<Cliente>(clienteCreacionDTO);
            cliente.Nit = id;
            this.inventarioDBContext.Entry(cliente).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> Delete(string id)
        {
            var nitCliente = await inventarioDBContext.Clientes.Select(x => x.Nit)
                .FirstOrDefaultAsync(x => x == id);
            if (nitCliente == default(string))
            {
                return NotFound();
            }
            inventarioDBContext.Remove(new Cliente { Nit = id });
            await inventarioDBContext.SaveChangesAsync();
            return NoContent();

        }

    }
}
