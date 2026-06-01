using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class ExtractGroupedByReleaseDateResponseDTO
    {
        public DateTime ReleaseDate { get; set; }
        public List<ExtractResponseDTO> Releases { get; set; }
    }
}
