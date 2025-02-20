﻿using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Support for adding attachments through the REST API is added in Redmine 1.4.0.
	/// </summary>
	[XmlRoot("upload")]
	public class Upload : IEquatable<Upload>
	{
		/// <summary>
		///     Gets or sets the uploaded token.
		/// </summary>
		/// <value>The name of the file.</value>
		[XmlElement("token")]
		public string Token { get; set; }

		/// <summary>
		///     Gets or sets the name of the file.
		///     Maximum allowed file size (1024000).
		/// </summary>
		/// <value>The name of the file.</value>
		[XmlElement("filename")]
		public string FileName { get; set; }

		/// <summary>
		///     Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		[XmlElement("content_type")]
		public string ContentType { get; set; }

		/// <summary>
		///     Gets or sets the file description. (Undocumented feature)
		/// </summary>
		/// <value>The file descroütopm.</value>
		[XmlElement("description")]
		public string Description { get; set; }

		public bool Equals(Upload other)
		{
			return other != null && Token == other.Token;
		}

		public XmlSchema GetSchema()
		{
			return null;
		}
	}
}
