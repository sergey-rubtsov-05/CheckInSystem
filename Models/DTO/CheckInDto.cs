using System;

namespace Models.DTO
{
    public class CheckInDto
    {
        public int CheckInId { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonFullName => $"{PersonFirstName} {PersonLastName}";
        public Sex PersonSex { get; set; }
        public DateTime PersonBirthDate { get; set; }
        public DateTime CheckInVisitDateTime { get; set; }
    }
}