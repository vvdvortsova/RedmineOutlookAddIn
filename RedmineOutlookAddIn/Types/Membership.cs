using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Only the roles can be updated, the project and the user of a membership are read-only.
	/// </summary>
	[XmlRoot("membership")]
	public class Membership : Identifiable<Membership>,
		IEquatable<Membership>,
		IXmlSerializable
	{
		/// <summary>
		///     Gets or sets the project.
		/// </summary>
		/// <value>The project.</value>
		[XmlElement("project")]
		public IdentifiableName Project { get; set; }

		/// <summary>
		///     Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		[XmlArray("roles")]
		[XmlArrayItem("role")]
		public List<MembershipRole> Roles { get; set; }

		public bool Equals(Membership other)
		{
			if (other == null) return false;
			return (Id == other.Id && Project == other.Project && Roles == other.Roles);
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
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
					case "project":
						Project = new IdentifiableName(reader);
						break;
					case "roles":
						Roles = reader.ReadElementContentAsCollection<MembershipRole>();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public void WriteXml(XmlWriter writer) { }
	}
}