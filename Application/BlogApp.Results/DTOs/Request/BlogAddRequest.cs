using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Results.DTOs.Request
{
    public class BlogAddRequest
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public int? AdminId { get; set; }
        public int? CategoryId { get; set; }
    }
}
