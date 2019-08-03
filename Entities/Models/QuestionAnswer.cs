using System;
using System.Collections.Generic;

namespace Entities.Models
{
	public partial class QuestionAnswer
	{
		public int QuestionAnswerId { get; set; }
		public int ProductProductId { get; set; }
		public string QuestionerId { get; set; }
		public string AnswererId { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
		public DateTime CreationDate { get; set; }

		public virtual Product ProductProduct { get; set; }
	}
}
