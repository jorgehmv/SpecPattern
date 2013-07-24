SpecPattern
======================================================================

Simple yet powerful implementation of specification pattern in C#. 

Relying in IQueryable it covers the three requirements this patterns aims to solve

- Validation
- Querying
- Building

Example

	// Check if project is either cool or popular but it should be trustable
	var atLeastTrustableSpec = (coolProjectSpec | popularProjectSpec)
									   & trustableProjectSpec ;
	atLeastTrustableSpec.IsSatisfiedBy(project);

	// Use the same specs but now to query database with an ORM
	var allGreatBuUnpopularProjects = Repository.Query<Project>()
									  .Where(greatButUnpopularSpec.Predicate);

Spec Pattern project consists of only five classes and in most of the cases you will only have to know about one.

**Creating a new Specification**

Simply inherit from Specification<T> and override DefinePredicate method which should return the predicate that defines your specification:

	public class Project
	{
		public string Name{ get; set; }

		public int Features { get; set; }

		public int Followers { get; set; }

		public bool Stable { get; set; }
	}

	public class CoolProjectSpec : Specification<Project>
	{
		protected override Expression<Func<Project, bool>> DefinePredicate()
		{
			return p => p.Features >= 5;
		}
	}


Of course you can work with instance fields inside your predicate if needed (usually you will):

	public class CoolProjectSpec : Specification<Project>
	{
		private readonly int minFeaturesToBeCool;
		public CoolProjectSpec(int minFeaturesToBeCool)
		{
			this.minFeaturesToBeCool = minFeaturesToBeCool;
		}

		protected override Expression<Func<Project, bool>> DefinePredicate()
		{
			return p => p.Features >= minFeaturesToBeCool;
		}
	}

**Combining specifications**

The main advantage of using specifications is the ability to combine them as if they were predicates, relying on C# operator overloading we can do this with a clean syntax:

	Specification<Project> coolProjectSpec = new CoolProjectSpec(5);
	Specification<Project> popularProjectSpec = new PopularProjectSpec();
	Specification<Project> trustableProjectSpec = new TrustableProjectSpec();

	var project = new Project { Features = 5, Followers = 250, Stable = true };
	
	//Check if project rules
	var projectRulesSpec = coolProjectSpec & popularProjectSpec 
							& trustableProjectSpec;
	projectRulesSpec.IsSatisfiedBy(project);

	// Check if project is cool and trustable but unpopular (i know that story)
	var greatButUnpopularSpec = coolProjectSpec & trustableProjectSpec 
								& !popularProjectSpec;
	greatButUnpopularSpec.IsSatisfiedBy(project);

	// Check if project is either cool or popular but it should be trustable
	var atLeastTrustableSpec = (coolProjectSpec | popularProjectSpec)
								& trustableProjectSpec ;
	atLeastTrustableSpec.IsSatisfiedBy(project);

**ORM integration**

The best part is you dont have to do anything to integrate it with your ORM; if it supports IQueryable you are done:

	// Use the same specs but now to query database with an ORM
	var allGreatButUnpopularProjects = Repository.Query<Project>()
									.Where(greatButUnpopularSpec.Predicate);
									
Nuget Package
===========================================
PM> Install-Package SpecPattern
