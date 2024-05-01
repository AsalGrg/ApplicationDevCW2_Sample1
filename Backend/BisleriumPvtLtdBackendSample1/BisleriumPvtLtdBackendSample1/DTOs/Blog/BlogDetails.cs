using BisleriumPvtLtdBackendSample1.DTOs.Comment;

namespace BisleriumPvtLtdBackendSample1.DTOs.Blog
{
    public class BlogDetails
    {
        public string Title { get; set; }
        public string Body{  get; set; }
        public string CoverImage { get; set; }
        public DateTime CreatedDate {  get; set; }
        public int NoOfDownVotes { get; set; }
        public int NoOfUpVotes { get; set; }
        public double BlogPopularityCalculation { get; set; }
        public UserDetailsSnippet AuthorDetails{ get; set; }
        public List<EachCommentDetail> BlogComments { get; set; }
        public bool HasReacted { get; set; } = false;
        public UserBlogReactionDetail? BlogReactionDetail { get; set; } = null;
    }
}
