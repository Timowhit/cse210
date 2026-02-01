public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        decimal subtotal = 0;
        
        foreach (Product product in _products)
        {
            subtotal += product.GetTotalCost();
        }

        // Add shipping cost: $5 for USA, $35 for international
        decimal shippingCost = _customer.LivesInUSA() ? 5 : 35;

        return subtotal + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "";
        
        foreach (Product product in _products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }

        return label;
    }

    public string GetShippingLabel()
    {
        return $"{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}
