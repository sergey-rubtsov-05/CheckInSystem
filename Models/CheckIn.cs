using System;

namespace Models
{
    public class CheckIn : Entity
    {
        public Person Person { get; set; }
        public int PersonId { get; set; }

        public DateTime VisitDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
