using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public string? Icon { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
