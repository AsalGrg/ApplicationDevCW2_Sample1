using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.Models;

namespace BisleriumPvtLtdBackendSample1.ServiceInterfaces
{
    public interface IBlogService
    {

        public BlogDetails GetBlogDetails(Guid blogId, Guid? accessingUserId);
        public List<Blog> GetAllBlogs();
        public BlogDetails AddNewBlog(AddBlogRequest addBlogRequest);
        public BlogDetails EditBlog(AddBlogRequest addBlogRequest, Guid blogId);
        public BlogDetails DeleteBlog(Guid blogId, Guid UserId);
        public EachNotificationDetails AddBlogReaction(AddReactionDto addReactionDto);
        public EachNotificationDetails DeleteBlogReaction (Guid reactionId);
        public EachNotificationDetails UpdateBlogReaction(Guid reactionId,AddReactionDto updatedReactionDto);
    }
}
