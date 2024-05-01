namespace BisleriumPvtLtdBackendSample1.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public Guid BlogId { get; set; }
        public DateTime AddedDate{ get; set; } = DateTime.Now;

        //Navigation Properties
        public User User { get; set; }
        public Blog Blog { get; set; }
    }
}
