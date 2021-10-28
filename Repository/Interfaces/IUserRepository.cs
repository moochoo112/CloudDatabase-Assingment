using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        User CreateUser(User user);

        IEnumerable<User> GetAllUsers();
        void EditUser(int userID, double mortageOffer);

        void Commit();
    }
}
