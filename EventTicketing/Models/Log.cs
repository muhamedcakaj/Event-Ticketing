using System.ComponentModel.DataAnnotations;

namespace EventTicketing.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public string Action { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
