using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
  public   class Customer
    {
        public int CustomerId { get; set; } 

        public string CustomerName { get; set; }

        public string Phone { get; set; }   

        public string State { get; set; }

        public string Township { get; set; }

        public string Address { get; set; }

        public DateTime JoinedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
