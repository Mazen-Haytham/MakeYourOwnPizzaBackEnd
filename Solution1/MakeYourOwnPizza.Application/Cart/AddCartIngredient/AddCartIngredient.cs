public class AddCartIngredientRequest
{
    public Guid CartItemId { get; set; }
    public Guid IngredientId { get; set; }

    public int Quantity { get; set; }
}