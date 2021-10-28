using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        User CreateUser(User user);

        IEnumerable<User> GetAllUsers();

        void SendMail(User user);
        void EditUser(int userID, double mortageOffer);

        void SaveChanges();
    }
}
