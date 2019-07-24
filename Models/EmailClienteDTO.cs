using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class EmailClienteDTO
    {
        public int CodigoEmail { get; set; }
        public string Email { get; set; }
        public int Nit { get; set; }
        public ClienteDTO Proveedor { get; set; }

    }
}
