using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kennemerland.Models {
	//[OpenApiProperty(Description = "This represents the model entity for pet of Swagger Pet Store.")]

	[OpenApiExample(typeof(DummyTeamExample))]
	public class Team {
		[OpenApiProperty(Description = "Gets or sets the pet ID.")]
		public long Id { get; set; }

		[OpenApiProperty(Description = "Gets or sets the category.")]
		public Category Category { get; set; }
		
		[OpenApiProperty(Description = "Gets or sets the name.")]
		[JsonRequired]
		public string Name { get; set; }

		[OpenApiProperty(Description = "Gets or sets the list of tags.")]
		public List<Tag> Tags { get; set; }

		[OpenApiProperty(Description = "Gets or sets the hierarchy level of the team, the higher the level is, the more steps the team has to the top.")]
		public int HierarchyLevel{ get; set; }

		[OpenApiProperty(Description = "Gets or sets the team object of the team which is hierarchied above this team, if no team is above this one, this means this is the top team (GGD, BRANDWEER).")]
		public Team TeamAbove { get; set; }

		[OpenApiProperty(Description = "Gets or sets the user object who is responsible for the team's goals.")]
		public User TeamBoss { get; set; }
	}

	public class DummyTeamExample : OpenApiExample<Team> {
		public override IOpenApiExample<Team> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Incidentbestrijding", "This is Zeus' summary", new Team() { Id = 34, Name = "Zeus" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Bruno", "This is bruno's summary", "This is bruno's description", new Team() { Id = 35, Name = "Bruno" }, NamingStrategy));

			return this;
		}
	}

	public class DummyTeamExamples : OpenApiExample<List<Team>> {
		public override IOpenApiExample<List<Team>> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Teams", new List<Team> {
				new Team() { Id = 33, Name = "Wagner" },
				new Team() { Id = 34, Name = "Zeus" },
				new Team() { Id = 35, Name = "Bruno" },
			}));

			return this;
		}
	}
}
