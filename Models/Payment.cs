using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
	public class Payment
	{
		public int PaymentId { get; set; }
		public bool PaymentCompleted { get; set; }
		public DateTime PaymentDate { get; set; }

		public int CustomerId { get; set; }
		public int SongId { get; set; }

		public Customer Customer { get; set; }
		public Song Song { get; set; }

	}
}
