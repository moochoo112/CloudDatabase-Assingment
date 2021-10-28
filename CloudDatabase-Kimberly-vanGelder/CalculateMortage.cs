using System;
using System.Collections.Generic;
using Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Service.Interfaces;

namespace CloudDatabase_Kimberly_vanGelder
{
    public class CalculateMortage
    {
        IUserService _userService { get; }

        public CalculateMortage(IUserService userService)
        {
            _userService = userService;
        }
        
        [Function("CalculateMortage")]
        public async void Run([TimerTrigger("0 0 0 * * *")] MyInfo myTimer, FunctionContext context)
        {
            IEnumerable<User> users = _userService.GetAllUsers();
            foreach (User user in users)
            {
                var mortageOffer = Calculate(user.YearlyIncome);
                _userService.EditUser(user.UserID, mortageOffer);
            }
            _userService.SaveChanges();
            var logger = context.GetLogger("CalculateMortage");
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
        
        public double Calculate(double income)
        {
            /*
            hypotheekrent = 5 %
            woonquote = 29 %
            woonlast = inkomen x woonquote
            maximale hypotheek = woonlast / 0.064419 (annuiteit bij 5% renten en een looptijd van 30)
            */
            var housingload = income * 0.29;
            var maximunMortagage = housingload / 0.064419;
            return maximunMortagage;
        }
    }
}
