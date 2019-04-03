using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	/// </summary>
	[XmlRoot("journal")]
	public class Journal : IXmlSerializable, IEquatable<Journal>
	{
		/// <summary>
		///     Gets or sets the id.
		/// </summary>
		/// <value>
		///     The id.
		/// </value>
		[XmlAttribute("id")]
		public int Id { get; set; }

		/// <summary>
		///     Gets or sets the user.
		/// </summary>
		/// <value>
		///     The user.
		/// </value>
		[XmlElement("user")]
		public IdentifiableName User { get; set; }

		/// <summary>
		///     Gets or sets the notes.
		/// </summary>
		/// <value>
		///     The notes.
		/// </value>
		[XmlElement("notes")]
		public string Notes { get; set; }

		/// <summary>
		///     Gets or sets the created on.
		/// </summary>
		/// <value>
		///     The created on.
		/// </value>
		[XmlElement("created_on")]
		public DateTime? CreatedOn { get; set; }

		/// <summary>
		///     Gets or sets the details.
		/// </summary>
		/// <value>
		///     The details.
		/// </value>
		[XmlArray("details")]
		[XmlArrayItem("detail")]
		public IList<Detail> Details { get; set; }

		public bool Equals(Journal other)
		{
			if (other == null) return false;
			return Id == other.Id && User == other.User && Notes == other.Notes &&
				   CreatedOn == other.CreatedOn && Details == other.Details;
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			Id = reader.ReadAttributeAsInt("id");
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
					case "user":
						User = new IdentifiableName(reader);
						break;
					case "notes":
						Notes = reader.ReadElementContentAsString();
						break;
					case "created_on":
						CreatedOn = reader.ReadElementContentAsNullableDateTime();
						break;
					case "details":
						Details = reader.ReadElementContentAsCollection<Detail>();
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
