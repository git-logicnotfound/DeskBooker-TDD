using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskBooker.Core.Models;

namespace DeskBooker.Core.Processors
{
    public class DeskBookingRequestProcessor
    {
        
        public DeskBooking BookDesk(DeskBooking deskBooking)
        {
            if(deskBooking is null)
            {
                throw new ArgumentNullException("Input parameter cannot be null");
            }
            return deskBooking;
        }
    }
}