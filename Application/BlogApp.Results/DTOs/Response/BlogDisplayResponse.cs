using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Results.DTOs.Response
{
    public class BlogDisplayResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
    }
}
