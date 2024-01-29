using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Models;
using DeskBooker.Core.Processors;
using Moq;
using Xunit;

namespace DeskBooker.Core.Tests
{
    public class DeskBookingRequestProcessorTests
    {
        public DeskBookingRequestProcessor _deskBookingService { get; }
        public DeskBookingDTO bookingDetails { get; }

        private readonly List<Desk> _deskDetails;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepository;
        private readonly Mock<IDeskAvailableRepository> _deskAvailableRepository;

        public DeskBookingRequestProcessorTests()
        {
             bookingDetails = new DeskBookingDTO {
                FirstName = "Raja",
                LastName = "K",
                EmailId = "raja.k@yahoo.com",
                BookingDate = new DateTime(2024,01,30)
            };

            _deskDetails = new List<Desk>() { new Desk { Id = 7} };
            _deskBookingRepository = new Mock<IDeskBookingRepository>();
            _deskAvailableRepository = new Mock<IDeskAvailableRepository>();
             _deskAvailableRepository.Setup(x => x.GetAvailableDeskDetails(It.IsAny<DateTime>())).Returns(_deskDetails);
            _deskBookingService = new DeskBookingRequestProcessor(_deskBookingRepository.Object,_deskAvailableRepository.Object);
        }

        [Fact]
        public void BookingDesk_ValidBookingDetails_ReturnBookedDetails()
        {       
            // Act
            var result = _deskBookingService.BookDesk(bookingDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookingDetails.FirstName, result.FirstName);
            Assert.Equal(bookingDetails.LastName, result.LastName);
            Assert.Equal(bookingDetails.EmailId, result.EmailId);
            Assert.Equal(bookingDetails.BookingDate.Date, result.BookingDate.Date);

        }

        [Fact]
        public void BookDesk_ParameterAsNull_ReturnsException()
        {
            //Assert
          var exceptionResult =  Assert.Throws<ArgumentNullException>(() => _deskBookingService.BookDesk(null));
          
         Assert.Equal("Input parameter cannot be null",exceptionResult.ParamName);
        }
        
        [Fact]
        public void BookDesk_PassBookDetails_ReturnsSavedData()
        {
            DeskBookingEntity deskBookingEntity = null;
            
            _deskBookingRepository.Setup(x => x.SaveBookDesk(It.IsAny<DeskBookingEntity>()))
            .Callback((DeskBookingEntity callBackResult)=> deskBookingEntity = callBackResult );

            _deskBookingService.BookDesk(bookingDetails);
            
            _deskBookingRepository.Verify(x => x.SaveBookDesk(It.IsAny<DeskBookingEntity>()),Times.Once);

            Assert.NotNull(deskBookingEntity);
            Assert.Equal(bookingDetails.FirstName, deskBookingEntity!.FirstName);
            Assert.Equal(bookingDetails.LastName, deskBookingEntity.LastName);
            Assert.Equal(bookingDetails.EmailId, deskBookingEntity.EmailId);
            Assert.Equal(bookingDetails.BookingDate.Date, deskBookingEntity.BookingDate.Date);
            Assert.Equal(_deskDetails.First().Id,deskBookingEntity.Id);
        }

       [Fact]
        public void BookDesk_PassDeskBookingRequest_ShouldNotCallSaveDesk()
        {      
            _deskDetails.Clear();
              _deskBookingService.BookDesk(bookingDetails);

              _deskBookingRepository.Verify(v => v.SaveBookDesk(It.IsAny<DeskBookingEntity>()),Times.Never);
        }
    }
}