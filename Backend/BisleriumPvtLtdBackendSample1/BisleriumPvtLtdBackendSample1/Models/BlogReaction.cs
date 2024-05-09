using Microsoft.AspNetCore.Identity;

namespace BisleriumPvtLtdBackendSample1.Models
{
    public class BlogReaction
    {
        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; }= DateTime.Now;
        public string? UserId { get; set; }
        public Guid? BlogId { get; set; }
        public Guid? ReactionTypeId { get; set; }

        //Navigation Properties
        public IdentityUser? User { get; set; }
        public Blog? Blog { get; set; }
        public ReactionType? ReactionType { get; set; }
    }
}
