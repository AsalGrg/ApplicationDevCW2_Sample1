namespace BisleriumPvtLtdBackendSample1.DTOs.Blog
{
    public class AddReactionDto
    {
        public string ReactionType { get; set; }
        public Guid BlogId {  get; set; }
    }
}
