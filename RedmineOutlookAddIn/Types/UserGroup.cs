using System;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	[XmlRoot("group")]
	public class UserGroup : IdentifiableName, IEquatable<UserGroup>
	{
		public bool Equals(UserGroup other)
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
