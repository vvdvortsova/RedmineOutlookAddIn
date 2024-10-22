﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Redmine.Net.Api.Types;
using Version = Redmine.Net.Api.Types.Version;

namespace Redmine.Net.Api
{
	public partial class RedmineManager
	{
		public event EventHandler<AsyncEventArgs> DownloadCompleted;

		public async Task<User> GetCurrentUserAsync(NameValueCollection parameters = null)
		{
			var uri = string.Format(
				REQUEST_FORMAT,
				_host,
				_urls[typeof(User)],
				CURRENT_USER_URI,
				_mimeFormat);
			var result = await new RedmineWebClient().DownloadStringTaskAsync(uri);
			return DeserializeResult<User>(result);
		}

		public async Task<WikiPage> GetWikiPageAsync(
			string projectId,
			NameValueCollection parameters,
			string pageName,
			uint version = 0)
		{
			var uri = version == 0
				? string.Format(WIKI_PAGE_FORMAT, _host, projectId, pageName, _mimeFormat)
				: string.Format(
					WIKI_VERSION_FORMAT,
					_host,
					projectId,
					pageName,
					version,
					_mimeFormat);
			var result = await new RedmineWebClient().DownloadStringTaskAsync(uri);
			return DeserializeResult<WikiPage>(result);
		}

		public Guid CreateOrUpdateWikiPageAsync(
			string projectId,
			string pageName,
			WikiPage wikiPage)
		{
			var result = Serialize(wikiPage);
			if (string.IsNullOrEmpty(result)) return Guid.Empty;
			var id = Guid.NewGuid();
			using (var wc = CreateWebClient(null))
			{
				wc.DownloadStringCompleted += WcDownloadStringCompleted;
				wc.UploadStringAsync(
					new Uri(
						string.Format(
							WIKI_PAGE_FORMAT,
							_host,
							projectId,
							pageName,
							_mimeFormat)),
					PUT,
					result,
					new AsyncToken
					{
						Method = RedmineMethod.CreateWiki,
						Parameter = projectId,
						ResponseType = typeof(WikiPage),
						TokenId = id
					});
			}
			return id;
		}

		public Guid DeleteWikiPageAsync(string projectId, string pageName)
		{
			using (var wc = CreateWebClient(null))
			{
				var id = Guid.NewGuid();
				wc.DownloadStringCompleted += WcDownloadStringCompleted;
				wc.UploadStringAsync(
					new Uri(
						string.Format(
							WIKI_PAGE_FORMAT,
							_host,
							projectId,
							pageName,
							_mimeFormat)),
					DELETE,
					string.Empty,
					new AsyncToken
					{
						Method = RedmineMethod.DeleteObject,
						ResponseType = typeof(WikiPage),
						Parameter = id,
						TokenId = id
					});
				return id;
			}
		}

		/// <summary>
		///     Support for adding attachments through the REST API is added in Redmine 1.4.0.
		///     Upload a file to server. This method does not block the calling thread.
		/// </summary>
		/// <param name="data">The content of the file that will be uploaded on server.</param>
		/// <returns>Returns the Guid associated with the async request.</returns>
		public Guid UploadDataAsync(byte[] data)
		{
			using (var wc = CreateUploadWebClient())
			{
				var id = Guid.NewGuid();
				wc.UploadDataCompleted += WcUploadDataCompleted;
				wc.UploadDataAsync(
					new Uri(string.Format(FORMAT, _host, "uploads", _mimeFormat)),
					POST,
					data,
					new AsyncToken
					{
						Method = RedmineMethod.UploadData,
						ResponseType = typeof(Upload),
						TokenId = id
					});
				return id;
			}
		}

		/// <summary>
		///     Adds an existing user to a group. This method does not block the calling thread.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns>Returns the Guid associated with the async request.</returns>
		public Guid AddUserToGroupAsync(int groupId, int userId)
		{
			using (var wc = CreateWebClient(null))
			{
				var id = Guid.NewGuid();
				var asyncToken = new AsyncToken
				{
					Method = RedmineMethod.AddUserToGroup,
					Parameter = userId,
					TokenId = id
				};
				wc.DownloadStringCompleted += WcDownloadStringCompleted;
				wc.UploadStringAsync(
					new Uri(
						string.Format(
							REQUEST_FORMAT,
							_host,
							_urls[typeof(Group)],
							groupId + "/users",
							_mimeFormat)),
					POST,
					_mimeFormat == MimeFormat.XML
						? "<user_id>" + userId + "</user_id>"
						: "user_id:" + userId,
					asyncToken);
				return id;
			}
		}

