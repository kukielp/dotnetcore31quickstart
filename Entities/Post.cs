using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pgapp.Entities
{
	public class Post
	{
        [Key]
        public int PostId { get; set; }
		public string Name { get; set; }
		public int Count { get; set; }
        public DateTimeOffset DateOfPost { get; set; }
		public List<Comment> Comments { get; set; }
	}
}