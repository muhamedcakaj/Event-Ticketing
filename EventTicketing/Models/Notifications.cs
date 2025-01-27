using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EventTicketing.Models
{
    public class Notifications
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
