using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LinkCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
        public int? IssuedById { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int? UsedById { get; set; }
        public DateTime? UsedAt { get;set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}
