using BisleriumPvtLtdBackendSample1.DTOs.Comment;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;

namespace BisleriumPvtLtdBackendSample1.Services
{
    public class CommentService : ICommentService
    {
        private readonly BisleriumBlogDbContext _dbContext;

        public CommentService(BisleriumBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CommentResponse AddComment(AddCommentRequestDto addComment)
        {
            if (addComment.UserId == null) return null;


            Comment savedComment = _dbContext.Add(new Comment()
            {
                Blog = _dbContext.Blogs.First(e => e.Id == addComment.BlogId),
                User = _dbContext.Users.First(e => e.Id == addComment.UserId),
                Body = addComment.CommentContent
            }).Entity;

            _dbContext.SaveChanges();
            return ChangeToCommentResponse(savedComment);
        }

        public CommentResponse EditComment(AddCommentRequestDto addCommentRequestDto)
        {
            Comment existingBlog = _dbContext.Comments.First(e => e.Id == addCommentRequestDto.CommentId);
            existingBlog.Body = addCommentRequestDto.CommentContent;

            Comment updatedCommnet = _dbContext.Update(existingBlog).Entity;
            _dbContext.SaveChanges();

            return ChangeToCommentResponse(updatedCommnet);
        }


        private CommentResponse ChangeToCommentResponse(Comment comment)
        {
            User user = _dbContext.Users.First(u => u.Id == comment.UserId);

            return new CommentResponse()
            {
                CommentContent = comment.Body,
                UserDp = "",
                Username = user.Name,
                AddedDate = comment.AddedDate
            };
        }



        public List<EachCommentDetail> GetAllBlogComments(Guid blogId, Guid? accessingUserId)
        {

            List<EachCommentDetail> commentDetails = new List<EachCommentDetail>();

            List<Comment> blogComments = _dbContext.Comments.Where(each => each.BlogId == blogId).ToList();

            foreach (Comment comment in blogComments)
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

                User user = _dbContext.Users.FirstOrDefault(each => each.Id == comment.UserId);


                EachCommentDetail eachCommentDetail = new EachCommentDetail()
                {
                        AuthorDetails = new ()
                {
                        UserId= comment.UserId,
                        UserDp= "",
                        Username= user.Name
                    },
                        CommentId= comment.Id,
                        CommentContent = comment.Body,
                        AddedDate = comment.AddedDate,
                        NoOfUpVotes = CalculateNoOfReactions(comment.Id, "Upvote"),
                        NoOfDownVotes = CalculateNoOfReactions(comment.Id, "Downvote"),
                        IsAuthor = isAuthor,
                        HasReacted = hasReacted,
                        ReactionDetail = userCommentReactionDetail
                 };

                    commentDetails.Add(eachCommentDetail);
                }

            return commentDetails;
        }

        private int CalculateNoOfReactions(Guid commentId, string reactionTypeTitle)
        {
            ReactionType reactionType = _dbContext.ReactionTypes.FirstOrDefault(each => each.Title == reactionTypeTitle);

            List<CommentReaction> commentReactions = _dbContext.CommentReactions.Where(each => each.CommentId == commentId && each.ReactionTypeId == reactionType.Id).ToList();

            return commentReactions.Count;
        }

    }
}
