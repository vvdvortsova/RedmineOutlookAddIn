﻿using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 2.2
	/// </summary>
	[XmlRoot("time_entry_activity")]
	public class TimeEntryActivity : IXmlSerializable, IEquatable<TimeEntryActivity>
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

		[XmlElement("is_default")]
		public bool IsDefault { get; set; }

		#region Implementation of IEquatable<TimeEntryActivity>

		public bool Equals(TimeEntryActivity other)
		{
			if (other == null) return false;
			return Id == other.Id && Name == other.Name && IsDefault == other.IsDefault;
		}

		#endregion

		public override string ToString()
		{
			return Id + ", " + Name;
		}

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		///     Generates an object from its XML representation.
		/// </summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
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
					case "is_default":
						IsDefault = reader.ReadElementContentAsBoolean();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public void WriteXml(XmlWriter writer) { }

		#endregion
	}
}