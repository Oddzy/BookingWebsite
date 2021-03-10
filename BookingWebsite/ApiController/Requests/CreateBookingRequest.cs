using System.ComponentModel.DataAnnotations;

namespace BookingWebsite.ApiController.Requests
{
    public class CreateBookingRequest : BaseBookingRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public override bool Operation { get; set; } = true;
    }
}
