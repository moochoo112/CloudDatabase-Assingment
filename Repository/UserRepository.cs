using DAL;
using Domain;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
        public User CreateUser(User user)
        {
            _userContext.Users.Add(user);
            Commit();
            return user;
        }

        public void EditUser(int userID, double mortageOffer)
        {
            User foundUser = _userContext.Users.Find(userID);
            foundUser.MortageOffer = mortageOffer;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userContext.Users;
        }

        public void Commit()
        {
            _userContext.SaveChanges();
        }
    }
}
