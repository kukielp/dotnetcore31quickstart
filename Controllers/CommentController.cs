using pgapp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace pgapp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public CommentsController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var comment = _context.Comments;

			return Ok(comment);
		}

		[HttpGet("{id}", Name = "GetCommentById")]
		public IActionResult GetById(int id)
		{
			var comment = _context.Find<Comment>(id);

			return Ok(comment);
		}

		[HttpPost]
		public IActionResult Comment([FromBody] Comment comment)
		{
			_context.Add(comment);

			_context.SaveChanges();

			return CreatedAtRoute(nameof(GetById), new { id = comment.CommentId }, comment);
		}
	}
}