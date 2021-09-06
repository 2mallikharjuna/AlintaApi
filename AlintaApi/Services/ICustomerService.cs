using System;
using AlintaApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlintaApi.Services
{
    /// <summary>
    /// ICustomer service definition
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get All the Customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        /// <summary>
        /// Get the Customer by Guid
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetCustomersByNameAsync(string name);
        /// <summary>
        /// Add the Customer entry
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        Task<Customer> AddCustomerAsync(Customer Customer);
        /// <summary>
        /// Update the Customer entry
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="Customer"></param>
        /// <returns></returns>
        Task<Customer> UpdateCustomerAsync(Customer Customer);
        /// <summary>
        /// Delete the Customer entry
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<Customer> DeleteCustomerAsync(Guid CustomerId);
    }
}
