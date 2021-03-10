using System;
using BookingWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using BookingWebsite.ApiController.Requests;

namespace BookingWebsite.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingService _bookingService;

        public BookingController()
        {
            _bookingService = new BookingService();
        }

        [HttpPost("createBooking")]
        public ActionResult CreateBooking([FromBody] CreateBookingRequest request)
        {
            Console.WriteLine($"Creating booking");
            if (request == null)
            {
                Console.WriteLine("Create booking request was null or invalid");
                return null;
            }

            _bookingService.CreateBooking(request.Name,request.Email,request.BookingId,request.Operation);

            var result = "Booking request sent";
            return Ok(result);
        }

        [HttpPost("deleteBooking")]
        public ActionResult DeleteBooking([FromBody] DeleteBookingRequest request)
        {
            Console.WriteLine($"Deleting booking");
            if (request == null)
            {
                Console.WriteLine("Delete booking request was null or invalid");
                return null;
            }

            _bookingService.DeleteBooking(request.BookingId,request.Operation);

            var result = "Request for delete was sent";
            return Ok(result);
        }
    }
}
