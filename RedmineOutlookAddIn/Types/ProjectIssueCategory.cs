using System;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	/// </summary>
	[XmlRoot("issue_category")]
	public class ProjectIssueCategory : IdentifiableName, IEquatable<ProjectTracker>
	{
		public bool Equals(ProjectTracker other)
		{
			if (other == null) return false;
			return Id == other.Id && Name == other.Name;
		}

		public override string ToString()
		{
			return Id + ", " + Name;
		}
	}
}
