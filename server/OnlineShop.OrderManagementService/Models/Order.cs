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
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
