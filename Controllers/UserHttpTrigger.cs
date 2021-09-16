using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Kennemerland.Models;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using Kennemerland.Services;

namespace Userstore.Controllers {
	public class UserHttpTrigger {
		ILogger Logger { get; }
		IUsersService UsersService { get; }

		public UserHttpTrigger(IUsersService UsersService, ILogger<UserHttpTrigger> Logger) {
			this.Logger = Logger;
			this.UsersService = UsersService;
		}

		[Function(nameof(UserHttpTrigger.AddUser))]
		[OpenApiOperation(operationId: "addUser", tags: new[] { "user" }, Summary = "Add a new user to the store", Description = "This add a new user to the store.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("userstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Example = typeof(DummyUserExample2), Required = true, Description = "User object that needs to be added to the store")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "New user details added", Description = "New user details added", Example = typeof(DummyUserExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Invalid input", Description = "Invalid input")]
		public async Task<HttpResponseData> AddUser([HttpTrigger(AuthorizationLevel.Function, "POST", Route = "user")] HttpRequestData req, FunctionContext executionContext) {
			// Parse input
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			User user = JsonConvert.DeserializeObject<User>(requestBody);

			user = await UsersService.CreateUser(user);


			//user.Id += 100;

			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			await response.WriteAsJsonAsync(user);

			return response;
		}

		[Function(nameof(UserHttpTrigger.UpdateUser))]
		[OpenApiOperation(operationId: "updateUser", tags: new[] { "user" }, Summary = "Update an existing user", Description = "This updates an existing user.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("userstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Required = true, Description = "User object that needs to be updated to the store")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "User details updated", Description = "User details updated", Example = typeof(DummyUserExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "User not found", Description = "User not found")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Validation exception", Description = "Validation exception")]
		public async Task<HttpResponseData> UpdateUser([HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "user")] HttpRequestData req, FunctionContext executionContext) {
			// Parse input
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			User user = JsonConvert.DeserializeObject<User>(requestBody);

			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			await response.WriteAsJsonAsync(user);

			return response;
		}

		[Function(nameof(UserHttpTrigger.FindUserByTags))]
		[OpenApiOperation(operationId: "findUsersByTags", tags: new[] { "user" }, Summary = "Finds Users by tags", Description = "Muliple tags can be provided with comma separated strings.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("userstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiParameter(name: "tags", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "Tags to filter by", Description = "Tags to filter by", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<User>), Summary = "successful operation", Description = "successful operation", Example = typeof(DummyUserExamples))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid tag value", Description = "Invalid tag value")]
		public async Task<HttpResponseData> FindUserByTags([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "user/findByTags")] HttpRequestData req, FunctionContext executionContext) {
			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			List<User> users = new List<User>() { };

			await response.WriteAsJsonAsync(users);

			return response;
		}

		[Function(nameof(UserHttpTrigger.GetUserById))]
		[OpenApiOperation(operationId: "getUserById", tags: new[] { "user" }, Summary = "Find user by ID", Description = "Returns a single user.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("userstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "ID of user to return", Description = "ID of user to return", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "successful operation", Description = "successful operation", Example = typeof(DummyUserExamples))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "User not found", Description = "User not found")]
		public async Task<HttpResponseData> GetUserById([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "user/{userId}")] HttpRequestData req, long userId, FunctionContext executionContext) {
			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			User user = await UsersService.GetUser(userId);

			await response.WriteAsJsonAsync(user);

			return response;
		}
	}
}
