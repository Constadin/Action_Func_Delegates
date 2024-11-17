using System;

namespace Library.Services
{
    /// <summary>
    /// Interface that defines methods for generating random values.
    /// </summary>
    public interface IRandomizeService
    {
        /// <summary>
        /// Generates a new GUID (Globally Unique Identifier).
        /// </summary>
        /// <returns>A new GUID.</returns>
        Guid RandomGuid();

        /// <summary>
        /// Generates a random decimal value, typically used for prices or other numerical values.
        /// </summary>
        /// <returns>A random decimal value.</returns>
        decimal RandomDecimal();
    }
}
