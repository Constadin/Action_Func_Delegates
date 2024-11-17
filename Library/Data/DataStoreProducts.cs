using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Data
{
    /// <summary>
    /// Represents a data store for products, responsible for populating and managing a list of products.
    /// </summary>
    public class DataStoreProducts
    {
        // List of products in the store
        public List<ProductModel> Items { get; set; } = new List<ProductModel>();

        // Service to generate random values (e.g., price and GUID)
        private readonly IRandomizeService _RandomizeService;

        /// <summary>
        /// Initializes the DataStoreProducts with required dependencies and populates the product list.
        /// </summary>
        /// <param name="serviceProvider">Service provider for dependency injection.</param>
        public DataStoreProducts(IServiceProvider serviceProvider)
        {
            // Resolve IRandomizeService from the service provider
            this._RandomizeService = serviceProvider.GetRequiredService<IRandomizeService>();

            // Populate the product list with random values
            GetPopulateProductsList();
        }

        /// <summary>
        /// Populates the list of products with sample data including name, unique GUID, and random price.
        /// </summary>
        private void GetPopulateProductsList()
        {
            // Array of product names to be used for each product
            var productName = new string[]
            {
                "Handmade Wooden Chair",
                "Handmade Leather Wallet",
                "Handmade Ceramic Mug",
                "Handmade Woolen Scarf",
                "Handmade Wooden Bowl"
            };

            // Loop to create and add 5 different products to the list
            for (int i = 0; i < 5; i++)
            {
                // Create a new product instance
                var product = new ProductModel();

                // Set a unique GUID for the product
                product.SetGuidId(this._RandomizeService.RandomGuid());

                // Set a unique name for the product from the predefined list
                product.SetItemName(productName[i]);

                // Set a unique random price for the product
                product.SetItemPrice(this._RandomizeService.RandomDecimal());

                // Add the newly created product to the list of items
                Items.Add(product);
            }
        }
    }
}
