using System;
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
        private readonly Mock<IDeskBookingRepository> _deskBookingRepository;

        public DeskBookingRequestProcessorTests()
        {
            _deskBookingRepository = new Mock<IDeskBookingRepository>();
            _deskBookingService = new DeskBookingRequestProcessor(_deskBookingRepository.Object);
        }

        [Fact]
        public void BookingDesk_ValidBookingDetails_ReturnBookedDetails()
        {
            // Arrange
            var bookingDetails = new DeskBookingDTO
            {
                FirstName = "Raja",
                LastName = "K",
                EmailId = "raja.k@yahoo.com",
                BookingDate = DateTime.Now
            };

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
            var BookDetails = new DeskBookingDTO {
                FirstName = "Raja",
                LastName = "K",
                EmailId = "raja.k@yahoo.com",
                BookingDate = DateTime.Now
            };

            _deskBookingRepository.Setup(x => x.SaveBookDesk(It.IsAny<DeskBookingEntity>()))
            .Callback((DeskBookingEntity callBackResult)=> deskBookingEntity = callBackResult );

            _deskBookingService.BookDesk(BookDetails);
            
            _deskBookingRepository.Verify(x => x.SaveBookDesk(It.IsAny<DeskBookingEntity>()),Times.Once);

            Assert.NotNull(deskBookingEntity);
            Assert.Equal(BookDetails.FirstName, deskBookingEntity!.FirstName);
            Assert.Equal(BookDetails.LastName, deskBookingEntity.LastName);
            Assert.Equal(BookDetails.EmailId, deskBookingEntity.EmailId);
            Assert.Equal(BookDetails.BookingDate.Date, deskBookingEntity.BookingDate.Date);
            
        }
    }
}