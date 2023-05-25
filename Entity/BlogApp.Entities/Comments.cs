using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogId  { get; set; }
        public int UserId { get; set;}
        public string comment { get; set; }
    }
}
