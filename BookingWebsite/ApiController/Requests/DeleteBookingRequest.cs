namespace BookingWebsite.ApiController.Requests
{
    public class DeleteBookingRequest : BaseBookingRequest
    {
        public override bool Operation { get; set; } = false;
    }
}