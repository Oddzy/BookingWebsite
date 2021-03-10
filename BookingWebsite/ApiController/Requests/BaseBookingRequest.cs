using System.ComponentModel.DataAnnotations;

namespace BookingWebsite.ApiController.Requests
{
    public abstract class BaseBookingRequest
    {
        public abstract bool Operation { get; set; }
        [Required]
        public int BookingId { get; set; }

  
    }
}