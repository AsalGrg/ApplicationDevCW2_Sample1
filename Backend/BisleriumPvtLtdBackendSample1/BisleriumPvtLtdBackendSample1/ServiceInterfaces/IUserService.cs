using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.Models;

namespace BisleriumPvtLtdBackendSample1.ServiceInterfaces
{
    public interface IUserService
    {
        void UpdateLastNotificationCheckedTime();

        Task<CompleteUserDetails> GetCompleteUserDetails();
    }
}
