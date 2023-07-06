using Books.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Books.ViewModels
{
	public class BookFormViewModel
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(128)]
		public string Title { get; set; }

		[Required]
		[MaxLength(128)]
		public string Author { get; set; }


		[Required]
		[MaxLength(1024)]
		public string Description { get; set; }

		[Required]
		[Display(Name = "Category")]
		public byte CategoryId { get; set; }

		public IEnumerable<Category> Categories { get; set; }
	}
}