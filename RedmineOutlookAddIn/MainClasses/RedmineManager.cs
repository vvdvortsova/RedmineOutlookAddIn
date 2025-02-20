﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Redmine.Net.Api.Types;
using Group = Redmine.Net.Api.Types.Group;
using Version = Redmine.Net.Api.Types.Version;
using System.Windows.Forms;

namespace Redmine.Net.Api
{
	/// <summary>
	///     The main class to access Redmine API.
	/// </summary>
	public partial class RedmineManager
	{
		private const string REQUEST_FORMAT = "{0}/{1}/{2}.{3}";
		private const string FORMAT = "{0}/{1}.{2}";
		private const string WIKI_INDEX_FORMAT = "{0}/projects/{1}/wiki/index.{2}";
		private const string WIKI_PAGE_FORMAT = "{0}/projects/{1}/wiki/{2}.{3}";
		private const string WIKI_VERSION_FORMAT = "{0}/projects/{1}/wiki/{2}/{3}.{4}";
		private const string ENTITY_WITH_PARENT_FORMAT = "{0}/{1}/{2}/{3}.{4}";
		private const string CURRENT_USER_URI = "current";
		private const string PUT = "PUT";
		private const string POST = "POST";
		private const string DELETE = "DELETE";
		private readonly CredentialCache _credentialCache;
		private readonly string _host, _apiKey, _basicAuthorization;
		private readonly string _mimeFormat;

		public readonly Dictionary<Type, string> _urls = new Dictionary<Type, string>
		{
			{typeof (Issue), "issues"},
			{typeof (Project), "projects"},
			{typeof (User), "users"},
			{typeof (News), "news"},
			{typeof (Query), "queries"},
			{typeof (Version), "versions"},
			{typeof (Attachment), "attachments"},
			{typeof (IssueRelation), "relations"},
			{typeof (TimeEntry), "time_entries"},
			{typeof (IssueStatus), "issue_statuses"},
			{typeof (Tracker), "trackers"},
			{typeof (IssueCategory), "issue_categories"},
			{typeof (Role), "roles"},
			{typeof (ProjectMembership), "memberships"},
			{typeof (Group), "groups"},
			{typeof (TimeEntryActivity), "enumerations/time_entry_activities"},
			{typeof (IssuePriority), "enumerations/issue_priorities"},
			{typeof (Watcher), "watchers"},
			{typeof (IssueCustomField), "custom_fields"},
			{typeof (CustomField), "custom_fields"}
		};

