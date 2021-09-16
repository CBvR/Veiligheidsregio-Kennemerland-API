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

namespace Teamstore.Controllers {
	public class TeamHttpTrigger {
		ILogger Logger { get; }
		ITeamsService TeamsService { get; }

		public TeamHttpTrigger(ITeamsService TeamsService, ILogger<TeamHttpTrigger> Logger) {
			this.Logger = Logger;
			this.TeamsService = TeamsService;
		}

		[Function(nameof(TeamHttpTrigger.AddTeam))]
		[OpenApiOperation(operationId: "addTeam", tags: new[] { "team" }, Summary = "Add a new team to the store", Description = "This add a new team to the store.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("teamstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Team), Example = typeof(DummyTeamExample), Required = true, Description = "Team object that needs to be added to the store")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Team), Summary = "New team details added", Description = "New team details added", Example = typeof(DummyTeamExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Invalid input", Description = "Invalid input")]
		public async Task<HttpResponseData> AddTeam([HttpTrigger(AuthorizationLevel.Function, "POST", Route = "team")] HttpRequestData req, FunctionContext executionContext) {
			// Parse input
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			Team team = JsonConvert.DeserializeObject<Team>(requestBody);

			team = await TeamsService.CreateTeam(team);


			//team.Id += 100;

			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			await response.WriteAsJsonAsync(team);

			return response;
		}

		[Function(nameof(TeamHttpTrigger.UpdateTeam))]
		[OpenApiOperation(operationId: "updateTeam", tags: new[] { "team" }, Summary = "Update an existing team", Description = "This updates an existing team.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("teamstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Team), Required = true, Description = "Team object that needs to be updated to the store")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Team), Summary = "Team details updated", Description = "Team details updated", Example = typeof(DummyTeamExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Team not found", Description = "Team not found")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Validation exception", Description = "Validation exception")]
		public async Task<HttpResponseData> UpdateTeam([HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "team")] HttpRequestData req, FunctionContext executionContext) {
			// Parse input
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			Team team = JsonConvert.DeserializeObject<Team>(requestBody);

			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			await response.WriteAsJsonAsync(team);

			return response;
		}

		[Function(nameof(TeamHttpTrigger.FindTeamByTags))]
		[OpenApiOperation(operationId: "findTeamsByTags", tags: new[] { "team" }, Summary = "Finds Teams by tags", Description = "Muliple tags can be provided with comma separated strings.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("teamstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiParameter(name: "tags", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "Tags to filter by", Description = "Tags to filter by", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Team>), Summary = "successful operation", Description = "successful operation", Example = typeof(DummyTeamExamples))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid tag value", Description = "Invalid tag value")]
		public async Task<HttpResponseData> FindTeamByTags([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "team/findByTags")] HttpRequestData req, FunctionContext executionContext) {
			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			List<Team> teams = new List<Team>() { };

			await response.WriteAsJsonAsync(teams);

			return response;
		}

		[Function(nameof(TeamHttpTrigger.GetTeamById))]
		[OpenApiOperation(operationId: "getTeamById", tags: new[] { "team" }, Summary = "Find team by ID", Description = "Returns a single team.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("teamstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiParameter(name: "teamId", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "ID of team to return", Description = "ID of team to return", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Team), Summary = "successful operation", Description = "successful operation", Example = typeof(DummyTeamExamples))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Team not found", Description = "Team not found")]
		public async Task<HttpResponseData> GetTeamById([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "team/{teamId}")] HttpRequestData req, long teamId, FunctionContext executionContext) {
			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			Team team = await TeamsService.GetTeam(teamId);

			await response.WriteAsJsonAsync(team);

			return response;
		}
	}
}
