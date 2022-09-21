using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
   public  class Book
    {
        public int BookId { get; set; } 
        public string BookName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Quantity { get; set; }
        public decimal? BookPrice { get; set; }
        public decimal? RentPrice { get; set; }
        public string Photo { get; set; }
        public DateTime ReachDate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
    }
}
