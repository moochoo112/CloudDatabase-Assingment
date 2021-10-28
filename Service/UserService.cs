using Domain;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        public IConfiguration Configuration { get; }
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            Configuration = configuration;
        }
        public User CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public async void SendMail(User user)
        {
            var key = Configuration.GetConnectionString("sendGridKey");
            var client = new SendGridClient(key);

            var from = new EmailAddress("kimvangelder@kpnmail.nl", "BuyMyHouse Estate Agents");
            var subject = "Mortgage application";
            var to = new EmailAddress(user.Email, user.Firstname + " "+ user.Lastname);

            var plainTextContent = "Hereby we send you your mortage application, here you can see how much you maximal mortgage would be.";
            var htmlContent = "<h3>Dear "+ user.Firstname + " "+ user.Lastname+ "</h3><p>Hereby we send you your mortage application, here you can see how much you maximal mortgage would be.</p></n><p> Your yearly income: €" + user.YearlyIncome + ",-</p></n><p> Your mortgage offer: €" + user.MortageOffer+ ",-</p></n><p>Kind regards, BuyMyHouse Estate Agents</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response1 = await client.SendEmailAsync(msg);
        }

        public void EditUser(int userID, double mortageOffer)
        {
            _userRepository.EditUser(userID, mortageOffer);
        }

        public void SaveChanges()
        {
            _userRepository.Commit();
        }
    }
}
