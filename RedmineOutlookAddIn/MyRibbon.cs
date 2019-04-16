using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.ComponentModel;

using MsoTDAddinLib;
using System.AddIn;
using Microsoft.Office.Interop.Outlook;
using System.Data;

using System;
using GemBox.Email;
using GemBox.Email.Calendar;
using System.IO;
//https://social.msdn.microsoft.com/Forums/office/en-US/4fa50649-db5c-4d2c-8ef0-5c08635c28e4/creating-an-outlook-task-in-net?forum=outlookdev
//https://docs.microsoft.com/en-us/visualstudio/vsto/how-to-programmatically-create-a-custom-calendar?view=vs-2019

namespace RedmineOutlookAddIn
{
	public partial class MyRibbon
	{
		public static string host = "http://79.137.216.214/redmine/";
		public static string apiKey = "4444025d7e83c49e92466b5399ba7ee06c464637";
		public static RedmineManager manager;// new RedmineManager(host, apiKey);
		//public Connection connection = new Connection();

		private void MyRibbon_Load(object sender, RibbonUIEventArgs e)
		{
			try
			{
				isExistUser();
				this.RibbonUI.Invalidate();

			}
			catch (System.Exception ex)
			{

				MessageBox.Show(ex.Message);
				MessageBox.Show("Чтобы пользоваться надстройкой" + Environment.NewLine + "Введите пользовательские данные");
			}
			

			//if(host==string.Empty||apiKey== string.Empty)
			//{
			//	MessageBox.Show("Чтобы пользоваться надстройкой" + Environment.NewLine + "Введите пользовательские данные");
			//}
		}

		internal bool isExistUser()
		{
			using (StreamReader sr = new StreamReader("../../Mypassword.txt"))
			{
				host = sr.ReadLine();
				apiKey = sr.ReadLine();
				
			}
			//host = RedmineOutlookAddIn.Properties.Settings.Default.host;
			//apiKey = RedmineOutlookAddIn.Properties.Settings.Default.apyKey;
			try
			{
				manager = new RedmineManager(host, apiKey);
				return true;
			}
			catch (System.Exception)
			{

				MessageBox.Show("Неправильные пользовательские данные!");
			}
			return false;
		}
		/// <summary>
		/// Метод позволяющий добавлять задачи в Redmine.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, RibbonControlEventArgs e)
		{
			try
			{
				if (!isExistUser())
				{
					throw new RedmineException("Авторизуйтесь");
				}
				AddTask();
				//AddTaskToCalendar();
			}
			catch (System.Exception ex )
			{
				MessageBox.Show(ex.Message);
			}
			
		}
		 private void AddTaskToCalendar()
		{
			// Create new calendar.
			Calendar calendar = new Calendar();

			// Create new task.
			Task task = new Task();
			task.Organizer = new MailAddress("info@gemboxsoftware.com", "GemBox d.o.o.");
			task.Name = "Implement iCalendar Support";
			task.Start = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			task.Deadline = new DateTime(2018, 1, 15, 0, 0, 0, DateTimeKind.Utc);
			task.Priority = 5;
			task.Categories.Add("New '.ics' format support");
			task.Attendees.Add(new MailAddress("programmer@example.com"));

			// Add task to the calendar.
			calendar.Tasks.Add(task);

			calendar.Save("Create And Save.ics");
			

		}

		public void AddTask()
		{
			try
			{

				Outlook.TaskItem newTaskItem =
					(Outlook.TaskItem)Globals.ThisAddIn.Application.CreateItem(Outlook.OlItemType.olTaskItem);
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

				MessageBox.Show("The following error occurred: " + ex.Message); 
			}
		}

		private void button3_Click(object sender, RibbonControlEventArgs e)
		{


			Connection connection = new Connection();
			connection.Show();
			isExistUser();
			this.RibbonUI.Invalidate();







		}

