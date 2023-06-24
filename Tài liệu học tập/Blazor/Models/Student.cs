using System;
using System.ComponentModel.DataAnnotations;

namespace DemoBlazor.Models
{
    public class Student
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string ClassId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}