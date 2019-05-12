using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System.Collections.Specialized;

using Microsoft.Office.Interop.Outlook;


namespace RedmineOutlookAddIn
{

	public partial class ThisAddIn
	{


		private Outlook.Inspectors Inspectors;

		static string host = "http://79.137.216.214/redmine/";
		static string apiKey = "4444025d7e83c49e92466b5399ba7ee06c464637";
		public static RedmineManager manager = new RedmineManager(host, apiKey);



		internal Object selObject = null;
		internal Outlook.Explorer currentExplorer = null;
		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{

			currentExplorer = this.Application.ActiveExplorer();
			currentExplorer.SelectionChange += new Outlook
				.ExplorerEvents_10_SelectionChangeEventHandler
				(CurrentExplorer_Event);


		}

		private void CurrentExplorer_Event()
		{
			Outlook.MAPIFolder selectedFolder =
				this.Application.ActiveExplorer().CurrentFolder;
			String expMessage = "Your current folder is "
				+ selectedFolder.Name + ".\n";
			String itemMessage = "Item is unknown.";
			try
			{
				if (this.Application.ActiveExplorer().Selection.Count > 0)
				{
					selObject = this.Application.ActiveExplorer().Selection[1];
					if (selObject is Outlook.MailItem)
					{
						Outlook.MailItem mailItem =
							(selObject as Outlook.MailItem);
						itemMessage = "The item is an e-mail message." +
							" The subject is " + mailItem.Subject + ".";
						mailItem.Display(false);
					}
					else if (selObject is Outlook.ContactItem)
					{
						Outlook.ContactItem contactItem =
							(selObject as Outlook.ContactItem);
						itemMessage = "The item is a contact." +
							" The full name is " + contactItem.Subject + ".";
						contactItem.Display(false);
					}
					else if (selObject is Outlook.AppointmentItem)
					{
						Outlook.AppointmentItem apptItem =
							(selObject as Outlook.AppointmentItem);
						itemMessage = "The item is an appointment." +
							" The subject is " + apptItem.Subject + ".";
					}
					else if (selObject is Outlook.TaskItem)
					{
						Outlook.TaskItem taskItem =
							(selObject as Outlook.TaskItem);
						itemMessage = "The item is a task. The body is "
							+ taskItem.Body + ".";
						ShowTaskOrShowParentTaskForm showTask = new ShowTaskOrShowParentTaskForm();
						showTask.Show();


					}
					else if (selObject is Outlook.MeetingItem)
					{
						Outlook.MeetingItem meetingItem =
							(selObject as Outlook.MeetingItem);
						itemMessage = "The item is a meeting item. " +
							 "The subject is " + meetingItem.Subject + ".";
					}
				}
				expMessage = expMessage + itemMessage;
			}
			catch (System.Exception ex)
			{
				expMessage = ex.Message;
			}
			MessageBox.Show(expMessage);
		}



		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{

			// Note: Outlook no longer raises this event. If you have code that 
			//    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
		}


		#region "Outlook07 Menu"

		

		private void TasksUpdates()
		{

			Outlook.NameSpace namespce = null;
			Outlook.Items tasks = null;
			Outlook.Application oApp = new Outlook.Application();
			namespce = oApp.GetNamespace("MAPI");
			tasks = namespce.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderTasks).Items;
			string temp = string.Empty;
			string tmpRedm = string.Empty;
			bool isExist = false;
			string AddedTastFromOutlook = string.Empty;

			foreach (Issue issue in manager.GetObjectList<Issue>(new NameValueCollection()))
			{
				foreach (Outlook.TaskItem task in tasks)
				{
					if (task.Subject == issue.Subject)
					{
						if ((DateTime)issue.UpdatedOn > task.LastModificationTime)
						{
							UpdateOneTask(task, issue);
						}
						isExist = true;
						continue;
					}

					temp += $"{task.Body}+{Environment.NewLine}";
					bool isCompleeted = task.Complete;//Check if your task is compleeted in your application you could use EntryID property to identify a task 
					if (isCompleeted == true && task.Status != OlTaskStatus.olTaskComplete)
					{
						task.MarkComplete();
						task.Save();
					}

					isExist = false;
				}
				if (isExist)
				{
					Outlook.TaskItem task = null;

					task = (Outlook.TaskItem)this.Application.CreateItem(Outlook.OlItemType.olTaskItem);
					task.Subject = "Review site design";
					task.StartDate = DateTime.Now;
					task.DueDate = DateTime.Now.AddDays(2);
					task.Status = Outlook.OlTaskStatus.olTaskNotStarted;
					task.Save();
					//newTaskItem.StartDate = (DateTime)issue.StartDate;
					//newTaskItem.DueDate = (DateTime)issue.DueDate;
					//AddedTastFromOutlook += $"{newTaskItem.Subject}+{Environment.NewLine}";

					//Create a issue.


				}
			}

			MessageBox.Show("NewTAsk  " + AddedTastFromOutlook);





		}

		private void UpdateOneTask(TaskItem task, Issue issue)
		{
			//task.DueDate = (DateTime)issue.DueDate;
			task.Body = issue.Description;

		}




		#endregion
		#region VSTO generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(ThisAddIn_Startup);
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}
		#endregion





		internal void AddTask()
		{
			try
			{

				Outlook.TaskItem newTaskItem =
					(Outlook.TaskItem)this.Application.CreateItem(Outlook.OlItemType.olTaskItem);
				newTaskItem.StartDate = DateTime.Now.AddHours(2);


				newTaskItem.Body = "Try to create a task";
				//Create a issue.

				newTaskItem.Save();
				newTaskItem.Display("True");

				var newIssue = new Issue
				{
					Subject = newTaskItem.Subject,
					Project = new IdentifiableName { Id = 1 },
					Description = newTaskItem.Body,
					StartDate = newTaskItem.StartDate,
					DueDate = newTaskItem.DueDate,

				};
				manager.CreateObject(newIssue);


				string str = string.Empty;
				manager.GetObjectList<Issue>(new NameValueCollection());
				foreach (var item in manager.GetObjectList<Issue>(new NameValueCollection()))
				{
					str += item.Description + $"{Environment.NewLine}";
				}
				string task_list = string.Empty;

				MessageBox.Show(manager.ToString() + $"{Environment.NewLine}" + str);

			}
			catch (System.Exception ex)
			{

				MessageBox.Show("The following error occurred: " + ex.Message); throw;
			}
		}


	}
}

