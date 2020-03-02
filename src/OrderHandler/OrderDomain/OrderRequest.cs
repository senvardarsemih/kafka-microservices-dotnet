namespace OrderDomain
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
    }
    public enum OrderStatus
    {
        InProgress,
        Completed,
        Rejected
    }
}
