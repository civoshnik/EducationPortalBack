public class CartItemDto
{
    public Guid CartItemId { get; set; }
    public Guid UserId { get; set; }
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public string ServiceName { get; set; }
    public decimal ServicePrice { get; set; }
}
