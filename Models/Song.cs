using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
	public class Song
	{
		public int SongId { get; set; }
		public string SongName { get; set; }
		public string SongLyrics { get; set; }
		public int? SongLength { get; set; }
		public int? SongYear { get; set; }
		public int ComposerId { get; set; }
		public int CategoryId { get; set; }

		public int SongPrice { get; set; }

		public Composer SongComposer { get; set; }
		public Category SongCategory { get; set; }

		public ICollection<Payment> SongPayments { get; set; }

	}
}
