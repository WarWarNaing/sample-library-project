using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
   public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime AccessTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
