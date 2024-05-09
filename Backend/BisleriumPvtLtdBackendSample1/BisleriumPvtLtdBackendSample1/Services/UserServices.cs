using BisleriumPvtLtdBackendSample1.DbContext;
using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BisleriumPvtLtdBackendSample1.Services
{
    public class UserServices : IUserService
    {
        private readonly BisleriumBlogDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IBlogService _blogService;

        public UserServices(BisleriumBlogDbContext dbContext,
             UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor,
             IBlogService blogService
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor; 
            _blogService = blogService;
        }


        public void UpdateLastNotificationCheckedTime()
        {
            throw new NotImplementedException();
        }


        public async Task<CompleteUserDetails> GetCompleteUserDetails()
        {

            string userId = getCurrentUser();
            var user = _dbContext.Users.First(each => each.Id == userId);

            var userRoles = await _userManager.GetRolesAsync(user);

            NotificationDetails commentNotificationsDetails = getAllCommentNotifications(userId);
            NotificationDetails blogreactionNotificationDetails = getAllBlogReactionNotifications(userId);
            NotificationDetails commentReactionNotificationDetails = getAllCommentReactionNotifications(userId);

            List<EachNotificationDetails> allNotifications = commentNotificationsDetails.Notifications
                .Concat(blogreactionNotificationDetails.Notifications)
                .Concat(commentReactionNotificationDetails.Notifications)
                .OrderByDescending(notification => notification.AddedDate)
                .ToList();

            int noOfNewNotifications = commentNotificationsDetails.NewNotificationsCount+ blogreactionNotificationDetails.NewNotificationsCount+ commentReactionNotificationDetails.NewNotificationsCount;
            return new CompleteUserDetails()
            {
                UserId = user.Id,
                UserDp = "",
                Username = user.UserName,
                Email= user.Email,
                Password= user.PasswordHash,
                IsAdmin= userRoles.Contains("Admin"),
                PhoneNumber = user.PhoneNumber,
                NewNotificationCount= noOfNewNotifications,
                allNotifications = allNotifications
            };
        }


        private NotificationDetails getAllCommentNotifications(string userId)
        {
            List<EachNotificationDetails> userCommentNotificationDetails = new List<EachNotificationDetails>();

            List<Blog> userBlogs = _blogService.GetUserBlogs(userId);  

            int newNotificationCount = 0;


            foreach (Blog blog in userBlogs)
            {
                List<Comment> blogComments = _dbContext.Comments.Where(each => each.BlogId == blog.Id).ToList();
                
                foreach (Comment comment in blogComments)
                {
                    string halfBody = $"commented on your blog";
                    EachNotificationDetails eachNotification = ChangeDtoToEachNotification(comment.AddedDate, "Comment", comment.UserId, halfBody);
                    userCommentNotificationDetails.Add(eachNotification);

                    if (eachNotification.isNew) newNotificationCount++;
                }
            }

            return new()
            {
                NewNotificationsCount = newNotificationCount,
                Notifications= userCommentNotificationDetails
            };
        }
        private NotificationDetails getAllBlogReactionNotifications(string userId)
        {
            List<EachNotificationDetails> userBlogReactionNotificationDetails = new List<EachNotificationDetails>();

            List<Blog> userBlogs = _blogService.GetUserBlogs(userId);

            int newNotificationCount = 0;

            List<ReactionType> reactionTypes = _dbContext.ReactionTypes.ToList();


            foreach (Blog blog in userBlogs)
            {
                List<BlogReaction> userBlogReactions=  _dbContext.BlogReactions.Where(each => each.BlogId == blog.Id).ToList();

                foreach (var blogReaction in userBlogReactions)
                {
                    var reactionType = reactionTypes.FirstOrDefault(each => each.Id == blogReaction.ReactionTypeId);

                    string halfBody = $"{reactionType.Title.ToLower()}d your blog";

                    EachNotificationDetails eachNotification = ChangeDtoToEachNotification(blogReaction.AddedDate, "Reaction", blogReaction.UserId, halfBody);
                    userBlogReactionNotificationDetails.Add(eachNotification);
                    if (eachNotification.isNew) newNotificationCount++;
                }
            }

            return new()
            {
                NewNotificationsCount = newNotificationCount,
                Notifications = userBlogReactionNotificationDetails
            };
        }


        private NotificationDetails getAllCommentReactionNotifications(string userId)
        {
            List<EachNotificationDetails> userBlogReactionNotificationDetails = new List<EachNotificationDetails>();

            List<Comment> userComments = _dbContext.Comments.Where(each=> each.UserId== userId).ToList();

            int newNotificationCount = 0;

            List<ReactionType> reactionTypes = _dbContext.ReactionTypes.ToList();



            foreach (Comment comment in userComments)
            {
                List<CommentReaction> userCommentReactions = _dbContext.CommentReactions.Where(each => each.CommentId == comment.Id).ToList();

                foreach (var commentReaction in userCommentReactions)
                {
                    var reactionType = reactionTypes.FirstOrDefault(each => each.Id == commentReaction.ReactionTypeId);

                    string halfBody = $"{reactionType.Title.ToLower()}d your comment"; 

                    EachNotificationDetails eachNotification = ChangeDtoToEachNotification(commentReaction.AddedDate, "Reaction", commentReaction.UserId, halfBody);
                    userBlogReactionNotificationDetails.Add(eachNotification);
                    if (eachNotification.isNew) newNotificationCount++;
                }
            }
            return new()
            {
                NewNotificationsCount = newNotificationCount,
                Notifications = userBlogReactionNotificationDetails
            };
        }

        private EachNotificationDetails ChangeDtoToEachNotification(DateTime AddedDate, string NotificationType, string userId, string halfBody)
        {
            NotificationCheckedTiming notificationCheckedTiming = _dbContext.NotificationCheckedTimings.FirstOrDefault(each => each.UserId== userId);
            bool isNew = true;

            if (notificationCheckedTiming != null)
            {
                isNew = notificationCheckedTiming.LastCheckTime < AddedDate;
            }
            
            
            var user = _dbContext.Users.First(each => each.Id == userId);

            return new()
            {
                AddedDate = AddedDate,
                NotificationType = "Reaction",
                UserDp = "",
                Username = user.UserName,
                isNew = isNew,
                Body = $"{user.UserName} {halfBody}"
            };
        }

        private string getCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);

            return userId;
        }

    }

    class NotificationDetails
    {
        public List<EachNotificationDetails> Notifications {  get; set; }
        public int NewNotificationsCount { get; set; }
    }
}
