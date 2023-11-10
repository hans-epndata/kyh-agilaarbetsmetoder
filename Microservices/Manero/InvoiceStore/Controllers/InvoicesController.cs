using InvoiceStore.Contexts;
using InvoiceStore.Models;
using InvoiceStore.Models.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceStore.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly DataContext _context;

        public InvoicesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceFormRegistration form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            InvoiceEntity entity = form;
            _context.Invoices.Add(entity);
            await _context.SaveChangesAsync();

            return Created("", (Invoice)entity);
        }
    }
}
