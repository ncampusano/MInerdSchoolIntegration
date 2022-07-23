using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MInerdSchoolIntegration.Models
{
    public class JSONObject
    {
        public List<Encabezado> Encabezado { get; set; } = new List<Encabezado>();
        public List<Detalle> Detalle { get; set; } = new List<Detalle>();
    }
}
