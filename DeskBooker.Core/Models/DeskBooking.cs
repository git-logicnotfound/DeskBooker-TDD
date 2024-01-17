using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskBooker.Core.Models
{
    public class DeskBooking
    {
        public String FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime BookingDate { get; set; }

    }
}