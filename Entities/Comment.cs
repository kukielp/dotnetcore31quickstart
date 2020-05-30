using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pgapp.Entities
{
	public class Comment
	{
        public int CommentId { get; set; }
		public string Author { get; set; }
		public string CommentText { get; set; }
        public int PostId { get; set; }
		[JsonIgnore]
		public Post Post { get; set; }
	}
}