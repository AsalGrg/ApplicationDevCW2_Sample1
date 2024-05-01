using BisleriumPvtLtdBackendSample1.DTOs.Comment;

namespace BisleriumPvtLtdBackendSample1.ServiceInterfaces
{
    public interface ICommentService
    {
        public CommentResponse AddComment(AddCommentRequestDto addComment);
        public CommentResponse EditComment(AddCommentRequestDto addCommentRequestDto);

        public List<EachCommentDetail> GetAllBlogComments(Guid blogId, Guid? accessingUserId);
    }
}
