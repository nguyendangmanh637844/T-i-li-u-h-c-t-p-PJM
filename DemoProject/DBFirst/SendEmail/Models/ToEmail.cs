using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SendEmail.Models;

[Table("ToEmail")]
public partial class ToEmail
{
    [Key]
    [Column(TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    public string? Email { get; set; }
}
