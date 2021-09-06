using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AlintaApi.Domain.Models;
using System.Collections.Generic;
using AlintaApi.Filters;
using AlintaApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace AlintaApi.Controllers
{
    /// <summary>
    /// Customer controller implmentaion
    /// </summary>
    [ApiController]
    [Route("api/Customers")]
    public class CustomersController : ControllerBase
    {
        #region Members
        private readonly ICustomerService _CustomerService;
        private ILogger<CustomersController> Logger { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CustomerService">Inject ICustomerService</param>
        /// <param name="logger">Inject CustomersController logger</param>
        public CustomersController(ICustomerService CustomerService, ILogger<CustomersController> logger)
        {
            _CustomerService = CustomerService ?? throw new ArgumentNullException(nameof(ICustomerService));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion
        #region Controller Methods
        /// <summary>
        /// HttpGet Method to return the all the Customers
        /// </summary>        
        /// <returns>Customers in json format</returns>         
        [HttpGet]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var result = await _CustomerService.GetAllCustomersAsync();
                if (result != null && result.Count() != 0)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get All Customers. Request: " + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAllCustomersAsync. Request: " + " Error " + ex);
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// HttpGet to return the matching customers
        /// </summary>        
        /// <returns>Customers in json format</returns>         
        [HttpGet("{name}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> GetCustomersByName([Required]string name)
        {            
            try
            {
                var result = await _CustomerService.GetCustomersByNameAsync(name);
                if (result.Count() != 0)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get Matching Customer. Request: " + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetCustomerByIdAsync. Request: " + " Error " + ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// HttpPost method to Create the customer using 
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [HttpPost("CreateCustomer")]
        [RequestValidate]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer Customer)
        {
            try
            {
                var entity = await _CustomerService.AddCustomerAsync(Customer);
                if (entity != null)
                    return new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Create Customer. Request: " + JsonConvert.SerializeObject(Customer) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateCustomerAsync. Request: " + JsonConvert.SerializeObject(Customer) + " Error " + ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Httpput method to update the customer details
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [HttpPut("UpdateCustomer")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer Customer)
        {
            try
            {
                var result = await _CustomerService.UpdateCustomerAsync( Customer);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Update Customer. Request: " + JsonConvert.SerializeObject(Customer) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in UpdateCustomerAsync. Request: " + JsonConvert.SerializeObject(Customer) + " Error " + ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// HttpDelete method to delete the customer
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpDelete("{CustomerId}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> DeleteCustomer([Required]Guid CustomerId)
        {
            try
            {
                var response = await _CustomerService.DeleteCustomerAsync(CustomerId);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Delete Customer. Request: " + JsonConvert.SerializeObject(CustomerId) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Get DeleteCustomerAsync. Request: " + JsonConvert.SerializeObject(CustomerId) + " Error " + ex);
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
