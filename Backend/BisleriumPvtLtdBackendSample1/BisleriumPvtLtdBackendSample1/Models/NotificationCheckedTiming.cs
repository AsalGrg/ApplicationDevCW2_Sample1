using Microsoft.AspNetCore.Identity;

namespace BisleriumPvtLtdBackendSample1.Models
{
    public class NotificationCheckedTiming
    {
        public Guid Id { get; set; }
        public string UserId {  get; set; }
        public DateTime LastCheckTime { get; set; }
        public IdentityUser User { get; set; }
    }
}
