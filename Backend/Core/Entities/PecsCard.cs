using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PecsCard
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public string? Category { get; set; }
        public int? OrderNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
