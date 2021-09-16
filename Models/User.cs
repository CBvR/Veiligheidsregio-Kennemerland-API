using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kennemerland.Models {
	//[OpenApiProperty(Description = "This represents the model entity for pet of Swagger Pet Store.")]

	[OpenApiExample(typeof(DummyUserExample))]
	public class User {
		[OpenApiProperty(Description = "Gets or sets the pet ID.")]
		public long? Id { get; set; }

		[OpenApiProperty(Description = "Gets or sets the category.")]
		public Category Category { get; set; }

		[OpenApiProperty(Description = "Gets or sets the name.")]
		[JsonRequired]
		public string Name { get; set; }

		[OpenApiProperty(Description = "Gets or sets the list of photo URLs.")]
		[JsonRequired]
		public List<string> PhotoUrls { get; set; } = new List<string>();

		[OpenApiProperty(Description = "Gets or sets the list of tags.")]
		public List<Tag> Tags { get; set; }
	}

	public class DummyUserExample : OpenApiExample<User> {
		public override IOpenApiExample<User> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Wagner", new User() { Id = 33, Name = "Wagner" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Zeus", "This is Zeus' summary", new User() { Id = 34, Name = "Zeus" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Bruno", "This is bruno's summary", "This is bruno's description", new User() { Id = 35, Name = "Bruno" }, NamingStrategy));

			return this;
		}
	}

	public class DummyUserExample2 : OpenApiExample<User> {
		public override IOpenApiExample<User> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Wagner2", new User() { Id = 33, Name = "Wagner2" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Zeus2", "This is Zeus' summary2", new User() { Id = 34, Name = "Zeus" }, NamingStrategy));
			Examples.Add(OpenApiExampleResolver.Resolve("Bruno2", "This is bruno's summary2", "This is bruno's description", new User() { Id = 35, Name = "Bruno" }, NamingStrategy));

			return this;
		}
	}

	public class DummyUserExamples : OpenApiExample<List<User>> {
		public override IOpenApiExample<List<User>> Build(NamingStrategy NamingStrategy = null) {
			Examples.Add(OpenApiExampleResolver.Resolve("Users", new List<User> {
				new User() { Id = 33, Name = "Wagner" },
				new User() { Id = 34, Name = "Zeus" },
				new User() { Id = 35, Name = "Bruno" },
			}));

			return this;
		}
	}
}
