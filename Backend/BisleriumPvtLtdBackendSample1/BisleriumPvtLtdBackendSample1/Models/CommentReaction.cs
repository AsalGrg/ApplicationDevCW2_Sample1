using Microsoft.AspNetCore.Identity;

namespace BisleriumPvtLtdBackendSample1.Models
{
    public class CommentReaction
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public string UserId { get; set; }
        public Guid ReactionTypeId { get; set; }
        public DateTime AddedDate { get; set; }= DateTime.Now;
        public IdentityUser User { get; set; }
        public ReactionType ReactionType { get; set; }
        public Comment Comment { get; set; }
    }
}
