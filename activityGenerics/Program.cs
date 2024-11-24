using activityGenerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activityGenerics
{
    public class DictionaryRepository<TKey, TValue> where TKey : IComparable
    {
        private Dictionary<TKey, TValue> _items = new Dictionary<TKey, TValue>();

        public void Add(TKey id, TValue item)
        {
            if (_items.ContainsKey(id))
            {
                throw new ArgumentException("An item with the same key already exists.");
            }
            _items[id] = item;
        }

        public TValue Get(TKey id)
        {
            if (!_items.TryGetValue(id, out TValue item))
            {
                throw new KeyNotFoundException("Item not found.");
            }
            return item;
        }

        public void Update(TKey id, TValue newItem)
        {
            if (!_items.ContainsKey(id))
            {
                throw new KeyNotFoundException("Item not found.");
            }
            _items[id] = newItem;
        }

        public void Delete(TKey id)
        {
            if (!_items.Remove(id))
            {
                throw new KeyNotFoundException("Item not found.");
            }
        }

        public List<KeyValuePair<TKey, TValue>> GetAll()
        {
            return new List<KeyValuePair<TKey, TValue>>(_items);
        }
    }

    public class Repository<T>
    {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public T Get(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }
            return _items[index];
        }

        public void Update(int index, T newItem)
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }
            _items[index] = newItem;
        }

        public void Delete(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }
            _items.RemoveAt(index);
        }

        public List<T> GetAll()
        {
            return _items;
        }
    }
}
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public override string ToString()
        {
            return $"Product ID: {ProductId}, Product Name: {ProductName}";
        }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public override string ToString()
        {
            return $"Student ID: {StudentId}, Student Name: {StudentName}";
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            DictionaryRepository<int, Product> productRepo = new DictionaryRepository<int, Product>();
            Repository<Student> studentRepo = new Repository<Student>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Get Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("6. Add Student");
                Console.WriteLine("7. Get Student");
                Console.WriteLine("8. Update Student");
                Console.WriteLine("9. Delete Student");
                Console.WriteLine("10. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        
                        Console.Write("Enter Product ID: ");
                        int addId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Product Name: ");
                        string addName = Console.ReadLine();
                        var newProduct = new Product { ProductId = addId, ProductName = addName };
                        try
                        {
                            productRepo.Add(addId, newProduct);
                            Console.WriteLine("Product added successfully.\n");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "2":
                        
                        Console.Write("Enter Product ID to retrieve: ");
                        int getId = int.Parse(Console.ReadLine());
                        try
                        {
                            var product = productRepo.Get(getId);
                            Console.WriteLine("Retrieved: " + product + "\n");
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "3":
                        
                        Console.Write("Enter Product ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("Enter new Product Name: ");
                        string updateName = Console.ReadLine();
                        var updatedProduct = new Product { ProductId = updateId, ProductName = updateName };
                        try
                        {
                            productRepo.Update(updateId, updatedProduct);
                            Console.WriteLine("Product updated successfully. \n");
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "4":
                        
                        Console.Write("Enter Product ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        try
                        {
                            productRepo.Delete(deleteId);
                            Console.WriteLine("Product deleted successfully. \n");
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "5":
                        
                        running = false;
                        Console.WriteLine("Exiting the program.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    