		/// <summary>
		///     Removes an user from a group. This method does not block the calling thread.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns>Returns the Guid associated with the async request.</returns>
		public Guid DeleteUserFromGroupAsync(int groupId, int userId)
		{
			using (var wc = CreateWebClient(null))
			{
				var id = Guid.NewGuid();
				wc.DownloadStringCompleted += WcDownloadStringCompleted;
				wc.UploadStringAsync(
					new Uri(
						string.Format(
							REQUEST_FORMAT,
							_host,
							_urls[typeof(Group)],
							groupId + "/users/" + userId,
							_mimeFormat)),
					DELETE,
					string.Empty,
					new AsyncToken
					{
						Method = RedmineMethod.DeleteUserFromGroup,
						Parameter = userId,
						TokenId = id
					});
				return id;
			}
		}

		/// <summary>
		///     Creates a new Redmine object. This method does not block the calling thread.
		/// </summary>
		/// <typeparam name="T">The type of object to create.</typeparam>
		/// <param name="obj">The object to create.</param>
		/// <returns>Returns the Guid associated with the async request.</returns>
		public Guid CreateObjectAsync<T>(T obj) where T : class, new()
		{
			var type = typeof(T);
			if (!_urls.ContainsKey(type)) return Guid.Empty;
			var result = Serialize(obj);
			if (string.IsNullOrEmpty(result)) return Guid.Empty;
			using (var wc = CreateWebClient(null))
			{
				var id = Guid.NewGuid();
				wc.DownloadStringCompleted += WcDownloadStringCompleted;
				wc.UploadStringAsync(
					new Uri(string.Format(FORMAT, _host, _urls[type], _mimeFormat)),
					POST,
					result,
					new AsyncToken
					{
						Method = RedmineMethod.CreateObject,
						ResponseType = type,
						Parameter = obj,
						TokenId = id
					});
				return id;
			}
		}

		/// <summary>
		///     Updates a Redmine object. This method does not block the calling thread.
		/// </summary>
		/// <typeparam name="T">The type of object to be update.</typeparam>
		/// <param name="id">The id of the object to be update.</param>
		/// <param name="obj">The object to be update.</param>
		/// <param name="projectId"></param>
		/// <returns>Returns the Guid associated with the async request.</returns>
		public Guid UpdateObjectAsync<T>(string id, T obj, string projectId = null)
			where T : class, new()
		{
			var type = typeof(T);
			if (!_urls.ContainsKey(type)) return Guid.Empty;
			var request = Serialize(obj);
			if (string.IsNullOrEmpty(request)) return Guid.Empty;
			using (var wc = CreateWebClient(null))
			{
				var guid = Guid.NewGuid();
				var asyncToken = new AsyncToken
				{
					Method = RedmineMethod.UpdateObject,
					ResponseType = type,
					Parameter = obj,
					TokenId = guid
				};
				wc.DownloadStringCompleted += WcDownloadStringCompleted;
				if (type == typeof(Version) || type == typeof(IssueCategory) ||
					type == typeof(ProjectMembership))
				{
					if (string.IsNullOrEmpty(projectId))
						throw new RedmineException("The project owner id is mandatory!");
					wc.UploadStringAsync(
						new Uri(
							string.Format(
								ENTITY_WITH_PARENT_FORMAT,
								_host,
								"projects",
								projectId,
								_urls[type],
								_mimeFormat)),
						PUT,
						request,
						asyncToken);
				}
				else
					wc.UploadStringAsync(
						new Uri(
							string.Format(
								REQUEST_FORMAT,
								_host,
								_urls[type],
								id,
								_mimeFormat)),
						PUT,
						request,
						asyncToken);
				return guid;
			}
		}

		/// <summary>
		///     Deletes the Redmine object. This method does not block the calling thread.
		/// </summary>
		/// <typeparam name="T">The type of objects to delete.</typeparam>
		/// <param name="id">The id of the object to delete</param>
		/// <param name="parameters">Optional filters and/or optional fetched data.</param>
		/// <returns>Returns the Guid associated with the async request.</returns>
		public Guid DeleteObjectAsync<T>(string id, NameValueCollection parameters)
			where T : class
		{
			var type = typeof(T);
			if (!_urls.ContainsKey(typeof(T))) return Guid.Empty;
			using (var wc = CreateWebClient(parameters))
			{
				var guid = Guid.NewGuid();
				wc.DownloadStringCompleted += WcDownloadStringCompleted;
				wc.UploadStringAsync(
					new Uri(
						string.Format(REQUEST_FORMAT, _host, _urls[type], id, _mimeFormat)),
					DELETE,
					string.Empty,
					new AsyncToken
					{
						Method = RedmineMethod.DeleteObject,
						ResponseType = type,
						Parameter = id,
						TokenId = guid
					});
				return guid;
			}
		}

