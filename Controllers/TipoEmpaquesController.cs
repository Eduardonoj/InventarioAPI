using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TipoEmpaquesController : ControllerBase
    {
        private readonly InventarioDBContext inventarioDBContext;
        private readonly IMapper mapper;
        
        public TipoEmpaquesController(InventarioDBContext inventarioDBContext, IMapper mapper)
        {
            this.inventarioDBContext = inventarioDBContext;
            this.mapper = mapper;
        }

        [HttpGet]
            public async Task<ActionResult<IEnumerable<TipoEmpaqueDTO>>>Get()
        {
            var tipoEmpaques = await inventarioDBContext.TipoEmpaques.ToListAsync();
            var tipoEmpaquesDTO = mapper.Map<List<TipoEmpaqueDTO>>(tipoEmpaques);
            return tipoEmpaquesDTO;
        }
        [HttpGet("{id}", Name ="GetTipoEmpaque")]
        public async Task<ActionResult<TipoEmpaqueDTO>> Get(int id)
        {
            var tipoEmpaque = await inventarioDBContext.TipoEmpaques.FirstOrDefaultAsync(x => x.CodigoEmpaque == id);
            if(tipoEmpaque == null)
            {
                return NotFound();
            }
            var tipoEmpaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoEmpaque);
            return tipoEmpaqueDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoEmpaqueCreacionDTO empaqueCreacion)
        {
            var tipoEmpaque = mapper.Map<TipoEmpaque>(empaqueCreacion);
            inventarioDBContext.Add(tipoEmpaque);
            await inventarioDBContext.SaveChangesAsync();
            var tipoEmpaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoEmpaque);
            return new CreatedAtRouteResult("GetTipoEmpaque", new { id = tipoEmpaque.CodigoEmpaque }, tipoEmpaqueDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoEmpaqueCreacionDTO tipoEmpaqueCreacionDTO)
        {
            var tipoEmpaque = this.mapper.Map<TipoEmpaque>(tipoEmpaqueCreacionDTO);
            tipoEmpaque.CodigoEmpaque = id;
            this.inventarioDBContext.Entry(tipoEmpaque).State = EntityState.Modified;
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoEmpaqueDTO>> Delete(int id)
        {
            var codigo = await this.inventarioDBContext.TipoEmpaques.Select(x => x.CodigoEmpaque)
                .FirstOrDefaultAsync(x => x == id);
            if (codigo == default(int))
            {
                return NotFound();
            }
            this.inventarioDBContext.Remove(new TipoEmpaque { CodigoEmpaque = id });
            await this.inventarioDBContext.SaveChangesAsync();
            return NoContent();
        }

    }

}
