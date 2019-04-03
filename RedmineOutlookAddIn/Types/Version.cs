using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.3
	/// </summary>
	[XmlRoot("version")]
	public class Version : IdentifiableName, IEquatable<Version>
	{
		/// <summary>
		///     Gets or sets the project.
		/// </summary>
		/// <value>The project.</value>
		[XmlElement("project")]
		public IdentifiableName Project { get; set; }

		/// <summary>
		///     Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		[XmlElement("description")]
		public string Description { get; set; }

		/// <summary>
		///     Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		[XmlElement("status")]
		public VersionStatus Status { get; set; }

		/// <summary>
		///     Gets or sets the due date.
		/// </summary>
		/// <value>The due date.</value>
		[XmlElement("due_date")]
		public DateTime? DueDate { get; set; }

		/// <summary>
		///     Gets or sets the sharing.
		/// </summary>
		/// <value>The sharing.</value>
		[XmlElement("sharing")]
		public VersionSharing Sharing { get; set; }

		/// <summary>
		///     Gets or sets the created on.
		/// </summary>
		/// <value>The created on.</value>
		[XmlElement("created_on")]
		public DateTime? CreatedOn { get; set; }

		/// <summary>
		///     Gets or sets the updated on.
		/// </summary>
		/// <value>The updated on.</value>
		[XmlElement("updated_on")]
		public DateTime? UpdatedOn { get; set; }

		/// <summary>
		///     Gets or sets the custom fields.
		/// </summary>
		/// <value>The custom fields.</value>
		[XmlArray("custom_fields")]
		[XmlArrayItem("custom_field")]
		public IList<IssueCustomField> CustomFields { get; set; }

		public bool Equals(Version other)
		{
			if (other == null) return false;
			return (Id == other.Id && Name == other.Name && Project == other.Project &&
					Description == other.Description && Status == other.Status &&
					DueDate == other.DueDate && Sharing == other.Sharing &&
					CreatedOn == other.CreatedOn && UpdatedOn == other.UpdatedOn &&
					CustomFields == other.CustomFields);
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
					case "project":
						Project = new IdentifiableName(reader);
						break;
					case "description":
						Description = reader.ReadElementContentAsString();
						break;
					case "status":
						Status =
							(VersionStatus)
								Enum.Parse(
									typeof(VersionStatus),
									reader.ReadElementContentAsString(),
									true);
						break;
					case "due_date":
						DueDate = reader.ReadElementContentAsNullableDateTime();
						break;
					case "sharing":
						Sharing =
							(VersionSharing)
								Enum.Parse(
									typeof(VersionSharing),
									reader.ReadElementContentAsString(),
									true);
						break;
					case "created_on":
						CreatedOn = reader.ReadElementContentAsNullableDateTime();
						break;
					case "updated_on":
						UpdatedOn = reader.ReadElementContentAsNullableDateTime();
						break;
					case "custom_fields":
						CustomFields =
							reader.ReadElementContentAsCollection<IssueCustomField>();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("name", Name);
			writer.WriteElementString("status", Status.ToString());
			writer.WriteElementString("sharing", Sharing.ToString());
			writer.WriteIfNotDefaultOrNull(DueDate, "due_date");
			writer.WriteElementString("description", Description);
		}
	}

	public enum VersionStatus
	{
		Open = 1,
		Locked,
		Closed
	}

	public enum VersionSharing
	{
		None = 1,
		Descendants,
		Hierarchy,
		Tree,
		System
	}
}
