using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class EmailCliente
    {
        public int CodigoEmail { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Nit { get; set; }
        public Cliente Cliente { get; set; }

    }
}
