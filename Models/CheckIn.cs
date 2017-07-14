using System;

namespace Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        public string Visitor { get; set; }
        public Sex Sex { get; set; }
        public DateTime VisitDateTime { get; set; }
    }

    public enum Sex
    {
        Undefined = 0,
        Male = 1,
        Female = 2
    }
}
