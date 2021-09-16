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
using Task = System.Threading.Tasks.Task;

namespace Kennemerland.Services {
	public interface IGoalsService {
		Task<Goal> CreateGoal(Goal goal);
		Task<Goal> GetGoal(long id);
	}

	public class GoalService : IGoalsService {
		List<Goal> Goals { get; } = new();

		public GoalService(ILogger<GoalService> Logger) {
			Logger.LogError("Some error");
		}

		public Task<Goal> CreateGoal(Goal goal) {
			Goals.Add(goal);
			return Task.FromResult(goal);
		}

		public Task<Goal> GetGoal(long id) {
			foreach (Goal goal in Goals) {
				if (goal.Id == id) {
					return Task.FromResult(goal);
				}
			}
			return Task.FromResult<Goal>(null);
		}
	}
}
