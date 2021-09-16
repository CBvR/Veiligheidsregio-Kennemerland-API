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
	public interface IUsersService {
		Task<User> CreateUser(User user);
		Task<User> GetUser(long id);
	}

	public class UserService : IUsersService {
		List<User> Users { get; } = new();

		public UserService(ILogger<UserService> Logger) {
			Logger.LogError("Some error");
		}

		public Task<User> CreateUser(User user) {
			Users.Add(user);
			return Task.FromResult(user);
		}

		public Task<User> GetUser(long id) {
			foreach (User user in Users) {
				if (user.Id == id) {
					return Task.FromResult(user);
				}
			}
			return Task.FromResult<User>(null);
		}
	}
}
