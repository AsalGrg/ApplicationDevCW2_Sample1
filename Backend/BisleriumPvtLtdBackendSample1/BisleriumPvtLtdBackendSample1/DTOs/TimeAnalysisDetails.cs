using BisleriumPvtLtdBackendSample1.DTOs.Blog;

namespace BisleriumPvtLtdBackendSample1.DTOs
{
    public class TimeAnalysisDetails
    {

        public int TotalPosts {  get; set; }
        public int TotalUpvotes { get; set; }
        public int TotalDownvotes { get; set; }
        public int TotalComments { get; set; }
        public List<BlogDetails> TopBlogs {  get; set; }
    }
}
