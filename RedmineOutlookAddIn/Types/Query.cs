using System;
using System.Xml;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.3
	/// </summary>
	[XmlRoot("query")]
	public class Query : IdentifiableName, IEquatable<Query>
	{
		/// <summary>
		///     Gets or sets a value indicating whether this instance is public.
		/// </summary>
		/// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
		[XmlElement("is_public")]
		public bool IsPublic { get; set; }

		/// <summary>
		///     Gets or sets the project id.
		/// </summary>
		/// <value>The project id.</value>
		[XmlElement("project_id")]
		public int? ProjectId { get; set; }

		public bool Equals(Query other)
		{
			if (other == null) return false;
			return (other.Id == Id && other.Name == Name && other.IsPublic == IsPublic &&
					other.ProjectId == ProjectId);
		}

		public override void ReadXml(XmlReader reader)
		{
			reader.Read();
			while (!reader.EOF)
			{
				if (reader.IsEmptyElement && !reader.HasAttributes)
				{
					reader.Read();
					continue;
				}
				switch (reader.Name)
				{
					case "id":
						Id = reader.ReadElementContentAsInt();
						break;
					case "name":
						Name = reader.ReadElementContentAsString();
						break;
					case "is_public":
						IsPublic = reader.ReadElementContentAsBoolean();
						break;
					case "project_id":
						ProjectId = reader.ReadElementContentAsNullableInt();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public override void WriteXml(XmlWriter writer) { }
	}
}
