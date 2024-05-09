namespace BisleriumPvtLtdBackendSample1.DTOs.Comment
{
    public class AddCommentRequestDto
    {
        //for edit only commentId
        public Guid? CommentId {  get; set; }


        public Guid BlogId { get; set; }
        public string CommentContent { get; set; }

    }
}
