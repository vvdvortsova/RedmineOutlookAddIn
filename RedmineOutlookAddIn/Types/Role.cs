using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.4
	/// </summary>
	[XmlRoot("role")]
	public class Role : IXmlSerializable, IEquatable<Role>
	{
		/// <summary>
		///     Gets or sets the id.
		/// </summary>
		/// <value>
		///     The id.
		/// </value>
		[XmlElement("id")]
		public int Id { get; set; }

		/// <summary>
		///     Gets or sets the name.
		/// </summary>
		/// <value>
		///     The name.
		/// </value>
		[XmlElement("name")]
		public string Name { get; set; }

		/// <summary>
		///     Gets or sets the permissions.
		/// </summary>
		/// <value>
		///     The issue relations.
		/// </value>
		[XmlArray("permissions")]
		[XmlArrayItem("permission")]
		public IList<Permission> Permissions { get; set; }

		public bool Equals(Role other)
		{
			if (other == null) return false;
			return Id == other.Id && Name == other.Name;
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
					case "name":
						Name = reader.ReadElementContentAsString();
						break;
					case "permissions":
						Permissions = reader.ReadElementContentAsCollection<Permission>();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public void WriteXml(XmlWriter writer) { }

		public override string ToString()
		{
			return Id + ", " + Name;
		}
	}
}