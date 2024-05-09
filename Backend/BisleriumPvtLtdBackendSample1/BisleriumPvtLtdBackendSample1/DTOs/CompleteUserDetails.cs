using Microsoft.EntityFrameworkCore.Storage;

namespace BisleriumPvtLtdBackendSample1.DTOs
{
    public class CompleteUserDetails
    {

        public string UserId {  get; set; }
        public string Username { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }

        public bool IsAdmin { get; set; }
        public int NewNotificationCount { get; set; }
        public string UserDp {  get; set; }
        public List<EachNotificationDetails> allNotifications { get; set; }
    }
}
