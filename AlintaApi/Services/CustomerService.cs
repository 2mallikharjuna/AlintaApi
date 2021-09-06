using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AlintaApi.Domain.Models;
using AlintaApi.Repositories;
using Microsoft.Extensions.Logging;

namespace AlintaApi.Services
{
    /// <summary>
    /// Customer service implementaion
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _CustomerRepository;
        private ILogger<CustomerService> Logger { get; }

        /// <summary>
        /// CustomerService constructor
        /// </summary>
        /// <param name="CustomerRepository"></param>        
        /// <param name="logger"></param>
        public CustomerService(ICustomerRepository CustomerRepository, ILogger<CustomerService> logger)
        {
            _CustomerRepository = CustomerRepository ?? throw new ArgumentNullException(nameof(ICustomerService));
            Logger = logger ?? throw new ArgumentNullException(nameof(ILogger<CustomerService>));
        }

        /// <summary>
        /// Get all the Customer entiries from Customer table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _CustomerRepository.GetAllCustomersAsync();
        }

        /// <summary>
        /// Get the Customer entity from give Customer Id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetCustomersByNameAsync(string name)
        {
            return await _CustomerRepository.GetCustomersByNameAsync(name);
        }

        /// <summary>
        /// Add Customer entity to Customer table
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public async Task<Customer> AddCustomerAsync(Customer Customer)
        {
            return await _CustomerRepository.AddCustomerAsync(Customer);
        }

        /// <summary>
        /// update the Customer entity of given Customer Id
        /// </summary>
        /// <param name="Customer"></param>        
        /// <returns></returns>
        public async Task<Customer> UpdateCustomerAsync(Customer Customer)
        {
            return await _CustomerRepository.UpdateCustomerAsync(Customer);
        }
        /// <summary>
        /// Delete the Customer entity of given Customer Id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public async Task<Customer> DeleteCustomerAsync(Guid CustomerId)
        {
            return await _CustomerRepository.DeleteCustomerAsync(CustomerId);
        }

    }
}