		private void WcDownloadStringCompleted(
			object sender,
			DownloadStringCompletedEventArgs e)
		{
			var ut = (AsyncToken)e.UserState;
			if (e.Error != null)
				HandleWebException((WebException)e.Error, ut.Method.ToString());
			else if (!e.Cancelled)
			{
				ShowAsyncResult(e.Result, ut.ResponseType, ut.Method, ut.JsonRoot);
			}
		}

		private void WcUploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
		{
			var ut = (AsyncToken)e.UserState;
			if (e.Error != null)
				HandleWebException((WebException)e.Error, ut.Method.ToString());
			else if (!e.Cancelled)
			{
				var responseString = Encoding.ASCII.GetString(e.Result);
				ShowAsyncResult(responseString, ut.ResponseType, ut.Method, ut.JsonRoot);
			}
		}

		public T DeserializeResult<T>(string result)
		{
			int totalItems;
			return DeserializeResult<T>(result, out totalItems);
		}

		public T DeserializeResult<T>(
			string result,
			out int totalItems,
			string jsonRoot = null)
		{
			totalItems = 0;
			if (string.IsNullOrWhiteSpace(result)) return default(T);
			var type = typeof(T);
			if (type == typeof(List<T>))
			{
				if (_mimeFormat == MimeFormat.JSON)
				{
					return
						(T)
							RedmineSerialization.JsonDeserializeToList(
								result,
								jsonRoot,
								type,
								out totalItems);
				}
				using (var text = new StringReader(result))
				{
					using (var xmlReader = new XmlTextReader(text))
					{
						xmlReader.WhitespaceHandling = WhitespaceHandling.None;
						xmlReader.Read();
						xmlReader.Read();
						totalItems = xmlReader.ReadAttributeAsInt("total_count");
						return (T)(object)xmlReader.ReadElementContentAsCollection(type);
					}
				}
			}
			if (_mimeFormat == MimeFormat.JSON)
			{
				return (T)RedmineSerialization.JsonDeserialize(result, type, jsonRoot);
			}
			return (T)RedmineSerialization.FromXml(result, type);
		}

		private void ShowAsyncResult(
			string response,
			Type responseType,
			RedmineMethod method,
			string jsonRoot)
		{
			var aev = new AsyncEventArgs();
			try
			{
				if (_mimeFormat == MimeFormat.JSON)
					if (method == RedmineMethod.GetObjectList)
					{
						int totalItems;
						aev.Result = RedmineSerialization.JsonDeserializeToList(
							response,
							jsonRoot,
							responseType,
							out totalItems);
						aev.TotalItems = totalItems;
					}
					else
						aev.Result = RedmineSerialization.JsonDeserialize(
							response,
							responseType,
							null);
				else if (method == RedmineMethod.GetObjectList)
				{
					using (var text = new StringReader(response))
					{
						using (var xmlReader = new XmlTextReader(text))
						{
							xmlReader.WhitespaceHandling = WhitespaceHandling.None;
							xmlReader.Read();
							xmlReader.Read();
							aev.TotalItems = xmlReader.ReadAttributeAsInt("total_count");
							aev.Result =
								xmlReader.ReadElementContentAsCollection(responseType);
						}
					}
				}
				else
					aev.Result = RedmineSerialization.FromXml(response, responseType);
			}
			catch (ThreadAbortException ex)
			{
				aev.Error = ex.Message;
			}
			catch (Exception ex)
			{
				aev.Error = ex.Message;
			}
			if (DownloadCompleted != null) DownloadCompleted(this, aev);
		}
	}

	internal class AsyncToken
	{
		public Guid TokenId { get; set; }
		public RedmineMethod Method { get; set; }
		public Type ResponseType { get; set; }
		public object Parameter { get; set; }
		public string JsonRoot { get; set; }
	}

	internal enum RedmineMethod
	{
		DeleteObject,
		UpdateObject,
		CreateObject,
		GetObject,
		GetObjectList,
		DeleteUserFromGroup,
		AddUserToGroup,
		UploadData,
		GetCurrentUser,
		GetWikiPage,
		GetAllWikis,
		CreateWiki,
		UpdateWiki,
		DeleteWiki
	}

	public class AsyncEventArgs : EventArgs
	{
		public string Error { get; set; }
		public object Result { get; set; }
		public int TotalItems { get; set; }
	}
}
