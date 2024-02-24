using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.WebAPI.Data;
using CRUD.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Contacts : Controller
    {
        private readonly ContactsDbContext dbContext;

        public Contacts(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        //get single id
        //name of the parametert needs to match the name inside the route in order to
        //retrieve the values from it

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContacts([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
           if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
           
        }



        [HttpGet]

        // GET: /<controller>/
        public IActionResult GetContact()
        {
           return Ok(dbContext.Contacts.ToList());
        }

        [HttpPost]


        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        //the name in the parameter has to match the name in the route field
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();
                return Ok(contact );

            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]


        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                dbContext.Remove(contact);
               await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

    }
}

