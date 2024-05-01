using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.Models;

namespace BisleriumPvtLtdBackendSample1.ServiceInterfaces
{
    public interface IUserService
    {
        //for testing
        LoginUserResponse LoginUser(LoginUserDto loginUserDto);

        User RegisterUser(RegisterUserDto registerUser);
    }
}
