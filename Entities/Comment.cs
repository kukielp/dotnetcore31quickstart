using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pgapp.Entities
{
	public class Comment
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid CommentId { get; set; }
		public string Author { get; set; }
		public string CommentText { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
	}
}