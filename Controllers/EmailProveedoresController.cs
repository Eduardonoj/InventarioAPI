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
    public class EmailProveedoresController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        public EmailProveedoresController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailProveedorDTO>>> Get()
        {
            var emailProveedores = await this.inventarioDBContext.EmailProveedores.ToListAsync();
            var emailProveedoresDTO = this.mapper.Map<List<EmailProveedorDTO>>(emailProveedores);
            return emailProveedoresDTO;
        }
        [HttpGet("{id}", Name = "GetEmailProveedor")]
        public async Task<ActionResult<EmailProveedorDTO>> Get(int id)
        {
            var emailProveedor = await this.inventarioDBContext.EmailProveedores.FirstOrDefaultAsync(x => x.CodigoEmail.Equals(id));
            if (emailProveedor == null)
            {
                return NotFound();
            }
            var emailProveedorDTO = this.mapper.Map<EmailProveedorDTO>(emailProveedor);
            return emailProveedorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmailProveedorCreacionDTO emailProveedorCreacionDTO)
        {
            var emailProveedor = this.mapper.Map<EmailProveedor>(emailProveedorCreacionDTO);
            this.inventarioDBContext.Add(emailProveedor);
            await this.inventarioDBContext.SaveChangesAsync();
            var emailProveedorDTO = this.mapper.Map<EmailProveedorDTO>(emailProveedor);
            return new CreatedAtRouteResult("GetEmailProveedor", new { id = emailProveedor.CodigoEmail },
                emailProveedorDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmailProveedorCreacionDTO emailProveedorCreacionDTO)
        {
            var emailProveedor = this.mapper.Map<EmailProveedor>(emailProveedorCreacionDTO);
            emailProveedor.CodigoEmail = id;
            this.inventarioDBContext.Entry(emailProveedor).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailProveedorDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.EmailProveedores.Select(x => x.CodigoEmail)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new EmailProveedor { CodigoEmail = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
