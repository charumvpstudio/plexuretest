using System;
using System.Collections.Generic;
using System.Linq;
using PracticeTest.Models;

namespace PracticeTest.Repository
{
    public class UserRepository : IUserRepository
    {
        List<User> _users = new List<User>();

        public UserRepository()
        {
            PopulateUsers();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Return users</returns>
        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Return a user by Id</returns>
        public IEnumerable<User> GetUsers(int id)
        {
            return _users.Where(x => x.Id.Equals(id)).ToList();
        }

        private void PopulateUsers()
        {
            _users.Add(new User() { Id = 101, Name = "User1", CouponId = 2 });
            _users.Add(new User() { Id = 102, Name = "User2", CouponId = 2 });
            _users.Add(new User() { Id = 103, Name = "User3", CouponId = 1 });
        }
    }
}