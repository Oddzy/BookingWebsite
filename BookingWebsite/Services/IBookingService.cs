using BookingWebsite.ApiController.Requests;
using Models.DataModels;

namespace BookingWebsite.Services
{
    public interface IBookingService
    {
        public void CreateBooking(string name, string email, int bookingId, bool operation);
        public void DeleteBooking(int bookingId,bool operation);

    }
}