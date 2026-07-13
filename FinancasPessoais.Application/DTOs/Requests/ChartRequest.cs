using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class ChartRequest
    {
        public List<Guid> Categories { get; set; }
    }
}
