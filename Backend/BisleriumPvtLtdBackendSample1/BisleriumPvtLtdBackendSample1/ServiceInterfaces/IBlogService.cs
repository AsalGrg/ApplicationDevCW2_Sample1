using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.Models;
using Microsoft.Identity.Client;

namespace BisleriumPvtLtdBackendSample1.ServiceInterfaces
{
    public interface IBlogService
    {

        public List<BlogDetails> GetAllFilterBlogs(string filter);
        public List<Blog> GetUserBlogs( string userId);
        public BlogDetails GetBlogDetails(Guid blogId);
      /*  public Blog GetAllBlogs();*/
        public Task<BlogDetails> AddNewBlog(AddBlogRequest addBlogRequest);
        public Task<BlogDetails> EditBlog(AddBlogRequest addBlogRequest, Guid blogId);
        public BlogDetails DeleteBlog(Guid blogId);
        public UserBlogReactionDetail AddBlogReaction(AddReactionDto addReactionDto);
        public EachNotificationDetails DeleteBlogReaction (Guid reactionId);
        public UserBlogReactionDetail UpdateBlogReaction(Guid reactionId,AddReactionDto updatedReactionDto);
        public TimeAnalysisDetails GetTotalTimeAnalysis(TimeAnalysisRequest timeAnalysisRequest);
    }
}