		private void button2_Click(object sender, RibbonControlEventArgs e)
		{
			CreateCustomCalendar();
		}
		private void AddAppointment()
		{
			try
			{
				Outlook.AppointmentItem newAppointment =
					(Outlook.AppointmentItem)
				Globals.ThisAddIn.Application.CreateItem(Outlook.OlItemType.olAppointmentItem);
				newAppointment.Display();
				newAppointment.Start = DateTime.Now.AddHours(2);
				newAppointment.End = DateTime.Now.AddHours(3);
				newAppointment.Location = "ConferenceRoom #2345";
				newAppointment.Body =
					"We will discuss progress on the group project.";
				newAppointment.AllDayEvent = false;
				newAppointment.Subject = "Group Project";
				newAppointment.Recipients.Add("Roger Harui");
				Outlook.Recipients sentTo = newAppointment.Recipients;
				Outlook.Recipient sentInvite = null;
				sentInvite = sentTo.Add("Holly Holt");
				sentInvite.Type = (int)Outlook.OlMeetingRecipientType
					.olRequired;
				sentInvite = sentTo.Add("David Junca ");
				sentInvite.Type = (int)Outlook.OlMeetingRecipientType
					.olOptional;
				sentTo.ResolveAll();
				newAppointment.Save();
				newAppointment.Display(true);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("The following error occurred: " + ex.Message);
			}
		}

		private void CreateCustomCalendar()
		{
			const string newCalendarName = "PersonalCalendar";
			Outlook.MAPIFolder primaryCalendar = (Outlook.MAPIFolder)
				Globals.ThisAddIn.Application.ActiveExplorer().Session.GetDefaultFolder
				 (Outlook.OlDefaultFolders.olFolderCalendar);
			//primaryCalendar.Display();

			
			
				
				
				

				Outlook.TaskItem newTaskItem = primaryCalendar.Items.Add
					(Outlook.OlItemType.olTaskItem)
					as Outlook.TaskItem;
				newTaskItem.Subject = "Into Calendar";
				newTaskItem.Save();
			primaryCalendar.Display();
			
			

		}

		private void button6_Click(object sender, RibbonControlEventArgs e)
		{
			TasksUpdates();
		}
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
				isExist = false;
				foreach (Outlook.TaskItem task in tasks)
				{
					if (task.Subject == issue.Subject)
					{
						if ((DateTime)issue.UpdatedOn > task.LastModificationTime)
						{
							UpdateOneTask(task, issue);
							

						}
						isExist = false;
						break;
					}
					else {
						isExist = true;//если такой задачи нет 

						continue; }
					
					//temp += $"{task.Body}+{Environment.NewLine}";
					//bool isCompleeted = task.Complete;//Check if your task is compleeted in your application you could use EntryID property to identify a task 
					//if (isCompleeted == true && task.Status != OlTaskStatus.olTaskComplete)
					//{
					//	task.MarkComplete();
					//	task.Save();
					//}

					
				}
				if (isExist)
				{
					Outlook.TaskItem task = null;

					task = (Outlook.TaskItem)Globals.ThisAddIn.Application.CreateItem(Outlook.OlItemType.olTaskItem);
					task.Subject = issue.Subject;
					task.StartDate = (DateTime)issue.StartDate;
					//task.DueDate =( DateTime)issue.DueDate;
					task.Body = issue.Description;
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
			task.Save();
		}

		private void button7_Click(object sender, RibbonControlEventArgs e)
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
			foreach (Outlook.TaskItem task in tasks)
			{
				tmpRedm += task.Subject+Environment.NewLine;

				//temp += $"{task.Body}+{Environment.NewLine}";
				//bool isCompleeted = task.Complete;//Check if your task is compleeted in your application you could use EntryID property to identify a task 
				//if (isCompleeted == true && task.Status != OlTaskStatus.olTaskComplete)
				//{
				//	task.MarkComplete();
				//	task.Save();
				//}


			}
			foreach (Issue issue in manager.GetObjectList<Issue>(new NameValueCollection()))
			{
				temp += issue.Subject + Environment.NewLine;
			}
				MessageBox.Show("TASK outlook"+Environment.NewLine+tmpRedm);
			MessageBox.Show("TASK Redmine" + Environment.NewLine + temp);

		}

		private void buttonUser_Click(object sender, RibbonControlEventArgs e)
		{
			try
			{
				MessageBox.Show(manager.ToString());
			}
			catch (System.Exception ex)
			{

				MessageBox.Show(ex.Message+Environment.NewLine+"Авторезуйтесь");
			}
			
		}
	}
}
