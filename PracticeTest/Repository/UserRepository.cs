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
        public IEnumerable<User> GetUsers(Guid id)
        {
            return _users.Where(x => x.Id.Equals(id)).ToList();
        }

        private void PopulateUsers()
        {
            _users.Add(new User() { Id = new Guid("4743d0e4-c303-4679-8533-9f5f312397a4"), Name = "User1" });
            _users.Add(new User() { Id = new Guid("4743d0e4-c303-4679-8533-9f5f312397a5"), Name = "User2" });
            _users.Add(new User() { Id = new Guid("4743d0e4-c303-4679-8533-9f5f312397a6"), Name = "User3" });
        }
    }
}