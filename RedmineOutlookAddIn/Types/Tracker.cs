using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.3
	/// </summary>
	[XmlRoot("tracker")]
	public class Tracker : IXmlSerializable, IEquatable<Tracker>
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
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool Equals(Tracker other)
		{
			if (other == null) return false;
			return Id == other.Id && Name == other.Name;
		}

		public void WriteXml(XmlWriter writer) { }

		public XmlSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		///     Generates an object from its XML representation.
		/// </summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
		public virtual void ReadXml(XmlReader reader)
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
					default:
						reader.Read();
						break;
				}
			}
		}

		public override string ToString()
		{
			return Id + ", " + Name;
		}
	}
}