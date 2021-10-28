using System;
using System.Collections.Generic;
using Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Service.Interfaces;

namespace CloudDatabase_Kimberly_vanGelder
{
    public class SendMail
    {
        IUserService _userService { get; }

        public SendMail(IUserService userService)
        {
            _userService = userService;
        }

        [Function("SendMail")]
        public void Run([TimerTrigger("0 0 7 * * *")] MyInfo myTimer, FunctionContext context)
        {
           IEnumerable<User> users = _userService.GetAllUsers();
            foreach (User user in users)
            {
                _userService.SendMail(user);
            }
            var logger = context.GetLogger("SendMail");
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

}
