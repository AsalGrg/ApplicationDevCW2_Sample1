namespace BisleriumPvtLtdBackendSample1.Models
{
    public class CommentReaction
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid ReactionTypeId { get; set; }

        public User User { get; set; }
        public ReactionType ReactionType { get; set; }
        public Comment Comment { get; set; }
    }
}
