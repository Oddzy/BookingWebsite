namespace Models.DataModels
{
    public class BookingChannelMessage : Booking
    {
        public bool Operation { get; set; }

        public BookingChannelMessage(string name, string email, int bookingId, bool operation) : base(name, email, bookingId)
        {
            Operation = operation;
        }

        public BookingChannelMessage(int bookingId, bool operation):base(bookingId)
        {
            Operation = operation;
        }

        public BookingChannelMessage()
        {
            
        }

    }
}
