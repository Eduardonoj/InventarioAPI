using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TelefonoClienteDTO
    {
        public int CodigoTelefonno { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public int Nit { get; set; }
        public ClienteDTO Cliente { get; set; }
    }
}
