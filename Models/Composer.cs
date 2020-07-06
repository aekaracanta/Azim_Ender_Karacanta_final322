using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
	public class Composer
	{
		public int ComposerId { get; set; }
		public string ComposerName { get; set; }
		public string ComposerSurname { get; set; }
		public string ComposerEmail { get; set; }
		public int? ComposerGrade { get; set; }

		public ICollection<Song> ComposerSongs { get; set; }
	}
}
