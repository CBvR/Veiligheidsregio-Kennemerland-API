using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kennemerland.Models {
	//[OpenApiProperty(Description = "This represents the model entity for pet of Swagger Pet Store.")]

	[OpenApiExample(typeof(DummyGoalExample))]
	public class Goal {
		[OpenApiProperty(Description = "Gets or sets the pet ID.")]
		public long? Id { get; set; }

		[OpenApiProperty(Description = "Gets or sets the category.")]
		public Category Category { get; set; }

		[OpenApiProperty(Description = "Gets or sets the name.")]
		[JsonRequired]
		public string Name { get; set; }

		[OpenApiProperty(Description = "Gets or sets the list of tags.")]
		public List<Tag> Tags { get; set; }

		[OpenApiProperty(Description = "Gets or sets the start date of the goal.")]
		public DateTime StartDate { get; set; }

		[OpenApiProperty(Description = "Gets or sets the end date of the goal")]
		public DateTime EndDate { get; set; }

		[OpenApiProperty(Description = "Gets or sets the owner / creator of this goal")]
		public User Creator { get; set; }
	}

	public class DummyGoalExample : OpenApiExample<Goal> {
		public override IOpenApiExample<Goal> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Wagner", new Goal() { Id = 33, Name = "Wagner" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Zeus", "This is Zeus' summary", new Goal() { Id = 34, Name = "Zeus" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Bruno", "This is bruno's summary", "This is bruno's description", new Goal() { Id = 35, Name = "Bruno" }, NamingStrategy));

			return this;
		}
	}

	public class DummyGoalExample2 : OpenApiExample<Goal> {
		public override IOpenApiExample<Goal> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Wagner2", new Goal() { Id = 33, Name = "Wagner2" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Zeus2", "This is Zeus' summary2", new Goal() { Id = 34, Name = "Zeus" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Bruno2", "This is bruno's summary2", "This is bruno's description", new Goal() { Id = 35, Name = "Bruno" }, NamingStrategy));

			return this;
		}
	}

	public class DummyGoalExamples : OpenApiExample<List<Goal>> {
		public override IOpenApiExample<List<Goal>> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Goals", new List<Goal> {
				new Goal() { Id = 33, Name = "Wagner" },
				new Goal() { Id = 34, Name = "Zeus" },
				new Goal() { Id = 35, Name = "Bruno" },
			}));

			return this;
		}
	}
}
