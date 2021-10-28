using Domain;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CloudDatabase_Kimberly_vanGelder
{
    public class UserController
    {
		ILogger Logger { get; }
		IUserService _userService { get; }

		public UserController(ILogger<UserController> logger, IUserService userService)
		{
			Logger = logger;
			_userService = userService;
		}

		[Function(nameof(UserController.CreateUser))]
		[OpenApiOperation(operationId: "addUser", tags: new[] { "user" }, Summary = "Adds a user", Description = "This adds a new user.", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Required = true, Description = "User object that needs to be added to the KiCoKalender")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(User), Summary = "New user added", Description = "New user added")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid input", Description = "Invalid input")]
		public async Task<HttpResponseData> CreateUser(
			[HttpTrigger(AuthorizationLevel.Function, "POST", Route = "user")]
			HttpRequestData req,
			FunctionContext executionContext)
		{
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
			try
            {
				string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
				User user = JsonConvert.DeserializeObject<User>(requestBody);

				if (user is null)
				{
					response = req.CreateResponse(HttpStatusCode.BadRequest);
				}
				else
				{
					_userService.CreateUser(user);
					await response.WriteAsJsonAsync(user);
				}
			}
			catch(Exception ex)
            {
				Logger.LogError(ex.Message);
				response = req.CreateResponse(HttpStatusCode.BadRequest);
			}
			return response;
		}
	}
}
