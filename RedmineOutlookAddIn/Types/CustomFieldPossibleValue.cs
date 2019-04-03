using System;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	[XmlRoot("possible_value")]
	public class CustomFieldPossibleValue : IEquatable<CustomFieldPossibleValue>
	{
		[XmlElement("value")]
		public string Value { get; set; }

		public bool Equals(CustomFieldPossibleValue other)
		{
			if (other == null) return false;
			return (Value == other.Value);
		}
	}
}