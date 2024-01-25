using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskBooker.Core.Models;

namespace DeskBooker.Core.DataInterface
{
    public interface IDeskBookingRepository
    {
        public void SaveBookDesk(DeskBookingEntity deskBookingRequest);
    }
}