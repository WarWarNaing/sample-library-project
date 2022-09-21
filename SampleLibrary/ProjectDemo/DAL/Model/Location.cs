using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
   public  class Location
    {
        public int Id { get; set; }
       
        public string Country { get; set; }

        public string StateDivision { get; set; }

        public string Township { get; set; }

        public DateTime AccessTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
