using Library.Data;
using Library.LogicOfModels;
using Library.Models;
using Library.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace ConsoleAppUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Set the console output encoding to UTF-8 to support special characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Set up the Dependency Injection (DI) container
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRandomizeService, RandomizeService>()  // Register RandomizeService as a singleton
                .AddTransient<DataStoreProducts>()                   // Register DataStoreProducts with transient lifetime
                .AddTransient<ShoppingCartLogic>()                  // Register ShoppingCartLogic with transient lifetime
                .BuildServiceProvider();                           // Build the service provider

            // Resolve the DataStoreProducts and ShoppingCartLogic instances from the DI container
            var dataStoreProducts = serviceProvider.GetRequiredService<DataStoreProducts>();
            var shoppingCartLogic = serviceProvider.GetRequiredService<ShoppingCartLogic>();

            // Pass shoppingCartLogic to the method to display items
            DisplayAllItemsCard(dataStoreProducts);

            // Change the text color to yellow
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Generate and display the total for the cart items
            Console.WriteLine($"The total (per price ) for the Cart 1 is {shoppingCartLogic.GenerateTotal(SubTotalInfo, CalculateLevelDiscount, AlertMsg).ToString("C2", CultureInfo.CreateSpecificCulture("en-GR"))}");

            // Reset the console text color back to default
            Console.ResetColor();

            decimal total = shoppingCartLogic.GenerateTotal((subTotal) => Console.WriteLine($"The subtotal for card 2 is {subTotal:C2}"),
                (productList, subTotalc) => {
                    if (productList.Count >= 5)
                    {
                        return subTotalc * 0.7M;
                    }
                    else
                    {
                        return subTotalc;
                    }
                },
                (message) => Console.WriteLine($"Cart 2 Alert: {message} for 5 items"));

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"The total (by pieces) for card 2 is {total}");

            Console.ResetColor();
                        
            Console.ReadKey();
        }


        private static void SubTotalInfo(decimal subTotal)
        { 
            Console.WriteLine($"This subTotal is {subTotal:C2}");
        }

        private static decimal CalculateLevelDiscount(List<ProductModel> items, decimal subTotal)
        {
            // Apply discounts based on the subtotal value
            switch (subTotal)
            {
                // Apply 20% discount if subtotal is between 100 and 200
                case var _ when (subTotal > 100 && subTotal <= 200):
                    return subTotal * 0.20M;

                // Apply 45% discount if subtotal is between 201 and 500
                case var _ when (subTotal > 201 && subTotal <= 500):
                    return subTotal * 0.45M;

                // Apply 75% discount if subtotal is above 501
                case var _ when subTotal > 501:
                    return subTotal * 0.75M;

                // No discount if subtotal is 100 or less
                default:
                    return subTotal;              // No discount
            }

        }

        private static void AlertMsg(string message)
        {
            Task.Delay(2000);
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays all items in the cart along with their prices.
        /// </summary>
        /// <param name="dataStoreProducts">The data store containing the products in the cart.</param>
        private static void DisplayAllItemsCard(DataStoreProducts dataStoreProducts)
        {
            // Loop through the list of items and display their names and prices
            foreach (var item in dataStoreProducts.Items)
            {
                Console.WriteLine($"Item in Cart|| {item.ItemName}\t|| Price: {item.ItemPrice.ToString("C2", CultureInfo.CreateSpecificCulture("en-GR"))}");
            }
        }

    }
}

// Note: You need to install the Microsoft Dependency Injection package in your project. 
// You can do this via NuGet Package Manager or by running the following command in the Package Manager Console:
// Install-Package Microsoft.Extensions.DependencyInjection
