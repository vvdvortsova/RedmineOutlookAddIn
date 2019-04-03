using System;
using System.Xml;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.3
	/// </summary>
	[XmlRoot("issue_status")]
	public class IssueStatus : IdentifiableName, IEquatable<IssueStatus>
	{
		/// <summary>
		///     Gets or sets a value indicating whether IssueStatus is default.
		/// </summary>
		/// <value>
		///     <c>true</c> if IssueStatus is default; otherwise, <c>false</c>.
		/// </value>
		[XmlElement("is_default")]
		public bool IsDefault { get; set; }

		/// <summary>
		///     Gets or sets a value indicating whether IssueStatus is closed.
		/// </summary>
		/// <value><c>true</c> if IssueStatus is closed; otherwise, <c>false</c>.</value>
		[XmlElement("is_closed")]
		public bool IsClosed { get; set; }

		public bool Equals(IssueStatus other)
		{
			if (other == null) return false;
			return (Id == other.Id && Name == other.Name && IsClosed == other.IsClosed &&
					IsDefault == other.IsDefault);
		}

		public override void ReadXml(XmlReader reader)
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
					case "is_closed":
						IsClosed = reader.ReadElementContentAsBoolean();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public override void WriteXml(XmlWriter writer) { }
	}
}
