using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;

namespace BisleriumPvtLtdBackendSample1.Services
{
    public class UserServices : IUserService
    {
        private readonly BisleriumBlogDbContext _dbContext;

        public UserServices(BisleriumBlogDbContext dbContext) { 
            _dbContext = dbContext;
        }   


        public LoginUserResponse LoginUser(LoginUserDto loginUserDto)
        {
            var user = _dbContext.Users.FirstOrDefault(each =>
                        (each.Email == loginUserDto.UsernameOrEmail || each.Name == loginUserDto.UsernameOrEmail)
                        && each.Password == loginUserDto.Password);

            if (user == null)
            {
                return null;

            }
            return new()
            {
                Id = user.Id,
                IsAdmin = _dbContext.Roles.FirstOrDefault(each=> each.Id== user.RolesId).Title.Equals("Admin"),
                UserDp = "",
                Username = user.Name
            };
        }

        public User RegisterUser(RegisterUserDto registerUser)
        {
            EntityEntry<User> addedUserEntry = _dbContext.Users.Add(new User()
            {
                Name = registerUser.Username,
                Email = registerUser.Email,
                Password = registerUser.Password,
                Roles = _dbContext.Roles.FirstOrDefault(each => each.Title.Equals("Normal")),
            });

            User addedUser = addedUserEntry.Entity;

            _dbContext.SaveChanges();

            return addedUser;
        }
    }
}
