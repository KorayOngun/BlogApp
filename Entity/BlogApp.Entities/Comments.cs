using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Entities
{
    public class Comment
    {

        public int BlogId  { get; set; }
        public Blog? Blog { get; set; }
        public int UserId { get; set;}
        public User? User { get; set; }  
        public string comment { get; set; }
    }
}
