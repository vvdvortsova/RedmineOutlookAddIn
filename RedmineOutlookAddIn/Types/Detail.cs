using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	/// </summary>
	[XmlRoot("detail")]
	public class Detail : IXmlSerializable, IEquatable<Detail>
	{
		/// <summary>
		///     Получить  property.
		/// </summary>
		/// <value>
		///     Property.
		/// </value>
		[XmlAttribute("property")]
		public string Property { get; set; }

		/// <summary>
		///     Получить status id.
		/// </summary>
		/// <value>
		///     Status id.
		/// </value>
		[XmlAttribute("name")]
		public string StatusId { get; set; }

		/// <summary>
		///     Получить old value.
		/// </summary>
		/// <value>
		///      Old value.
		/// </value>
		[XmlElement("old_value")]
		public string OldValue { get; set; }

		/// <summary>
		///     Получить новое значение.
		/// </summary>
		/// <value>
		///     New value.
		/// </value>
		[XmlElement("new_value")]
		public string NewValue { get; set; }

		public bool Equals(Detail other)
		{
			if (other == null) return false;
			return Property == other.Property && StatusId == other.StatusId &&
				   OldValue == other.OldValue && NewValue == other.NewValue;
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			Property = reader.GetAttribute("property");
			StatusId = reader.GetAttribute("name");
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
					case "old_value":
						OldValue = reader.ReadElementContentAsString();
						break;
					case "new_value":
						NewValue = reader.ReadElementContentAsString();
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
