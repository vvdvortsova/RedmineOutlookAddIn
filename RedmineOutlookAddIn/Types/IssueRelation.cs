﻿using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.3
	/// </summary>
	[XmlRoot("relation")]
	public class IssueRelation : Identifiable<IssueRelation>,
		IXmlSerializable,
		IEquatable<IssueRelation>
	{
		/// <summary>
		///     Gets or sets the issue id.
		/// </summary>
		/// <value>The issue id.</value>
		[XmlElement("issue_id")]
		public int IssueId { get; set; }

		/// <summary>
		///     Gets or sets the related issue id.
		/// </summary>
		/// <value>The issue to id.</value>
		[XmlElement("issue_to_id")]
		public int IssueToId { get; set; }

		/// <summary>
		///     Gets or sets the type of relation.
		/// </summary>
		/// <value>The type.</value>
		[XmlElement("relation_type")]
		public IssueRelationType Type { get; set; }

		/// <summary>
		///     Gets or sets the delay for a "precedes" or "follows" relation.
		/// </summary>
		/// <value>The delay.</value>
		[XmlElement("delay")]
		public int? Delay { get; set; }

		public bool Equals(IssueRelation other)
		{
			if (other == null) return false;
			return (Id == other.Id && IssueId == other.IssueId &&
					IssueToId == other.IssueToId && Type == other.Type &&
					Delay == other.Delay);
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsEmptyElement) reader.Read();
			while (!reader.EOF)
			{
				if (reader.IsEmptyElement && !reader.HasAttributes)
				{
					reader.Read();
					continue;
				}
				if (reader.IsEmptyElement && reader.HasAttributes)
				{
					while (reader.MoveToNextAttribute())
					{
						var attributeName = reader.Name;
						switch (reader.Name)
						{
							case "id":
								Id = reader.ReadAttributeAsInt(attributeName);
								break;
							case "issue_id":
								IssueId = reader.ReadAttributeAsInt(attributeName);
								break;
							case "issue_to_id":
								IssueToId = reader.ReadAttributeAsInt(attributeName);
								break;
							case "relation_type":
								var rt = reader.GetAttribute(attributeName);
								if (!string.IsNullOrEmpty(rt))
								{
									Type =
										(IssueRelationType)
											Enum.Parse(
												typeof(IssueRelationType),
												rt,
												true);
								}
								break;
							case "delay":
								Delay = reader.ReadAttributeAsNullableInt(attributeName);
								break;
						}
					}
					return;
				}
				switch (reader.Name)
				{
					case "id":
						Id = reader.ReadElementContentAsInt();
						break;
					case "issue_id":
						IssueId = reader.ReadElementContentAsInt();
						break;
					case "issue_to_id":
						IssueToId = reader.ReadElementContentAsInt();
						break;
					case "relation_type":
						var rt = reader.ReadElementContentAsString();
						if (!string.IsNullOrEmpty(rt))
						{
							Type =
								(IssueRelationType)
									Enum.Parse(typeof(IssueRelationType), rt, true);
						}
						break;
					case "delay":
						Delay = reader.ReadElementContentAsNullableInt();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("issue_to_id", IssueToId.ToString());
			writer.WriteElementString("relation_type", Type.ToString());
			if (Type == IssueRelationType.Precedes || Type == IssueRelationType.Follows)
				writer.WriteIfNotDefaultOrNull(Delay, "delay");
		}
	}
}
