using Microsoft.AspNetCore.Identity;

namespace BisleriumPvtLtdBackendSample1.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string CoverImage { get; set; }
        public string Title {  get; set; }
        public string Body { get; set; }
        public DateTime AddedDate { get; set; }
        public string UserId { get; set; }

        //Navigation Properties
        public IdentityUser User { get; set; }
    }
}
