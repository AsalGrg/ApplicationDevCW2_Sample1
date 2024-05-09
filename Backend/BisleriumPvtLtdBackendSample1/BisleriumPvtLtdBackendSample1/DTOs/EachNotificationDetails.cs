using Microsoft.Identity.Client;

namespace BisleriumPvtLtdBackendSample1.DTOs
{
    public class EachNotificationDetails
    {
        public string Username {  get; set; }
        public string UserDp {  get; set; }
        public string NotificationType {  get; set; }//can be commnet or react
        public DateTime AddedDate {  get; set; }
        public bool isNew {  get; set; }= false;
        public string? Body { get; set; }
    }
}
