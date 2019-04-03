﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	/// </summary>
	[XmlRoot("custom_field")]
	public class IssueCustomField : IdentifiableName, IEquatable<IssueCustomField>
	{
		/// <summary>
		///     Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		[XmlArray("value")]
		[XmlArrayItem("value")]
		public IList<CustomFieldValue> Values { get; set; }

		[XmlAttribute("multiple")]
		public bool Multiple { get; set; }

		public bool Equals(IssueCustomField other)
		{
			if (other == null) return false;
			return (Id == other.Id && Name == other.Name && Multiple == other.Multiple &&
					Values == other.Values);
		}

		public override void ReadXml(XmlReader reader)
		{
			Id = reader.ReadAttributeAsInt("id");
			Name = reader.GetAttribute("name");
			Multiple = reader.ReadAttributeAsBoolean("multiple");
			reader.Read();
			if (string.IsNullOrEmpty(reader.GetAttribute("type")))
			{
				Values = new List<CustomFieldValue>
				{
					new CustomFieldValue
					{
						Info = reader.ReadElementContentAsString()
					}
				};
			}
			else
			{
				var result = reader.ReadElementContentAsCollection<CustomFieldValue>();
				Values = result;
			}
		}

		public override void WriteXml(XmlWriter writer)
		{
			if (Values == null) return;
			var itemsCount = Values.Count;
			writer.WriteAttributeString("id", Id.ToString(CultureInfo.InvariantCulture));
			if (itemsCount > 1)
			{
				writer.WriteStartElement("value");
				writer.WriteAttributeString("type", "array");
				foreach (var v in Values) writer.WriteElementString("value", v.Info);
				writer.WriteEndElement();
			}
			else
			{
				writer.WriteElementString("value", itemsCount > 0 ? Values[0].Info : null);
			}
		}
	}
}