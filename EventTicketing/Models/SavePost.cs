using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventTicketing.Models
{
    public class SavePost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}