using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create Order 1 - USA Customer
        Address address1 = new Address("123 Oak Street", "Los Angeles", "CA", "USA");
        Customer customer1 = new Customer("John Smith", address1);
        Order order1 = new Order(customer1);

        order1.AddProduct(new Product("Wireless Mouse", "TECH-001", 29.99m, 2));
        order1.AddProduct(new Product("USB-C Cable", "TECH-002", 12.50m, 3));
        order1.AddProduct(new Product("Laptop Stand", "TECH-003", 45.00m, 1));

        // Create Order 2 - International Customer (Canada)
        Address address2 = new Address("456 Maple Avenue", "Toronto", "Ontario", "Canada");
        Customer customer2 = new Customer("Emily Johnson", address2);
        Order order2 = new Order(customer2);

        order2.AddProduct(new Product("Bluetooth Headphones", "AUDIO-101", 79.99m, 1));
        order2.AddProduct(new Product("Phone Case", "ACC-201", 19.99m, 2));

        // Store orders in a list
        List<Order> orders = new List<Order> { order1, order2 };

        // Display information for each order
        int orderNumber = 1;
        foreach (Order order in orders)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine($"ORDER #{orderNumber}");
            Console.WriteLine("===========================================");
            
            Console.WriteLine("\n--- PACKING LABEL ---");
            Console.WriteLine(order.GetPackingLabel());
            
            Console.WriteLine("--- SHIPPING LABEL ---");
            Console.WriteLine(order.GetShippingLabel());
            
            Console.WriteLine($"\n--- ORDER TOTAL ---");
            Console.WriteLine($"Total Price: ${order.GetTotalPrice():0.00}");
            Console.WriteLine();

            orderNumber++;
        }
    }
}