		/// <summary>
		///     Initializes a new instance of the <see cref="RedmineManager" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="mimeFormat"></param>
		/// <param name="verifyServerCert">if set to <c>true</c> [verify server cert].</param>
		public RedmineManager(
			string host,
			string mimeFormat = MimeFormat.XML,
			bool verifyServerCert = true)
		{
			PageSize = 25;
			Uri uriResult;
			if (!Uri.TryCreate(host, UriKind.Absolute, out uriResult) ||
				!(uriResult.Scheme == Uri.UriSchemeHttp ||
				  uriResult.Scheme == Uri.UriSchemeHttps))
				host = "http://" + host;
			if (!Uri.TryCreate(host, UriKind.Absolute, out uriResult))
				throw new RedmineException("The host is not valid!");
			_host = host;
			_mimeFormat = mimeFormat;
			if (!verifyServerCert)
				ServicePointManager.ServerCertificateValidationCallback +=
					RemoteCertValidate;
			MessageBox.Show("I was created3");
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="RedmineManager" /> class.
		///     Most of the time, the API requires authentication. To enable the API-style authentication, you have to check Enable
		///     REST API in Administration -> Settings -> Authentication. Then, authentication can be done in 2 different ways:
		///     using your regular login/password via HTTP Basic authentication.
		///     using your API key which is a handy way to avoid putting a password in a script. The API key may be attached to
		///     each request in one of the following way:
		///     passed in as a "key" parameter
		///     passed in as a username with a random password via HTTP Basic authentication
		///     passed in as a "X-Redmine-API-Key" HTTP header (added in Redmine 1.1.0)
		///     You can find your API key on your account page ( /my/account ) when logged in, on the right-hand pane of the
		///     default layout.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="apiKey">The API key.</param>
		/// <param name="mimeFormat">The Mime format.</param>
		/// <param name="verifyServerCert">if set to <c>true</c> [verify server cert].</param>
		public RedmineManager(
			string host,
			string apiKey,
			string mimeFormat = MimeFormat.XML,
			bool verifyServerCert = true)
			: this(host, mimeFormat, verifyServerCert)
		{
			PageSize = 25;
			_apiKey = apiKey;
			MessageBox.Show("I was created1");
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="RedmineManager" /> class.
		///     Most of the time, the API requires authentication. To enable the API-style authentication, you have to check Enable
		///     REST API in Administration -> Settings -> Authentication. Then, authentication can be done in 2 different ways:
		///     using your regular login/password via HTTP Basic authentication.
		///     using your API key which is a handy way to avoid putting a password in a script. The API key may be attached to
		///     each request in one of the following way:
		///     passed in as a "key" parameter
		///     passed in as a username with a random password via HTTP Basic authentication
		///     passed in as a "X-Redmine-API-Key" HTTP header (added in Redmine 1.1.0)
		///     You can find your API key on your account page ( /my/account ) when logged in, on the right-hand pane of the
		///     default layout.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="login">The login.</param>
		/// <param name="password">The password.</param>
		/// <param name="mimeFormat">The Mime format.</param>
		/// <param name="verifyServerCert">if set to <c>true</c> [verify server cert].</param>
		public RedmineManager(
			string host,
			string login,
			string password,
			string mimeFormat = MimeFormat.XML,
			bool verifyServerCert = true)
			: this(host, mimeFormat, verifyServerCert)
		{
			PageSize = 25;
			Uri uriResult;
			if (!Uri.TryCreate(host, UriKind.Absolute, out uriResult) ||
				!(uriResult.Scheme == Uri.UriSchemeHttp ||
				  uriResult.Scheme == Uri.UriSchemeHttps))
				host = "http://" + host;
			if (!Uri.TryCreate(host, UriKind.Absolute, out uriResult))
				throw new RedmineException("The host is not valid!");
			_credentialCache = new CredentialCache
			{
				{uriResult, "Basic", new NetworkCredential(login, password)}
			};
			_basicAuthorization = "Basic " +
								 Convert.ToBase64String(
									 Encoding.ASCII.GetBytes(login + ":" + password));
			MessageBox.Show("I was created3");
		}

		/// <summary>
		///     Maximum page-size when retrieving complete object lists
		///     <remarks>
		///         By default only 25 results can be retrieved per request. Maximum is 100. To change the maximum value set
		///         in your Settings -> General, "Objects per page options".By adding (for instance) 9999 there would make you able
		///         to get that many results per request.
		///     </remarks>
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		///     As of Redmine 2.2.0 you can impersonate user setting user login (eg. jsmith). This only works when using the API
		///     with an administrator account, this header will be ignored when using the API with a regular user account.
		/// </summary>
		public string ImpersonateUser { get; set; }

		/// <summary>
		///     Returns the user whose credentials are used to access the API.
		/// </summary>
		/// <param name="parameters">The accepted parameters are: memberships and groups (added in 2.1).</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidOperationException">
		///     An error occurred during deserialization. The original exception is available
		///     using the System.Exception.InnerException property.
		/// </exception>
		public User GetCurrentUser(NameValueCollection parameters = null)
		{
			return
				ExecuteDownload<User>(
					string.Format(
						REQUEST_FORMAT,
						_host,
						_urls[typeof(User)],
						CURRENT_USER_URI,
						_mimeFormat),
					"GetCurrentUser",
					parameters);
		}

		/// <summary>
		///     Returns a list of users.
		/// </summary>
		/// <param name="userStatus">get only users with the given status. Default is 1 (active users)</param>
		/// <param name="name">
		///     filter users on their login, firstname, lastname and mail ; if the pattern contains a space, it
		///     will also return users whose firstname match the first word or lastname match the second word.
		/// </param>
		/// <param name="groupId">get only users who are members of the given group</param>
		/// <returns></returns>
		public IList<User> GetUsers(
			UserStatus userStatus = UserStatus.StatusActive,
			string name = null,
			int groupId = 0)
		{
			var filters = new NameValueCollection
			{
				{"status", ((int) userStatus).ToString(CultureInfo.InvariantCulture)}
			};
			if (!string.IsNullOrWhiteSpace(name)) filters.Add("name", name);
			if (groupId > 0)
				filters.Add("groupId", groupId.ToString(CultureInfo.InvariantCulture));
			return GetTotalObjectList<User>(filters);
		}

		public void AddWatcher(int issueId, int userId)
		{
			ExecuteUpload(
				string.Format(
					REQUEST_FORMAT,
					_host,
					_urls[typeof(Issue)],
					issueId + "/watchers",
					_mimeFormat),
				POST,
				_mimeFormat == MimeFormat.XML
					? "<user_id>" + userId + "</user_id>"
					: "user_id:" + userId,
				"AddWatcher");
		}

		public void RemoveWatcher(int issueId, int userId)
		{
			ExecuteUpload(
				string.Format(
					REQUEST_FORMAT,
					_host,
					_urls[typeof(Issue)],
					issueId + "/watchers/" + userId,
					_mimeFormat),
				DELETE,
				string.Empty,
				"RemoveWatcher");
		}

		/// <summary>
		///     Adds an existing user to a group.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <param name="userId">The user id.</param>
		public void AddUser(int groupId, int userId)
		{
			ExecuteUpload(
				string.Format(
					REQUEST_FORMAT,
					_host,
					_urls[typeof(Group)],
					groupId + "/users",
					_mimeFormat),
				POST,
				_mimeFormat == MimeFormat.XML
					? "<user_id>" + userId + "</user_id>"
					: "user_id:" + userId,
				"AddUser");
		}

		/// <summary>
		///     Removes an user from a group.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <param name="userId">The user id.</param>
		public void DeleteUser(int groupId, int userId)
		{
			ExecuteUpload(
				string.Format(
					REQUEST_FORMAT,
					_host,
					_urls[typeof(Group)],
					groupId + "/users/" + userId,
					_mimeFormat),
				DELETE,
				string.Empty,
				"DeleteUser");
		}

		/// <summary>
		///     Downloads the user whose credentials are used to access the API. This method does not block the calling thread.
		/// </summary>
		/// <returns>Returns the Guid associated with the async request.</returns>
		/// <exception cref="System.InvalidOperationException">
		///     An error occurred during deserialization. The original exception is available
		///     using the System.Exception.InnerException property.
		/// </exception>
		/// <summary>
		///     Returns the details of a wiki page or the details of an old version of a wiki page if the <b>version</b> parameter
		///     is set.
		/// </summary>
		/// <param name="projectId">The project id or identifier.</param>
		/// <param name="parameters">
		///     attachments
		///     The accepted parameters are: memberships and groups (added in 2.1).
		/// </param>
		/// <param name="pageName">The wiki page name.</param>
		/// <param name="version">The version of the wiki page.</param>
		/// <returns></returns>
		public WikiPage GetWikiPage(
			string projectId,
			NameValueCollection parameters,
			string pageName,
			uint version = 0)
		{
			var address = version == 0
				? string.Format(WIKI_PAGE_FORMAT, _host, projectId, pageName, _mimeFormat)
				: string.Format(
					WIKI_VERSION_FORMAT,
					_host,
					projectId,
					pageName,
					version,
					_mimeFormat);
			return ExecuteDownload<WikiPage>(address, "GetWikiPage", parameters);
		}

		/// <summary>
		///     Returns the list of all pages in a project wiki.
		/// </summary>
		/// <param name="projectId">The project id or identifier.</param>
		/// <returns></returns>
		public IList<WikiPage> GetAllWikiPages(string projectId)
		{
			int totalCount;
			return
				ExecuteDownloadList<WikiPage>(
					string.Format(WIKI_INDEX_FORMAT, _host, projectId, _mimeFormat),
					"GetAllWikiPages",
					"wiki",
					out totalCount);
		}

		/// <summary>
		///     Creates or updates a wiki page.
		/// </summary>
		/// <param name="projectId">The project id or identifier.</param>
		/// <param name="pageName">The wiki page name.</param>
		/// <param name="wikiPage">The wiki page to create or update.</param>
		/// <returns></returns>
		public WikiPage CreateOrUpdateWikiPage(
			string projectId,
			string pageName,
			WikiPage wikiPage)
		{
			var result = Serialize(wikiPage);
			if (string.IsNullOrEmpty(result)) return null;
			return
				ExecuteUpload<WikiPage>(
					string.Format(WIKI_PAGE_FORMAT, _host, projectId, pageName, _mimeFormat),
					PUT,
					result,
					"CreateOrUpdateWikiPage");
		}

		/// <summary>
		///     Deletes a wiki page, its attachments and its history. If the deleted page is a parent page, its child pages are not
		///     deleted but changed as root pages.
		/// </summary>
		/// <param name="projectId">The project id or identifier.</param>
		/// <param name="pageName">The wiki page name.</param>
		public void DeleteWikiPage(string projectId, string pageName)
		{
			ExecuteUpload(
				string.Format(WIKI_PAGE_FORMAT, _host, projectId, pageName, _mimeFormat),
				DELETE,
				string.Empty,
				"DeleteWikiPage");
		}

		/// <summary>
		///     Support for adding attachments through the REST API is added in Redmine 1.4.0.
		///     Upload a file to server.
		/// </summary>
		/// <param name="data">The content of the file that will be uploaded on server.</param>
		/// <returns>Returns the token for uploaded file.</returns>
		public Upload UploadFile(byte[] data)
		{
			using (var wc = CreateUploadWebClient())
			{
				try
				{
					var response =
						wc.UploadData(
							string.Format(FORMAT, _host, "uploads", _mimeFormat),
							data);
					var responseString = Encoding.ASCII.GetString(response);
					return Deserialize<Upload>(responseString);
				}
				catch (WebException webException)
				{
					HandleWebException(webException, "Upload");
				}
			}
			return null;
		}

		public byte[] DownloadFile(string address)
		{
			using (var wc = CreateUploadWebClient())
			{
				try
				{
					return wc.DownloadData(address);
				}
				catch (WebException webException)
				{
					HandleWebException(webException, "Download");
				}
			}
			return null;
		}

		/// <summary>
		///     Returns a paginated list of objects.
		/// </summary>
		/// <typeparam name="T">The type of objects to retrieve.</typeparam>
		/// <param name="parameters">Optional filters and/or optional fetched data.</param>
		/// <returns>Returns a paginated list of objects.</returns>
		/// <remarks>
		///     By default only 25 results can be retrieved by request. Maximum is 100. To change the maximum value set in
		///     your Settings -> General, "Objects per page options".By adding (for instance) 9999 there would make you able to get
		///     that many results per request.
		/// </remarks>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		public IList<T> GetObjectList<T>(NameValueCollection parameters)
			where T : class, new()
		{
			int totalCount;
			return GetObjectList<T>(parameters, out totalCount);
		}

		/// <summary>
		///     Returns a paginated list of objects.
		/// </summary>
		/// <typeparam name="T">The type of objects to retrieve.</typeparam>
		/// <param name="parameters">Optional filters and/or optional fetched data.</param>
		/// <param name="totalCount">Provide information about the total object count available in Redmine.</param>
		/// <returns>Returns a paginated list of objects.</returns>
		/// <remarks>
		///     By default only 25 results can be retrieved by request. Maximum is 100. To change the maximum value set in
		///     your Settings -> General, "Objects per page options".By adding (for instance) 9999 there would make you able to get
		///     that many results per request.
		/// </remarks>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		/// <code></code>
		public IList<T> GetObjectList<T>(
			NameValueCollection parameters,
			out int totalCount) where T : class, new()
		{
			totalCount = -1;
			if (!_urls.ContainsKey(typeof(T))) return null;
			var type = typeof(T);
			string address;
			if (type == typeof(Version) || type == typeof(IssueCategory) ||
				type == typeof(ProjectMembership))
			{
				var projectId = GetOwnerId(parameters, "project_id");
				if (string.IsNullOrEmpty(projectId))
					throw new RedmineException(
						"The project id is mandatory! \nCheck if you have included the parameter project_id to parameters.");
				address = string.Format(
					ENTITY_WITH_PARENT_FORMAT,
					_host,
					"projects",
					projectId,
					_urls[type],
					_mimeFormat);
			}
			else if (type == typeof(IssueRelation))
			{
				var issueId = GetOwnerId(parameters, "issue_id");
				if (string.IsNullOrEmpty(issueId))
					throw new RedmineException(
						"The issue id is mandatory! \nCheck if you have included the parameter issue_id to parameters");
				address = string.Format(
					ENTITY_WITH_PARENT_FORMAT,
					_host,
					"issues",
					issueId,
					_urls[type],
					_mimeFormat);
			}
			else
				address = string.Format(FORMAT, _host, _urls[type], _mimeFormat);
			return ExecuteDownloadList<T>(
				address,
				"GetObjectList<" + type.Name + ">",
				_urls[type],
				out totalCount,
				parameters);
		}

		/// <summary>
		///     Returns the complete list of objects.
		/// </summary>
		/// <typeparam name="T">The type of objects to retrieve.</typeparam>
		/// <param name="parameters">Optional filters and/or optional fetched data.</param>
		/// <returns>Returns a complete list of objects.</returns>
		/// <remarks>
		///     By default only 25 results can be retrieved per request. Maximum is 100. To change the maximum value set in
		///     your Settings -> General, "Objects per page options".By adding (for instance) 9999 there would make you able to get
		///     that many results per request.
		/// </remarks>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		public IList<T> GetTotalObjectList<T>(NameValueCollection parameters)
			where T : class, new()
		{
			int totalCount, pageSize;
			List<T> resultList = null;
			if (parameters == null) parameters = new NameValueCollection();
			var offset = 0;
			int.TryParse(parameters["limit"], out pageSize);
			if (pageSize == default(int))
			{
				pageSize = PageSize > 0 ? PageSize : 25;
				parameters.Set("limit", pageSize.ToString(CultureInfo.InvariantCulture));
			}
			do
			{
				parameters.Set("offset", offset.ToString(CultureInfo.InvariantCulture));
				var tempResult = (List<T>)GetObjectList<T>(parameters, out totalCount);
				if (resultList == null)
					resultList = tempResult;
				else
					resultList.AddRange(tempResult);
				offset += pageSize;
			} while (offset < totalCount);
			return resultList;
		}

		/// <summary>
		///     Returns a Redmine object.
		/// </summary>
		/// <typeparam name="T">The type of objects to retrieve.</typeparam>
		/// <param name="id">The id of the object.</param>
		/// <param name="parameters">Optional filters and/or optional fetched data.</param>
		/// <returns>Returns the object of type T.</returns>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		/// <exception cref="System.InvalidOperationException">
		///     An error occurred during deserialization. The original exception is available
		///     using the System.Exception.InnerException property.
		/// </exception>
		/// <code> 
		/// <example>
		///         string issueId = "927";
		///         NameValueCollection parameters = null;
		///         Issue issue = redmineManager.GetObject&lt;Issue&gt;(issueId, parameters);
		///     </example>
		/// </code>
		public T GetObject<T>(string id, NameValueCollection parameters)
			where T : class, new()
		{
			var type = typeof(T);
			return !_urls.ContainsKey(type)
				? null
				: ExecuteDownload<T>(
					string.Format(REQUEST_FORMAT, _host, _urls[type], id, _mimeFormat),
					"GetObject<" + type.Name + ">",
					parameters);
		}

		/// <summary>
		///     Creates a new Redmine object.
		/// </summary>
		/// <typeparam name="T">The type of object to create.</typeparam>
		/// <param name="obj">The object to create.</param>
		/// <param name="ownerId"></param>
		/// <remarks>
		///     When trying to create an object with invalid or missing attribute parameters, you will get a 422 Unprocessable
		///     Entity response. That means that the object could not be created.
		/// </remarks>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		/// <code>
		/// <example>
		///         var project = new Project();
		///         project.Name = "test";
		///         project.Identifier = "the project identifier";
		///         project.Description = "the project description";
		///         redmineManager.CreateObject(project);
		///     </example>
		/// </code>
		public T CreateObject<T>(T obj, string ownerId = null) where T : class, new()
		{
			var type = typeof(T);
			if (!_urls.ContainsKey(type)) return null;
			var result = Serialize(obj);
			if (string.IsNullOrEmpty(result)) return null;
			string address;
			if (type == typeof(Version) || type == typeof(IssueCategory) ||
				type == typeof(ProjectMembership))
			{
				if (string.IsNullOrEmpty(ownerId))
					throw new RedmineException("The owner id(project id) is mandatory!");
				address = string.Format(
					ENTITY_WITH_PARENT_FORMAT,
					_host,
					"projects",
					ownerId,
					_urls[type],
					_mimeFormat);
			}
			else if (type == typeof(IssueRelation))
			{
				if (string.IsNullOrEmpty(ownerId))
					throw new RedmineException("The owner id(issue id) is mandatory!");
				address = string.Format(
					ENTITY_WITH_PARENT_FORMAT,
					_host,
					"issues",
					ownerId,
					_urls[type],
					_mimeFormat);
			}
			else
				address = string.Format(FORMAT, _host, _urls[type], _mimeFormat);
			return ExecuteUpload<T>(
				address,
				POST,
				result,
				"CreateObject<" + type.Name + ">");
		}

		/// <summary>
		///     Updates a Redmine object.
		/// </summary>
		/// <typeparam name="T">The type of object to be update.</typeparam>
		/// <param name="id">The id of the object to be update.</param>
		/// <param name="obj">The object to be update.</param>
		/// <remarks>
		///     When trying to update an object with invalid or missing attribute parameters, you will get a 422 Unprocessable
		///     Entity response. That means that the object could not be updated.
		/// </remarks>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		/// <code></code>
		public void UpdateObject<T>(string id, T obj) where T : class, new()
		{
			UpdateObject(id, obj, null);
		}

		/// <summary>
		///     Updates a Redmine object.
		/// </summary>
		/// <typeparam name="T">The type of object to be update.</typeparam>
		/// <param name="id">The id of the object to be update.</param>
		/// <param name="obj">The object to be update.</param>
		/// <param name="projectId"></param>
		/// <remarks>
		///     When trying to update an object with invalid or missing attribute parameters, you will get a 422 Unprocessable
		///     Entity response. That means that the object could not be updated.
		/// </remarks>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		/// <code></code>
		public void UpdateObject<T>(string id, T obj, string projectId)
			where T : class, new()
		{
			var type = typeof(T);
			if (!_urls.ContainsKey(type)) return;
			var request = Serialize(obj);
			if (string.IsNullOrEmpty(request)) return;
			request = Regex.Replace(request, @"\r\n|\r|\n", "\r\n", RegexOptions.Compiled);
			string address;
			if (type == typeof(Version) || type == typeof(IssueCategory) ||
				type == typeof(ProjectMembership))
			{
				if (string.IsNullOrEmpty(projectId))
					throw new RedmineException("The project owner id is mandatory!");
				address = string.Format(
					ENTITY_WITH_PARENT_FORMAT,
					_host,
					"projects",
					projectId,
					_urls[type],
					_mimeFormat);
			}
			else
			{
				address = string.Format(REQUEST_FORMAT, _host, _urls[type], id, _mimeFormat);
			}
			ExecuteUpload(address, PUT, request, "UpdateObject<" + type.Name + ">");
		}

		/// <summary>
		///     Deletes the Redmine object.
		/// </summary>
		/// <typeparam name="T">The type of objects to delete.</typeparam>
		/// <param name="id">The id of the object to delete</param>
		/// <param name="parameters">Optional filters and/or optional fetched data.</param>
		/// <exception cref="Redmine.Net.Api.RedmineException"></exception>
		/// <code></code>
		public void DeleteObject<T>(string id, NameValueCollection parameters)
			where T : class
		{
			var type = typeof(T);
			if (!_urls.ContainsKey(typeof(T))) return;
			ExecuteUpload(
				string.Format(REQUEST_FORMAT, _host, _urls[type], id, _mimeFormat),
				DELETE,
				string.Empty,
				"DeleteObject<" + type.Name + ">");
		}

		/// <summary>
		///     Creates the Redmine web client.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		/// <code></code>
		protected WebClient CreateWebClient(NameValueCollection parameters)
		{
			var webClient = new RedmineWebClient();
			if (parameters != null) webClient.QueryString = parameters;
			if (!string.IsNullOrEmpty(_apiKey))
			{
				webClient.QueryString["key"] = _apiKey;
			}
			else
			{
				if (_credentialCache != null) webClient.Credentials = _credentialCache;
			}
			if (!string.IsNullOrWhiteSpace(ImpersonateUser))
				webClient.Headers.Add("X-Redmine-Switch-User", ImpersonateUser);
			webClient.Headers.Add(
				"Content-Type",
				_mimeFormat == MimeFormat.JSON
					? "application/json; charset=utf-8"
					: "application/xml; charset=utf-8");
			webClient.Encoding = Encoding.UTF8;
			return webClient;
		}

		/// <summary>
		///     Creates the Redmine web client.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		/// <code></code>
		protected WebClient CreateUploadWebClient(NameValueCollection parameters = null)
		{
			var webClient = new RedmineWebClient();
			if (parameters != null) webClient.QueryString = parameters;
			if (!string.IsNullOrEmpty(_apiKey))
			{
				webClient.QueryString["key"] = _apiKey;
			}
			else
			{
				if (_credentialCache != null) webClient.Credentials = _credentialCache;
			}
			webClient.UseDefaultCredentials = false;
			webClient.Headers.Add("Content-Type", "application/octet-stream");
			// Workaround - it seems that WebClient doesn't send credentials in each POST request
			webClient.Headers.Add("Authorization", _basicAuthorization);
			return webClient;
		}

		/// <summary>
		///     This is to take care of SSL certification validation which are not issued by Trusted Root CA. Recommended for
		///     testing  only.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="cert">The cert.</param>
		/// <param name="chain">The chain.</param>
		/// <param name="error">The error.</param>
		/// <returns></returns>
		/// <code></code>
		protected bool RemoteCertValidate(
			object sender,
			X509Certificate cert,
			X509Chain chain,
			SslPolicyErrors error)
		{
			//Cert Validation Logic
			return true;
		}

		private void HandleWebException(WebException exception, string method)
		{
			if (exception == null) return;
			switch (exception.Status)
			{
				case WebExceptionStatus.Timeout:
					throw new RedmineException("Timeout!");
				case WebExceptionStatus.NameResolutionFailure:
					throw new RedmineException("Bad domain name!");
				case WebExceptionStatus.ProtocolError:
					{
						var response = (HttpWebResponse)exception.Response;
						switch ((int)response.StatusCode)
						{
							case (int)HttpStatusCode.InternalServerError:
							case (int)HttpStatusCode.Unauthorized:
							case (int)HttpStatusCode.NotFound:
							case (int)HttpStatusCode.Forbidden:
								throw new RedmineException(response.StatusDescription);
							case (int)HttpStatusCode.Conflict:
								throw new RedmineException(
									"The page that you are trying to update is staled!");
							case 422:
								var errors = ReadWebExceptionResponse(exception.Response);
								var message = string.Empty;
								if (errors != null)
								{
									message = errors.Aggregate(
										message,
										(current, error) => current + (error.Info + "\n"));
								}
								throw new RedmineException(
									method + " has invalid or missing attribute parameters: " +
									message);
							case (int)HttpStatusCode.NotAcceptable:
								throw new RedmineException(response.StatusDescription);
						}
					}
					break;
				default:
					throw new RedmineException(exception.Message);
			}
		}

		private static string GetOwnerId(
			NameValueCollection parameters,
			string parameterName)
		{
			if (parameters == null) return null;
			var ownerId = parameters.Get(parameterName);
			return string.IsNullOrEmpty(ownerId) ? null : ownerId;
		}

		private IEnumerable<Error> ReadWebExceptionResponse(WebResponse webResponse)
		{
			using (var dataStream = webResponse.GetResponseStream())
			{
				if (dataStream == null) return null;
				var reader = new StreamReader(dataStream);
				var responseFromServer = reader.ReadToEnd();
				if (responseFromServer.Trim().Length > 0)
				{
					try
					{
						int totalCount;
						return DeserializeList<Error>(
							responseFromServer,
							"errors",
							out totalCount);
					}
					catch (Exception ex)
					{
						Trace.TraceError(ex.Message);
					}
				}
				return null;
			}
		}

		private string Serialize<T>(T obj) where T : class, new()
		{
			if (_mimeFormat == MimeFormat.JSON)
				return RedmineSerialization.JsonSerializer(obj);
			return RedmineSerialization.ToXml(obj);
		}

		private T Deserialize<T>(string response) where T : class, new()
		{
			var type = typeof(T);
			if (_mimeFormat == MimeFormat.JSON)
				return type == typeof(IssueCategory)
					? RedmineSerialization.JsonDeserialize<T>(response, "issue_category")
					: RedmineSerialization.JsonDeserialize<T>(
						response,
						type == typeof(IssueRelation) ? "relation" : null);
			return RedmineSerialization.FromXml<T>(response);
		}

		private IList<T> DeserializeList<T>(
			string response,
			string jsonRoot,
			out int totalCount) where T : class, new()
		{
			var type = typeof(T);
			if (_mimeFormat == MimeFormat.JSON)
			{
				if (type == typeof(IssuePriority)) jsonRoot = "issue_priorities";
				if (type == typeof(TimeEntryActivity))
					jsonRoot = "time_entry_activities";
				return RedmineSerialization.JsonDeserializeToList<T>(
					response,
					jsonRoot,
					out totalCount);
			}
			using (var text = new StringReader(response))
			{
				using (var xmlReader = new XmlTextReader(text))
				{
					xmlReader.WhitespaceHandling = WhitespaceHandling.None;
					xmlReader.Read();
					xmlReader.Read();
					totalCount = xmlReader.ReadAttributeAsInt("total_count");
					return xmlReader.ReadElementContentAsCollection<T>();
				}
			}
		}

		private void ExecuteUpload(
			string address,
			string actionType,
			string data,
			string methodName)
		{
			using (var wc = CreateWebClient(null))
			{
				try
				{
					if (actionType == POST || actionType == DELETE || actionType == PUT)
					{
						wc.UploadString(address, actionType, data);
					}
				}
				catch (WebException webException)
				{
					HandleWebException(webException, methodName);
				}
			}
		}

		private T ExecuteUpload<T>(
			string address,
			string actionType,
			string data,
			string methodName) where T : class, new()
		{
			using (var wc = CreateWebClient(null))
			{
				try
				{
					if (actionType == POST || actionType == DELETE || actionType == PUT)
					{
						var response = wc.UploadString(address, actionType, data);
						return Deserialize<T>(response);
					}
				}
				catch (WebException webException)
				{
					HandleWebException(webException, methodName);
				}
				return default(T);
			}
		}

		private T ExecuteDownload<T>(
			string address,
			string methodName,
			NameValueCollection parameters = null) where T : class, new()
		{
			using (var wc = CreateWebClient(parameters))
			{
				try
				{
					var response = wc.DownloadString(address);
					return Deserialize<T>(response);
				}
				catch (WebException webException)
				{
					HandleWebException(webException, methodName);
				}
				return default(T);
			}
		}

		private IList<T> ExecuteDownloadList<T>(
			string address,
			string methodName,
			string jsonRoot,
			out int totalCount,
			NameValueCollection parameters = null) where T : class, new()
		{
			totalCount = -1;
			using (var wc = CreateWebClient(parameters))
			{
				try
				{
					var response = wc.DownloadString(address);
					return DeserializeList<T>(response, jsonRoot, out totalCount);
				}
				catch (WebException webException)
				{
					HandleWebException(webException, methodName);
				}
				return null;
			}
		}
	}
}