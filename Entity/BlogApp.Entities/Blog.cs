using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Entities
{
    public class Blog : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
