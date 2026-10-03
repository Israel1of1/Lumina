using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ContentKeywordDto
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int KeywordId { get; set; }
        public string KeywordName { get; set; } = string.Empty;
    }
}
