using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.3
	/// </summary>
	[XmlRoot("issue_category")]
	public class IssueCategory : Identifiable<IssueCategory>,
		IEquatable<IssueCategory>,
		IXmlSerializable
	{
		/// <summary>
		///     Gets or sets the project.
		/// </summary>
		/// <value>
		///     The project.
		/// </value>
		[XmlElement("project ")]
		public IdentifiableName Project { get; set; }

		/// <summary>
		///     Gets or sets the asign to.
		/// </summary>
		/// <value>
		///     The asign to.
		/// </value>
		[XmlElement("assigned_to")]
		public IdentifiableName AsignTo { get; set; }

		/// <summary>
		///     Gets or sets the name.
		/// </summary>
		/// <value>
		///     The name.
		/// </value>
		[XmlElement("name")]
		public string Name { get; set; }

		public bool Equals(IssueCategory other)
		{
			if (other == null) return false;
			return (Id == other.Id && Project == other.Project && AsignTo == other.AsignTo &&
					Name == other.Name);
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
					case "assigned_to":
						AsignTo = new IdentifiableName(reader);
						break;
					case "name":
						Name = reader.ReadElementContentAsString();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteIdIfNotNull(Project, "project_id");
			writer.WriteElementString("name", Name);
			writer.WriteIdIfNotNull(AsignTo, "assigned_to_id");
		}
	}
}
