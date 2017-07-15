using System;

namespace Models
{
    public class CheckIn : Entity
    {
        public string Visitor { get; set; }
        public Sex Sex { get; set; }
        public DateTime VisitDateTime { get; set; }
    }
}
