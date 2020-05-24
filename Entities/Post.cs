using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pgapp.Entities
{
	public class Post
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid PostId { get; set; }
		public string Name { get; set; }
		public int Count { get; set; }
        public DateTimeOffset DateOfPost { get; set; }
		public List<Comment> Comments { get; set; }
	}
}