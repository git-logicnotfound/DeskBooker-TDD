using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Models;

namespace DeskBooker.Core.Processors
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository _deskBookingRepository;
        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository)
        {
            _deskBookingRepository = deskBookingRepository;
        }

        public DeskBookingDTO BookDesk(DeskBookingDTO deskBooking)
        {
            if (deskBooking is null)
            {
                throw new ArgumentNullException("Input parameter cannot be null");
            }

          _deskBookingRepository.SaveBookDesk(MapObject<DeskBookingEntity>(deskBooking));

            return deskBooking;
        }

        private static T MapObject<T>(DeskBookingDTO data) where T : DeskBooking, new()
        {
            return new T {
                FirstName = "Raja",
                LastName = "K",
                EmailId = "raja.k@yahoo.com",
                BookingDate = DateTime.Now
            };
        }
    }
}