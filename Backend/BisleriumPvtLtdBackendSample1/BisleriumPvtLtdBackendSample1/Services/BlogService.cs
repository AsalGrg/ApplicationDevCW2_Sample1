using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.DTOs.Comment;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;

namespace BisleriumPvtLtdBackendSample1.Services
{
    public class BlogService : IBlogService
    {
        private BisleriumBlogDbContext _dbContext;
        private ICommentService _commentService;

        public BlogService(BisleriumBlogDbContext dbContext, ICommentService commentService)
        {
            _dbContext = dbContext;
            _commentService = commentService;
        }


        public BlogDetails GetBlogDetails(Guid blogId, Guid? accessingUserId)
        {
            System.Diagnostics.Debug.WriteLine("dobeee");
            System.Diagnostics.Debug.WriteLine(blogId);
            System.Diagnostics.Debug.WriteLine(accessingUserId);
            
            System.Diagnostics.Debug.WriteLine("dobeee");

            Blog blog = _dbContext.Blogs.FirstOrDefault(each => each.Id == blogId);

            System.Diagnostics.Debug.WriteLine("Yerrrra");
            System.Diagnostics.Debug.WriteLine(blog.Title);

            User author = _dbContext.Users.FirstOrDefault(each => each.Id == blog.UserId);

            System.Diagnostics.Debug.WriteLine(author.Name);
            bool hasReacted = false;
            UserBlogReactionDetail blogReactionDetail = null;


            List<EachCommentDetail> blogComments = _commentService.GetAllBlogComments(blogId, accessingUserId);

            if (accessingUserId != null)
            {
                System.Diagnostics.Debug.WriteLine("Yerrrra YYYEYEYE");

                BlogReaction userBlogReaction = _dbContext.BlogReactions.FirstOrDefault(each => each.BlogId == blogId && each.UserId == accessingUserId);

                if (userBlogReaction != null)
                {
                    hasReacted= true;
                    blogReactionDetail = new()
                    {
                        ReactionId = userBlogReaction.Id,
                        ReactionName = _dbContext.ReactionTypes.FirstOrDefault(each => each.Id == userBlogReaction.ReactionTypeId).Title
                    };
                }
            }
            return new BlogDetails()
            {
                Title = blog.Title,
                Body = blog.Body,
                CoverImage = blog.CoverImage,
                CreatedDate = blog.AddedDate,
                NoOfUpVotes= CalculateNoOfReactions(blog.Id, "Upvote"),
                NoOfDownVotes= CalculateNoOfReactions(blog.Id, "Downvote"),
                BlogPopularityCalculation= CalculatePopularity(blog.Id),
                AuthorDetails = new(){
                    UserDp = "",
                    UserId = author.Id,
                    Username = author.Name,
                },

                BlogComments = blogComments,
                HasReacted= hasReacted,
                BlogReactionDetail= blogReactionDetail
            };
        }


        //utility method
        private int CalculatePopularity (Guid blogId)
        {
            Blog blog = _dbContext.Blogs.FirstOrDefault(each => each.Id == blogId);

            int noOfUpvotes = CalculateNoOfReactions(blogId, "Upvote");
            int noOfDownVotes = CalculateNoOfReactions(blogId, "Downvote");
            int noOfComments = GetNoOfComments(blogId);

            int popularity = 2 * noOfUpvotes + (-1) * noOfDownVotes + 1 * noOfComments;

            return popularity;
        }

        private int CalculateNoOfReactions(Guid blogId, string reactionTypeTitle)
        {
            ReactionType reactionType = _dbContext.ReactionTypes.FirstOrDefault(each => each.Title == reactionTypeTitle);

            List<BlogReaction> blogReactions = _dbContext.BlogReactions.Where(each => each.BlogId == blogId && each.ReactionTypeId == reactionType.Id).ToList();

            return blogReactions.Count;
        }

        private int GetNoOfComments (Guid blogId)
        {
            System.Diagnostics.Debug.WriteLine("YYYYYY");
            System.Diagnostics.Debug.WriteLine(blogId);
            List<Comment> blogComments = _dbContext.Comments.Where(each=> each.BlogId== blogId).ToList();
            System.Diagnostics.Debug.WriteLine(blogComments.Count);
            return blogComments.Count;
        }
        //utility methods ends

