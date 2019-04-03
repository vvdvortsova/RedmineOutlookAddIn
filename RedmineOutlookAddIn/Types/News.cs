using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.1
	/// </summary>
	[XmlRoot("news")]
	public class News : Identifiable<News>, IEquatable<News>, IXmlSerializable
	{
		/// <summary>
		///     Gets or sets the project.
		/// </summary>
		/// <value>The project.</value>
		[XmlElement("project")]
		public IdentifiableName Project { get; set; }

		/// <summary>
		///     Gets or sets the author.
		/// </summary>
		/// <value>The author.</value>
		[XmlElement("author")]
		public IdentifiableName Author { get; set; }

		/// <summary>
		///     Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[XmlElement("title")]
		public string Title { get; set; }

		/// <summary>
		///     Gets or sets the summary.
		/// </summary>
		/// <value>The summary.</value>
		[XmlElement("summary")]
		public string Summary { get; set; }

		/// <summary>
		///     Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		[XmlElement("description")]
		public string Description { get; set; }

		/// <summary>
		///     Gets or sets the created on.
		/// </summary>
		/// <value>The created on.</value>
		[XmlElement("created_on")]
		public DateTime? CreatedOn { get; set; }

		public bool Equals(News other)
		{
			if (other == null) return false;
			return (Id == other.Id && Project == other.Project && Author == other.Author &&
					Title == other.Title && Summary == other.Summary &&
					Description == other.Description && CreatedOn == other.CreatedOn);
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
					case "author":
						Author = new IdentifiableName(reader);
						break;
					case "title":
						Title = reader.ReadElementContentAsString();
						break;
					case "summary":
						Summary = reader.ReadElementContentAsString();
						break;
					case "description":
						Description = reader.ReadElementContentAsString();
						break;
					case "created_on":
						CreatedOn = reader.ReadElementContentAsNullableDateTime();
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