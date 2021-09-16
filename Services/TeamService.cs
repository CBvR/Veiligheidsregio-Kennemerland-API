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

namespace Kennemerland.Services
{
    public interface ITeamsService
    {
        Task<Team> CreateTeam(Team team);
        Task<Team> GetTeam(long id);
        Team GetTeamAbove(Team team);
    }

    public class TeamService : ITeamsService
    {
        List<Team> Teams { get; } = new();

        public TeamService(ILogger<TeamService> Logger)
        {
            Logger.LogError("Some error");
        }

        public Task<Team> CreateTeam(Team team)
        {
            Teams.Add(team);
            return Task.FromResult(team);
        }

        public Task<Team> GetTeam(long id)
        {
            foreach (Team team in Teams)
            {
                if (team.Id == id)
                {
                    return Task.FromResult(team);
                }
            }
            return Task.FromResult<Team>(null);
        }

        public Team GetTeamAbove(Team team)
        {
            if (team.HierarchyLevel > 0)
            {
                foreach (Team t in Teams)
                {
                    if (t.Id == team.TeamAbove.Id)
                    {
                        return t;
                    }
                }
                return team;
            }
            else
            {
                return team;
            }
        }
    }
}