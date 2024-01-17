using System;
using DeskBooker.Core.Models;
using DeskBooker.Core.Processors;
using Xunit;

namespace DeskBooker.Core.Tests
{
    public class DeskBookingRequestProcessorTests
    {
        public DeskBookingRequestProcessor _deskBookingRequest { get; }
        public DeskBookingRequestProcessorTests()
        {
            _deskBookingRequest = new DeskBookingRequestProcessor();
        }

        [Fact]
        public void BookingDesk_ValidBookingDetails_ReturnBookedDetails()
        {
            // Arrange
            var bookingDetails = new DeskBooking
            {
                FirstName = "Raja",
                LastName = "K",
                EmailId = "raja.k@yahoo.com",
                BookingDate = DateTime.Now
            };

            // Act
            var result = _deskBookingRequest.BookDesk(bookingDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookingDetails.FirstName, result.FirstName);
            Assert.Equal(bookingDetails.LastName, result.LastName);
            Assert.Equal(bookingDetails.EmailId, result.EmailId);
            Assert.Equal(bookingDetails.BookingDate, result.BookingDate);

        }

        [Fact]
        public void BookDesk_ParameterAsNull_ReturnsException()
        {
            //Assert
          var exceptionResult =  Assert.Throws<ArgumentNullException>(() => _deskBookingRequest.BookDesk(null));
          
         Assert.Equal("Input parameter cannot be null",exceptionResult.ParamName);
        }
    }
}