using System;
using System.ComponentModel.DataAnnotations;

namespace DemoBlazor.Models
{
    public class MyClass
    {
        public MyClass()
        { }

        public MyClass(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [Required]
        public string ClassId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}