using System;
using System.Threading.Tasks;
using AlintaApi.Domain.Models;
using System.Collections.Generic;

namespace AlintaApi.Repositories
{
    /// <summary>
    /// Customer Reposiotory definition
    /// </summary>
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        /// <summary>
        /// Get the customers matched with first or last name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetCustomersByNameAsync(string name);
        /// <summary>
        /// Add the new customer record
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        Task<Customer> AddCustomerAsync(Customer Customer);
        /// <summary>
        /// update the existing customer record
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        Task<Customer> UpdateCustomerAsync(Customer Customer);
        /// <summary>
        /// Delete the customer record
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<Customer> DeleteCustomerAsync(Guid CustomerId);
    }
}
