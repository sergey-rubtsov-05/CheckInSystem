using System;

namespace Models.DTO
{
    public class CheckInDto
    {
        public int CheckInId { get; set; }
        public string PersonFullName { get; set; }
        public Sex PersonSex { get; set; }
        public DateTime PersonBirthDate { get; set; }
        public DateTime CheckInVisitDateTime { get; set; }
    }
}