        public EachNotificationDetails AddBlogReaction(AddReactionDto addReactionDto)
        {

            ReactionType reactionType = _dbContext.ReactionTypes.First(each => each.Title == addReactionDto.ReactionType);
            Blog blog = _dbContext.Blogs.First(each => each.Id == addReactionDto.BlogId);
            User user = _dbContext.Users.First(each => each.Id == addReactionDto.UserId);

            BlogReaction savedReaction = _dbContext.Add(
                new BlogReaction()
                {
                    User= user,
                    ReactionType = reactionType,
                    Blog= blog,
                }
                ).Entity;

            _dbContext.SaveChanges();


            return ChangeToNotificationDetails(savedReaction);
        }

        public BlogDetails AddNewBlog(AddBlogRequest addBlogRequest)
        {
            Blog addedBlog = _dbContext.Add(
                new Blog() { 

                    Title = addBlogRequest.Title,
                    Body = addBlogRequest.Body,
                    CoverImage= addBlogRequest.CoverImage.FileName,
                    AddedDate= DateTime.Now,
                    User = _dbContext.Users.FirstOrDefault(each => each.Id== addBlogRequest.UserId)
                }
                ).Entity;

            _dbContext.SaveChanges();

            return ChangeBlogModalToBlogDetails( addedBlog );
        }

        public BlogDetails DeleteBlog(Guid blogId, Guid UserId)
        {
            Blog blog= _dbContext.Blogs.First(each => each.Id==blogId);

            if (!blog.UserId.Equals(UserId)) return null;

            _dbContext.Remove(blog);
            _dbContext.SaveChanges();

            return ChangeBlogModalToBlogDetails(blog);
        }

        public EachNotificationDetails DeleteBlogReaction(Guid reactionId)
        {
            BlogReaction blogReaction = _dbContext.BlogReactions.FirstOrDefault(each => each.Id == reactionId);
            BlogReaction removedBlogReaction = _dbContext.Remove(blogReaction).Entity;
            _dbContext.SaveChanges();
            return ChangeToNotificationDetails(removedBlogReaction);
        }


        public EachNotificationDetails UpdateBlogReaction(Guid reactionId,AddReactionDto addReactionDto)
        {
            BlogReaction blogReaction = _dbContext.BlogReactions.FirstOrDefault(each => each.Id == reactionId);

            blogReaction.ReactionType= _dbContext.ReactionTypes.FirstOrDefault(each => each.Title== addReactionDto.ReactionType );
            BlogReaction removedBlogReaction = _dbContext.Update(blogReaction).Entity;
            _dbContext.SaveChanges();
            return ChangeToNotificationDetails(removedBlogReaction);
        }



        public BlogDetails EditBlog(AddBlogRequest addBlogRequest, Guid blogId)
        {
            bool hasError = false;

            Blog existingBlog = _dbContext.Blogs.FirstOrDefault(b => b.Id == blogId);

            System.Diagnostics.Debug.WriteLine(addBlogRequest.UserId);
            System.Diagnostics.Debug.WriteLine(existingBlog.UserId);
            if (!addBlogRequest.UserId.Equals(existingBlog.UserId)) return null;


            existingBlog.Title = addBlogRequest.Title;
            existingBlog.Body = addBlogRequest.Body;
            existingBlog.CoverImage = addBlogRequest.CoverImage.FileName;


            Blog updatedBlog = _dbContext.Update(existingBlog).Entity;

            _dbContext.SaveChanges();
            return ChangeBlogModalToBlogDetails(updatedBlog);
        }
        


        public List<Blog> GetAllBlogs()
        {
            return _dbContext.Blogs.ToList();
        }


        private BlogDetails ChangeBlogModalToBlogDetails (Blog blog)
        {

            User blogAuthor = _dbContext.Users.FirstOrDefault(each => (
                                each.Id == blog.UserId
                                ));
            return new()
            {
                Body = blog.Body,
                Title = blog.Title,
                CoverImage = blog.CoverImage,
                CreatedDate = blog.AddedDate,
                AuthorDetails = new()
                {
                    UserId = blogAuthor.Id,
                    Username = blogAuthor.Name,
                    UserDp=""
                }
            };
        }

        private EachNotificationDetails ChangeToNotificationDetails(BlogReaction blogReaction)
        {
            return new()
            {
                AddedDate = blogReaction.AddedDate,
                NotificationType = "Reaction",
                UserDp = "",
                Username = _dbContext.Users.First(each => each.Id == blogReaction.UserId).Name
            };
        }

     
    }
}
