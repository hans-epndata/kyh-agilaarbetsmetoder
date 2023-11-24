using ClientApp.ServiceReference;
using System;
using System.Collections.Generic;

namespace ClientApp.Services
{
    internal static class MenuService
    {
        public static void ShowMenuOptions()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("# MENU #");
                Console.WriteLine($"{"1",-5} View Products");
                Console.WriteLine($"{"2",-5} Add Product");
                Console.WriteLine($"{"0",-5} Exit Application");
                Console.Write("Choose one option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowViewOption();
                        break;
                    
                    case "2":
                        ShowAddOption();
                        break;

                    case "0":
                        ShowExitOption();
                        break;
                }
            }
        }

        private static void ShowViewOption() 
        {
            Console.Clear();
            var client = new ServiceReference.ProductServiceClient();
            var result = client.GetProductsFromList();

            if (result.Status == ServiceCode.OK)
            {
                foreach (var product in result.Products)
                {
                    Console.WriteLine($"{"Article Number", -5} {product.ArticleNumber}");
                    Console.WriteLine($"{"Product Title", -5} {product.Title}");
                    Console.WriteLine($"{"Price", -5} {product.Price}");
                    Console.WriteLine($"{"Description", -5} {product.Description}");
                    Console.WriteLine("");
                }
            }
            else
            {
                DisplayStatusMessage(result.Status);
            }

            Console.ReadKey();
        }


        private static void ShowAddOption() 
        {
            var product = new Product();

            Console.Clear();
            Console.Write("Enter Article Number: ");
            product.ArticleNumber = Console.ReadLine();

            Console.Write("Enter Manifacture: ");
            product.Manifacture = Console.ReadLine();

            Console.Write("Enter Product Title: ");
            product.Title = Console.ReadLine();

            Console.Write("Enter Product Description: ");
            product.Description = Console.ReadLine();

            Console.Write("Enter Price: ");
            product.Price = decimal.Parse(Console.ReadLine());

            var client = new ServiceReference.ProductServiceClient();
            var result = client.AddProductToList(product);

            DisplayStatusMessage(result.Status);
            Console.ReadKey();
        }


        private static void ShowExitOption() 
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit the application? (y/n): ");
            var option = Console.ReadLine();

            if (option.ToLower() == "y")
                Environment.Exit(0);
        }


        private static void DisplayStatusMessage(ServiceCode status)
        {
            switch (status)
            {
                case ServiceCode.DELETED:
                    Console.WriteLine("\nProduct was deleted successfully.");
                    break;

                case ServiceCode.UPDATED:
                    Console.WriteLine("\nProduct was updated successfully.");
                    break;

                case ServiceCode.ADDED:
                    Console.WriteLine("\nProduct was created successfully.");
                    break;

                case ServiceCode.EXISTS:
                    Console.WriteLine("\nProduct already exists.");
                    break;

                case ServiceCode.NOTFOUND:
                    Console.WriteLine("\nProduct was not found.");
                    break;
            }
        }
    }
}
