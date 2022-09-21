using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
   public class RentedBook
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal? RentPrice { get; set; }
        public decimal? PenaltyFee { get; set; }
        public bool IsDeStrict { get; set; }
        public bool IsRePay { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AccessTime { get; set; }
    }
}
