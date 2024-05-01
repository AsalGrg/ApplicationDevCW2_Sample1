namespace BisleriumPvtLtdBackendSample1.DTOs.Blog
{
    public class AddBlogRequest
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile CoverImage {  get; set; }
    }
}
