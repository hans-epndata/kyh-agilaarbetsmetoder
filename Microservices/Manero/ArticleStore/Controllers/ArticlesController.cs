using ArticleStore.Contexts;
using ArticleStore.Models;
using ArticleStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleStore.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DataContext _context;

        public ArticlesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (await _context.Articles.AnyAsync(x => x.ArticleNumber == req.ArticleNumber))
                return Conflict($"Article Number {req.ArticleNumber} already exists in article registry.");

            ArticleEntity entity = req;
            _context.Articles.Add(entity);
            await _context.SaveChangesAsync();

            return Created("", entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _context.Articles.Select(entity => (Article)entity).ToListAsync();
            return Ok(articles);
        }

        [HttpGet("{articleNumber}")]
        public async Task<IActionResult> Get(string articleNumber)
        {
            var articleEntity = await _context.Articles.FirstOrDefaultAsync(x => x.ArticleNumber == articleNumber);
            if (articleEntity == null)
                return NotFound($"Article with {articleNumber} was not found.");

            return Ok((Article)articleEntity);
        }
    }
}
