using BisleriumPvtLtdBackendSample1.DbContext;
using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.DTOs.Comment;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Globalization;
using System.Security.Claims;
using System.Web;

namespace BisleriumPvtLtdBackendSample1.Services
{
    public class BlogService : IBlogService
    {
        private BisleriumBlogDbContext _dbContext;
        private ICommentService _commentService;
        private FileUploadService _fileUploadService;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BlogService(BisleriumBlogDbContext dbContext, ICommentService commentService, UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor, FileUploadService fileUploadService)
        {
            _dbContext = dbContext;
            _commentService = commentService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _fileUploadService = fileUploadService;
        }


        public BlogDetails GetBlogDetails(Guid blogId)
        {
            string accessingUserId = getCurrentUser();

            Blog blog = _dbContext.Blogs.FirstOrDefault(each => each.Id == blogId);

            IdentityUser author = _dbContext.Users.FirstOrDefault(each => each.Id == blog.UserId);

            System.Diagnostics.Debug.WriteLine(author.UserName);
            bool hasReacted = false;
            bool isAuthor = false;
            UserBlogReactionDetail blogReactionDetail = null;


            List<EachCommentDetail> blogComments = _commentService.GetAllBlogComments(blogId, accessingUserId);

            if (accessingUserId != null)
            {
                if (accessingUserId.Equals(blog.UserId)) isAuthor = true;
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
                Id = blogId,
                isAuthor= isAuthor,
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
                    Username = author.UserName,
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

        public UserBlogReactionDetail AddBlogReaction(AddReactionDto addReactionDto)
        {

            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);

            ReactionType reactionType = _dbContext.ReactionTypes.First(each => each.Title == addReactionDto.ReactionType);
            Blog blog = _dbContext.Blogs.First(each => each.Id == addReactionDto.BlogId);


            BlogReaction savedReaction = _dbContext.Add(
                new BlogReaction()
                {
                    UserId= userId,
                    ReactionType = reactionType,
                    Blog= blog,
                }
                ).Entity;

            _dbContext.SaveChanges();


            return new()
            {
                ReactionId = savedReaction.Id,
                ReactionName = addReactionDto.ReactionType
            };
        }

        public async Task<BlogDetails> AddNewBlog(AddBlogRequest addBlogRequest)
        {
             string userId = getCurrentUser();

            Blog addedBlog = _dbContext.Add(
                new Blog() {
                    Title = addBlogRequest.Title,
                    Body = addBlogRequest.Body,
                    CoverImage= await _fileUploadService.UploadFileAsync(addBlogRequest.CoverImage),
                    AddedDate= DateTime.Now,
                    UserId= userId
                }
                ).Entity;

            _dbContext.SaveChanges();

            return ChangeBlogModalToBlogDetails( addedBlog );
        }

        public BlogDetails DeleteBlog(Guid blogId)
        {
            string UserId = getCurrentUser();
            Blog blog= _dbContext.Blogs.First(each => each.Id==blogId);

            if (!blog.UserId.Equals(UserId)) return null;

            var reactions = _dbContext.BlogReactions.Where(each => each.BlogId == blogId).ToList();

            _dbContext.BlogReactions.RemoveRange(reactions);

            var comments = _dbContext.Comments.Where(each => each.BlogId==blogId).ToList();
            foreach (var comment in comments)
            {
                var commentReactions = _dbContext.CommentReactions.Where(each => each.CommentId == comment.Id).ToList();
                _dbContext.CommentReactions.RemoveRange(commentReactions);
            }

            _dbContext.Comments.RemoveRange(comments);
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


        public UserBlogReactionDetail UpdateBlogReaction(Guid reactionId,AddReactionDto addReactionDto)
        {
            BlogReaction blogReaction = _dbContext.BlogReactions.FirstOrDefault(each => each.Id == reactionId);

            blogReaction.ReactionType= _dbContext.ReactionTypes.FirstOrDefault(each => each.Title== addReactionDto.ReactionType );
            BlogReaction updatedBlogReaction = _dbContext.Update(blogReaction).Entity;
            _dbContext.SaveChanges();
            return new()
            {
                ReactionId = updatedBlogReaction.Id,
                ReactionName = addReactionDto.ReactionType
            }; ;
        }



        public async Task<BlogDetails> EditBlog(AddBlogRequest addBlogRequest, Guid blogId)
        {
            bool hasError = false;

            string currentUserId = getCurrentUser();

            Blog existingBlog = _dbContext.Blogs.FirstOrDefault(b => b.Id == blogId);

            if (!currentUserId.Equals(existingBlog.UserId)) return null;


            existingBlog.Title = addBlogRequest.Title;
            existingBlog.Body = addBlogRequest.Body;
            existingBlog.CoverImage = addBlogRequest.CoverImage!=null? await _fileUploadService.UploadFileAsync(addBlogRequest.CoverImage): existingBlog.CoverImage;

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

            IdentityUser blogAuthor = _dbContext.Users.FirstOrDefault(each => (
                                each.Id == blog.UserId
                                ));
            return new()
            {
                Id= blog.Id,
                Body = blog.Body,
                Title = blog.Title,
                CoverImage = blog.CoverImage,
                CreatedDate = blog.AddedDate,
                AuthorDetails = new()
                {
                    UserId = blogAuthor.Id,
                    Username = blogAuthor.UserName,
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
                Username = _dbContext.Users.First(each => each.Id == blogReaction.UserId).UserName
            };
        }
        private string getCurrentUser()
    {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);

            return userId;
        }

        public List<Blog> GetUserBlogs(string userId)
        {

            return _dbContext.Blogs.Where(each => each.UserId == userId).ToList();
        }

        public List<BlogDetails> GetAllFilterBlogs(string filter)
        {
            
            List<Blog> allBlogs = _dbContext.Blogs.ToList();

            List<BlogDetails> blogDetails = new();
            foreach (var blog in allBlogs)
            {
                blogDetails.Add(GetBlogDetails(blog.Id));
            }

            if (filter == "Random")
            {
                return blogDetails;
            }
            else if (filter== "Recency")
            {
                blogDetails = blogDetails.OrderByDescending(b => b.CreatedDate).ToList();
            }
            else if (filter == "Popularity")
            {
                blogDetails = blogDetails.OrderByDescending(b => b.BlogPopularityCalculation).ToList();
            }

            return blogDetails;

        }

        public TimeAnalysisDetails GetTotalTimeAnalysis(TimeAnalysisRequest timeAnalysisRequest)
        {

            List<Blog> allBlogs = new();
            List<BlogDetails> blogDetails = new();


            int totalUpvotes = 0;
            int totalDownvotes = 0;
            int noOfComments = 0;

            if(timeAnalysisRequest.SelectedDateOption=="All Time")
            {
                allBlogs = GetAllBlogs();
            }else if(timeAnalysisRequest.SelectedDateOption == "Select Date")
            {
                string inputString = timeAnalysisRequest.SelectedCalendarDate;
                string formatString = "yyyy-MM-dd";

                int monthNumber;
                int year;

                if (DateTime.TryParseExact(inputString, formatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    allBlogs = _dbContext.Blogs.Where(each => each.AddedDate.Month.Equals(dateTime.Month) && each.AddedDate.Year.Equals(dateTime.Year)).ToList();
                }
            }

            foreach (var blog in allBlogs)
            {
                totalUpvotes += CalculateNoOfReactions(blog.Id, "Upvote");
                totalDownvotes +=CalculateNoOfReactions(blog.Id, "Downvote");
                noOfComments +=GetNoOfComments(blog.Id);
                blogDetails.Add(GetBlogDetails(blog.Id));

            }
            return new()
            {
                TopBlogs= blogDetails
                          .OrderByDescending(b => b.BlogPopularityCalculation)
                          .Take(10)
                          .ToList(),
                TotalComments = noOfComments,
                TotalDownvotes= totalDownvotes,
                TotalUpvotes= totalUpvotes,
                TotalPosts= blogDetails.Count,
            };
        }
    }

}
