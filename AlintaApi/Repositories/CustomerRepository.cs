using System;
using System.Linq;
using System.Threading.Tasks;
using AlintaApi.Domain.Models;
using System.Collections.Generic;

namespace AlintaApi.Repositories
{
    /// <summary>
    /// Customer Repository implementation
    /// </summary>
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        IQueryable<Customer> _Customers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appDbContext"></param>
        public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            appDbContext.Database.EnsureCreated();
            _Customers = GetAll();
        }

        /// <summary>
        /// Read all the customers
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return _Customers.ToList();
        }

        /// <summary>
        /// Read the customer matching with first/last name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetCustomersByNameAsync(string name)
        {
            var foundCustomers = _Customers.Where(pItem => pItem.FirstName.ToUpper().Contains(name.ToUpper()) || pItem.LastName.ToUpper().Contains(name.ToUpper())).ToList();            
            return foundCustomers;
        }

        /// <summary>
        /// Add the customer to repository
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public async Task<Customer> AddCustomerAsync(Customer Customer)
        {
            var findCustomer = _Customers.FirstOrDefault(pItem => pItem.Id == Customer.Id);
            if (findCustomer != null) throw new ArgumentException($"Customer already exists with CustomerID {Customer.Id}");
            return await AddAsync(Customer);
        }

        /// <summary>
        /// Update the existing customer details 
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public async Task<Customer> UpdateCustomerAsync(Customer Customer)
        {           
            var findCustomer = _Customers.FirstOrDefault(pItem => pItem.Id == Customer.Id);
            if (findCustomer == null) throw new ArgumentException($"Customer Not exists with CustomerID {Customer.Id}");
            return await UpdateAsync(Customer);
        }

        /// <summary>
        /// Delete the existing Customer 
        /// </summary>
        /// /// <param name="CustomerId"></param>
        /// <returns></returns>
        public async Task<Customer> DeleteCustomerAsync(Guid CustomerId)
        {
            var findCustomer = _Customers.FirstOrDefault(pItem => pItem.Id == CustomerId);
            if (findCustomer == null) throw new ArgumentException($"Customer Not exists with CustomerID {CustomerId}");
            return await DeleteAsync(_Customers.FirstOrDefault(pItem => pItem.Id == CustomerId));
        }
    }
}
