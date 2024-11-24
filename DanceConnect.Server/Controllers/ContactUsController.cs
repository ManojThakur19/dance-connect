using DanceConnect.Server.DataContext;
using DanceConnect.Server.Entities;
using DanceConnect.Server.Helpers;
using DanceConnect.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DanceConnect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly DanceConnectContext _context;

        public ContactUsController(DanceConnectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContactMessageById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact([FromBody] ContactUs contact)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContacts), new { id = contact.Id }, contact);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("reply")]
        public async Task<IActionResult> SendResponse([FromBody] MessageReply contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var message = await _context.Contacts.FindAsync(contact.ReplyTo);
            if (message == null)
                return BadRequest(ModelState);

            message.MessageResponse = contact.Message;
             
            bool isEmailSent = EmailHelper.SendEmail(message, "Confirmation Message");
            
            _context.Contacts.Update(message);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
