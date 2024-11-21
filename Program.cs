using System;
using System.Collections.Generic;

public class DictionaryRepository<TKey, TValue> where TKey : IComparable<TKey>
{
    private Dictionary<TKey, TValue> items;

    public DictionaryRepository()
    {
        items = new Dictionary<TKey, TValue>();
    }

    
    public void Add(TKey id, TValue item)
    {
        if (items.ContainsKey(id))
        {
            throw new ArgumentException("An item with this key already exists.");
        }

        items.Add(id, item);
    }

    
    public TValue Get(TKey id)
    {
        if (items.ContainsKey(id))
        {
            return items[id];
        }

        throw new KeyNotFoundException("Item with this key not found.");
    }

    
    public void Update(TKey id, TValue newItem)
    {
        if (!items.ContainsKey(id))
        {
            throw new KeyNotFoundException("Item with this key not found.");
        }

        items[id] = newItem;
    }

    
    public void Delete(TKey id)
    {
        if (!items.ContainsKey(id))
        {
            throw new KeyNotFoundException("Item with this key not found.");
        }

        items.Remove(id);
    }

    
    public void DisplayAllItems()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("No products available.\n");
            return;
        }

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public Product(int id, string name)
    {
        ProductId = id;
        ProductName = name;
    }

    public override string ToString()
    {
        return $"Product ID: {ProductId}, Product Name: {ProductName}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var productRepo = new DictionaryRepository<int, Product>();

        while (true)
        {
            Console.WriteLine("\n--- Product Repository ---");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Retrieve Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Display All Products");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                   
                    Console.Write("Enter Product ID: ");
                    int addId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Product Name: ");
                    string addName = Console.ReadLine();

                    try
                    {
                        productRepo.Add(addId, new Product(addId, addName));
                        Console.WriteLine("Product added successfully.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "2":
                    
                    Console.Write("Enter Product ID to retrieve: ");
                    int getId = int.Parse(Console.ReadLine());

                    try
                    {
                        var product = productRepo.Get(getId);
                        Console.WriteLine("Retrieved Product: " + product);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "3":
                    
                    Console.Write("Enter Product ID to update: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Enter New Product Name: ");
                    string updateName = Console.ReadLine();

                    try
                    {
                        productRepo.Update(updateId, new Product(updateId, updateName));
                        Console.WriteLine("Product updated successfully.");
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "4":
                    
                    Console.Write("Enter Product ID to delete: ");
                    int deleteId = int.Parse(Console.ReadLine());

                    try
                    {
                        productRepo.Delete(deleteId);
                        Console.WriteLine("Product deleted successfully.");
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "5":
                    
                    Console.WriteLine("\nDisplaying all products:");
                    productRepo.DisplayAllItems();
                    break;

                case "6":
                    
                    Console.WriteLine("Exiting the application.");
                    return;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}
