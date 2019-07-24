using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TelefonoProveedorDTO
    {
        public int CodigoTelefonno { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public int CodigoProveedor { get; set; }
        public ProveedorDTO Proveedor { get; set; }
    }
}
