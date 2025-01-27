using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventTicketing.Models
{
    public class UserRelationship
    {
        [Required]
        public string FollowerId { get; set; }

        [Required]
        public string FollowedId { get; set; }

        [ForeignKey("FollowerId")]
        public ApplicationUser FollowerUser { get; set; }

        [ForeignKey("FollowedId")]
        public ApplicationUser FollowedUser { get; set; }
    }
}
