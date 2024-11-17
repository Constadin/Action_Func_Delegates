using Library.Data;
using Library.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.LogicOfModels
{
    /// <summary>
    /// Handles the logic of the shopping cart, including calculating totals and applying discounts.
    /// </summary>
    public class ShoppingCartLogic
    {
        // Service provider for dependency injection
        public readonly IServiceProvider _ServiceProvider;

        public delegate void DiscountOnItem(decimal SubTotal);

        // Data store for products, injected through the constructor
        private DataStoreProducts _DataStoreProducts;

        /// <summary>
        /// Initializes the ShoppingCartLogic with required dependencies.
        /// </summary>
        /// <param name="serviceProvider">Service provider to resolve dependencies.</param>
        public ShoppingCartLogic(IServiceProvider serviceProvider)
        {
            // Resolving the DataStoreProducts service through the service provider
            this._DataStoreProducts = serviceProvider.GetRequiredService<DataStoreProducts>();
        }

        /// <summary>
        /// Calculates the total price of items in the shopping cart, applying discounts based on subtotal.
        /// </summary>
        /// <returns>The total price after applying applicable discounts.</returns>
        public decimal GenerateTotal(DiscountOnItem discountOn, Func<List<ProductModel>, decimal, decimal> discountCalculator, Action<string> discountingMsg)
        {    
            // Calculate the subtotal by summing up the item prices
            decimal subTotal = this._DataStoreProducts.Items.Sum((x) => x.ItemPrice);

            discountOn(subTotal);
                        
            discountingMsg("Cart 1 Alert: We are applying your discount");

            return discountCalculator(this._DataStoreProducts.Items, subTotal);
        }
    }
}
