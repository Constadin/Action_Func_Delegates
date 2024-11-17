using System;

/// <summary>
/// Provides methods to generate random values for different data types.
/// </summary>
namespace Library.Services
{
    /// <summary>
    /// Service that generates random values such as decimal and GUID.
    /// Implements IRandomizeService interface.
    /// </summary>
    public class RandomizeService : IRandomizeService
    {
        // Instance of Random class to generate random values.
        private readonly Random _Random = new Random();

        /// <summary>
        /// Generates a random decimal value between 30 and 1000, with a fractional part.
        /// </summary>
        /// <returns>A random decimal value rounded to two decimal places.</returns>
        public decimal RandomDecimal()
        {
            // Generate fractional part using NextDouble and convert it to decimal.
            decimal fractionalPart = (decimal)this._Random.NextDouble();

            // Generate the whole part of the price between 30 and 1000.
            decimal price = (decimal)this._Random.Next(30, 1000) + fractionalPart;

            // Return the generated price rounded to two decimal places.
            return Math.Round(price, 2);
        }

        /// <summary>
        /// Generates a new GUID (Globally Unique Identifier).
        /// </summary>
        /// <returns>A new GUID.</returns>
        public Guid RandomGuid()
        {
            // Return a newly generated GUID.
            return Guid.NewGuid();
        }
    }
}
