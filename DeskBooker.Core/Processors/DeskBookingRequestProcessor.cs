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
        private readonly IDeskAvailableRepository _deskAvailableRepository;
        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository, IDeskAvailableRepository deskAvailableRepository)
        {
            _deskAvailableRepository = deskAvailableRepository;
            _deskBookingRepository = deskBookingRepository;
        }

        public DeskBookingDTO BookDesk(DeskBookingDTO deskBooking)
        {
            if (deskBooking is null)
            {
                throw new ArgumentNullException("Input parameter cannot be null");
            }

            var deskDetails = _deskAvailableRepository.GetAvailableDeskDetails(deskBooking.BookingDate);

          if(deskDetails.FirstOrDefault() is Desk deskData)
          {
             var deskBookingEntity = MapObject<DeskBookingEntity>(deskBooking);
             deskBookingEntity.Id = deskData.Id;
             _deskBookingRepository.SaveBookDesk(deskBookingEntity);
          }
  
            return deskBooking;
        }

        private static T MapObject<T>(DeskBookingDTO data) where T : DeskBooking, new()
        {
            return new T {
                FirstName = data.FirstName,
                LastName = data.LastName,
                EmailId = data.EmailId,
                BookingDate = data.BookingDate
            };
        }
    }
}