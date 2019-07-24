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
    public class EmailClientesController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public EmailClientesController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailClienteDTO>>> Get()
        {
            var emailClientes = await this.inventarioDBContext.EmailClientes.ToListAsync();
            var emailClientesDTO = this.mapper.Map<List<EmailClienteDTO>>(emailClientes);
            return emailClientesDTO;
        }
        [HttpGet("{id}", Name = "GetEmailCliente")]
        public async Task<ActionResult<EmailClienteDTO>> Get(int id)
        {
            var emailCliente = await this.inventarioDBContext.EmailClientes.FirstOrDefaultAsync(x => x.CodigoEmail.Equals(id));
            if (emailCliente == null)
            {
                return NotFound();
            }
            var emailClienteDTO = this.mapper.Map<EmailClienteDTO>(emailCliente);
            return emailClienteDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmailClienteCreacionDTO emailClienteCreacionDTO)
        {
            var emailCliente = this.mapper.Map<EmailCliente>(emailClienteCreacionDTO);
            this.inventarioDBContext.Add(emailCliente);
            await this.inventarioDBContext.SaveChangesAsync();
            var emailClienteDTO = this.mapper.Map<EmailClienteDTO>(emailCliente);
            return new CreatedAtRouteResult("GetEmailCliente", new { id = emailCliente.CodigoEmail },
                emailClienteDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmailClienteCreacionDTO emailClienteCreacionDTO)
        {
            var emailCliente = this.mapper.Map<EmailCliente>(emailClienteCreacionDTO);
            emailCliente.CodigoEmail = id;
            this.inventarioDBContext.Entry(emailCliente).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailClienteDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.EmailClientes.Select(x => x.CodigoEmail)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new EmailCliente { CodigoEmail = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    
}
}
