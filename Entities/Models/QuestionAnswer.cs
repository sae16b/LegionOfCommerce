using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class QuestionAnswer
  {
    public int QuestionAnswerId { get; set; }
    public int ProductProductId { get; set; }
    public int QuestionerId { get; set; }
    public int? AnswererId { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public DateTime CreationDate { get; set; }

    public virtual Product ProductProduct { get; set; }
  }
}
