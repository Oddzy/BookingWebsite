namespace Models.DataModels
{
    public class Booking
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public int BookingId { get; set; }

        public Booking()
        {
            
        }

        public Booking(int bookingId)
        {
            BookingId = bookingId;
        }

        public Booking(string name, string email, int bookingId)
        {
            Name = name;
            Email = email;
            BookingId = bookingId;
        }
    }
}
