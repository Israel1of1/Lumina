using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class LinkCodeInfoDto
    {
        public string Purpose { get; set; } = string.Empty;
        public DateTime? ExpiresAt { get; set; }
    }
}
