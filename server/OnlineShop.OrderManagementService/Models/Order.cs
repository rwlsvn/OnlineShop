namespace OnlineShop.OrderManagementService.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddresss { get; set; }
        public DateTime? CreationDate { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> Items { get; set; }   
    }

    public enum OrderStatus
    {
        New,
        Processing,
        Shipped,
        Delivered,
        Canceled,
        Returned
    }
}
