using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pgapp.Entities
{
	public class Comment
	{
        public int CommentId { get; set; }
		public string Author { get; set; } = string.Empty;
		public string CommentText { get; set; } = string.Empty;
        public int PostId { get; set; }
		[JsonIgnore]
		public Post? Post { get; set; }
	}
}