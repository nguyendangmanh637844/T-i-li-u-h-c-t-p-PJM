using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SendEmail.Models;

public partial class Email
{
    [Key]
    public int Id { get; set; }

    public string? To { get; set; }

    public string? Subject { get; set; }

    public string? MessageBodyDay { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime? SendDate { get; set; }

    public DateTime? Intention { get; set; }

    public int Attempts { get; set; }

    public bool UsingTemplate { get; set; }

    public string? ErrorMessage { get; set; }
}
