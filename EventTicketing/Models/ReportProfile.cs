using EventTicketing.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class ReportProfile
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }  // The user being reported

    [Required]
    public string ReportedById { get; set; }  // The user who is reporting

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [ForeignKey("ReportedById")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ApplicationUser ReportedBy { get; set; } // Relationship for the user who made the report
}