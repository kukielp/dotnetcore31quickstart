using System;
using System.ComponentModel.DataAnnotations;

namespace pgapp.Entities
{
	public class Comment
	{
        public int CommentId { get; set; }
		public string Author { get; set; }
		public string CommentText { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
	}
}