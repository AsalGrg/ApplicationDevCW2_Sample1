using BisleriumPvtLtdBackendSample1.DbContext;
using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.DTOs.Comment;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;

namespace BisleriumPvtLtdBackendSample1.Services
{
    public class CommentService : ICommentService
    {
        private readonly BisleriumBlogDbContext _dbContext;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(BisleriumBlogDbContext dbContext,
            UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public EachCommentDetail AddComment(AddCommentRequestDto addComment)
        {

            string userId = getCurrentUser();

            Comment savedComment = _dbContext.Add(new Comment()
            {
                Blog = _dbContext.Blogs.First(e => e.Id == addComment.BlogId),
                User = _dbContext.Users.First(e => e.Id == userId),
                Body = addComment.CommentContent
            }).Entity;

            _dbContext.SaveChanges();
            return ChangeToCommentDetails(savedComment, userId);
        }

        public EachCommentDetail EditComment(AddCommentRequestDto addCommentRequestDto)
        {
            string userId = getCurrentUser();
            Comment existingBlog = _dbContext.Comments.First(e => e.Id == addCommentRequestDto.CommentId);
            existingBlog.Body = addCommentRequestDto.CommentContent;

            Comment updatedCommnet = _dbContext.Update(existingBlog).Entity;
            _dbContext.SaveChanges();

            return ChangeToCommentDetails(updatedCommnet, userId);
        }


        private CommentResponse ChangeToCommentResponse(Comment comment)
        {
            IdentityUser user = _dbContext.Users.First(u => u.Id == comment.UserId);

            return new CommentResponse()
            {
                CommentContent = comment.Body,
                UserDp = "",
                Username = user.UserName,
                AddedDate = comment.AddedDate
            };
        }



        public List<EachCommentDetail> GetAllBlogComments(Guid blogId, string? accessingUserId)
        {

            List<EachCommentDetail> commentDetails = new List<EachCommentDetail>();

            List<Comment> blogComments = _dbContext.Comments.Where(each => each.BlogId == blogId).ToList();

            foreach (Comment comment in blogComments)
            {
                    
                    commentDetails.Add(ChangeToCommentDetails(comment, accessingUserId));
                }

            return commentDetails;
        }

        private EachCommentDetail ChangeToCommentDetails(Comment comment, string? accessingUserId)
        {
            //if accessing user id matches the comment author Id

            bool isAuthor = false;
            bool hasReacted = false;
            UserCommentReactionDetail userCommentReactionDetail = null;

            if (accessingUserId != null)
            {
                isAuthor = comment.UserId.Equals(accessingUserId);

                CommentReaction commentReaction = _dbContext.CommentReactions.FirstOrDefault(each => each.CommentId == comment.Id && each.UserId == accessingUserId);

                if (commentReaction != null)
                {
                    hasReacted = true;
                    userCommentReactionDetail = new()
                    {
                        ReactionId = commentReaction.Id,
                        ReactionName = _dbContext.ReactionTypes.FirstOrDefault(each => each.Id == commentReaction.ReactionTypeId).Title
                    };
                }
            }

            IdentityUser user = _dbContext.Users.FirstOrDefault(each => each.Id == comment.UserId);


            EachCommentDetail eachCommentDetail = new EachCommentDetail()
            {
                AuthorDetails = new()
                {
                    UserId = comment.UserId,
                    UserDp = "",
                    Username = user.UserName
                },
                CommentId = comment.Id,
                CommentContent = comment.Body,
                AddedDate = comment.AddedDate,
                NoOfUpVotes = CalculateNoOfReactions(comment.Id, "Upvote"),
                NoOfDownVotes = CalculateNoOfReactions(comment.Id, "Downvote"),
                IsAuthor = isAuthor,
                HasReacted = hasReacted,
                ReactionDetail = userCommentReactionDetail
            };

            return eachCommentDetail;

        }

        private int CalculateNoOfReactions(Guid commentId, string reactionTypeTitle)
        {
            ReactionType reactionType = _dbContext.ReactionTypes.FirstOrDefault(each => each.Title == reactionTypeTitle);

            List<CommentReaction> commentReactions = _dbContext.CommentReactions.Where(each => each.CommentId == commentId && each.ReactionTypeId == reactionType.Id).ToList();

            return commentReactions.Count;
        }

        public UserCommentReactionDetail AddCommentReaction(AddCommentReactionDto addCommentReaction)
        {
            string userId = getCurrentUser();

            ReactionType reactionType = _dbContext.ReactionTypes.First(each => each.Title == addCommentReaction.ReactionType);
            Comment comment = _dbContext.Comments.First(each => each.Id == addCommentReaction.CommentId);


            CommentReaction savedReaction = _dbContext.Add(
                new CommentReaction()
                {
                    UserId = userId,
                    ReactionType = reactionType,
                    Comment = comment,
                }
                ).Entity;

            _dbContext.SaveChanges();


            return new()
            {
                ReactionId = savedReaction.Id,
                ReactionName = addCommentReaction.ReactionType
            };
        }

        public EachNotificationDetails DeleteCommentReaction(Guid reactionId)
        {
            CommentReaction commentReaction = _dbContext.CommentReactions.FirstOrDefault(each => each.Id == reactionId);
            CommentReaction removedCommentReaction = _dbContext.Remove(commentReaction).Entity;
            _dbContext.SaveChanges();
            return ChangeToNotificationDetails(removedCommentReaction);
        }

        public UserCommentReactionDetail UpdateCommentReaction(Guid reactionId, AddCommentReactionDto commentReactionDto)
        {
            CommentReaction commentReaction= _dbContext.CommentReactions.FirstOrDefault(each => each.Id == reactionId);

            commentReaction.ReactionType = _dbContext.ReactionTypes.FirstOrDefault(each => each.Title == commentReactionDto.ReactionType);
            CommentReaction updatedCommentReaction = _dbContext.Update(commentReaction).Entity;
            _dbContext.SaveChanges();
            return new()
            {
                ReactionId = updatedCommentReaction.Id,
                ReactionName = commentReactionDto.ReactionType
            };
        }

        private EachNotificationDetails ChangeToNotificationDetails(CommentReaction commentReaction)
        {
            return new()
            {
                AddedDate = commentReaction.AddedDate,
                NotificationType = "Reaction",
                UserDp = "",
                Username = _dbContext.Users.First(each => each.Id == commentReaction.UserId).UserName
            };
        }
        private string getCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);

            return userId;
        }

        public string DeleteComment(Guid commentId)
        {

            string UserId = getCurrentUser();
            Comment comment = _dbContext.Comments.First(each => each.Id == commentId);

            if (!comment.UserId.Equals(UserId)) return null;
            var commentReactions = _dbContext.CommentReactions.Where(each => each.CommentId == comment.Id).ToList();
            _dbContext.CommentReactions.RemoveRange(commentReactions);

            _dbContext.Remove(comment);
            _dbContext.SaveChanges();

            return "Comment deleted successfully";
        }
    }
}
