using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SendEmail.Models;

[Table("MessageBodyNight")]
public partial class MessageBodyNight
{
    [Key]
    [Column(TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("VN")]
    public string? Vn { get; set; }

    [Column("EN")]
    public string? En { get; set; }
}
