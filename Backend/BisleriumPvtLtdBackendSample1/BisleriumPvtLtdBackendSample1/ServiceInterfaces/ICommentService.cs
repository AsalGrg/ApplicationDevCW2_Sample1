using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Comment;

namespace BisleriumPvtLtdBackendSample1.ServiceInterfaces
{
    public interface ICommentService
    {
        public EachCommentDetail AddComment(AddCommentRequestDto addComment);
        public EachCommentDetail EditComment(AddCommentRequestDto addCommentRequestDto);
        public List<EachCommentDetail> GetAllBlogComments(Guid blogId, string? accessingUserId);
        public UserCommentReactionDetail AddCommentReaction(AddCommentReactionDto addCommentReaction);
        public EachNotificationDetails DeleteCommentReaction(Guid reactionId);
        public UserCommentReactionDetail UpdateCommentReaction(Guid reactionId, AddCommentReactionDto commentReactionDto);
    }
}
