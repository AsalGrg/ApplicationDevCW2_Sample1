using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Primitives;

namespace BisleriumPvtLtdBackendSample1.DTOs.Comment
{
    public class EachCommentDetail
    {
        public Guid CommentId { get; set; }
        public UserDetailsSnippet AuthorDetails { get; set; }
        public string CommentContent { get; set; }
        public DateTime AddedDate { get; set; }
        public int NoOfUpVotes { get; set; }
        public int NoOfDownVotes { get; set; }
        public bool IsAuthor {  get; set; }=false;
        public bool HasReacted {  get; set; }=false;
        public UserCommentReactionDetail? ReactionDetail { get; set; } = null;
    }
}
