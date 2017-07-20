using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class CheckInDto
    {
        public int CheckInId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string PersonFirstName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string PersonLastName { get; set; }
        public string PersonFullName => $"{PersonFirstName} {PersonLastName}";

        [Required]
        public Sex PersonSex { get; set; }

        [Required]
        public DateTime PersonBirthDate { get; set; }

        [Required]
        public DateTime CheckInVisitDateTime { get; set; }
    }
}