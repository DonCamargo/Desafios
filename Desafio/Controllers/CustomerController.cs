using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Desafio.Models;
using System.Text.RegularExpressions;

namespace Desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CustomerController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomerModel()
        {
            return await _context.CustomerModel.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerModel(long id)
        {
            var customerModel = await _context.CustomerModel.FindAsync(id);

            if (customerModel == null)
            {
                return NotFound("Cliente não encontrado. Tente novamente");
            }

            return customerModel;
        }

        [HttpGet("{document}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerByDocument(string document)
        {
            var customerbyDocument = await _context.CustomerModel.FindAsync(document);
            if (customerbyDocument == null)
            {
                return NotFound("Cliente não encontrado. Tente inserir o documento novamente");
            }
            return customerbyDocument;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerModel>> GetCustomerByEmail(string email)
        {
            var customerByEmail = await _context.CustomerModel.FindAsync(email);

            if (customerByEmail == null)
            {
                return NotFound("Cliente não encontrado. Tente inserir o email novamente.");
            }

            return customerByEmail;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerModel(long id, CustomerModel customerModel)
        {
            if (id != customerModel.Id)
            {
                return BadRequest("Cliente não encontrado. Tente novamente");
            }

            _context.Entry(customerModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerModelExists(id))
                {
                    return NotFound("Cliente não cadastrado");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomerModel(CustomerModel customerModel)
        {
            _context.CustomerModel.Add(customerModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerModel", new { id = customerModel.Id }, customerModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerModel>> DeleteCustomerModel(long id)
        {
            var customerModel = await _context.CustomerModel.FindAsync(id);
            if (customerModel == null)
            {
                return NotFound();
            }

            _context.CustomerModel.Remove(customerModel);
            await _context.SaveChangesAsync();

            return customerModel;
        }

        private bool CustomerModelExists(long id)
        {
            return _context.CustomerModel.Any(e => e.Id == id);
        }
    }
}
