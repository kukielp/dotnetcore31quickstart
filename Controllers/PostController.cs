using pgapp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace pgapp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public PostsController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var posts = _context.Posts
						.Include(c => c.Comments);

			return Ok(posts);
		}

		[HttpGet("{id}", Name = "GetById")]
		public IActionResult GetById(Guid id)
		{
			var post = _context.Posts
                       .Where(p => p.PostId == id)
                       .Include(c => c.Comments)
                       .FirstOrDefault();

			return Ok(post);
		}

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Posts
        ///     {
        ///        "name": "The name of the post",
        ///        "dateOfPost": "2020-05-24T08:25:38.861Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
		public IActionResult Post([FromBody] Post post)
		{
			_context.Add(post);

			_context.SaveChanges();

			return CreatedAtRoute(nameof(GetById), new { id = post.PostId }, post);
		}

        [HttpPost("{id}/addcomment", Name = "AddComment")]
        public ActionResult AddComment(Guid id, [FromBody] Comment comment)
        {

            comment.PostId = id;

			_context.Add(comment);
			_context.SaveChanges();

			var result = _context.Posts
                       .Where(p => p.PostId == id)
					   .Include(c => c.Comments)
					   .FirstOrDefault();
			
			return CreatedAtRoute(nameof(AddComment), result);
		}
	}
}