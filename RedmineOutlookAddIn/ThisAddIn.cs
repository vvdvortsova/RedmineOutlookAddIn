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
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using MsoTDAddinLib;
using System.AddIn;
using Microsoft.Office.Interop.Outlook;
using System.Data;
using Microsoft.Office.Tools.Ribbon;

namespace RedmineOutlookAddIn
{
   
	public partial class ThisAddIn
	{
		
		private Office.CommandBar _objOtherMenuBar;
		private Office.CommandBarPopup _objNewOtherMenuBar;
		private Office.CommandBar _objMenuBar;
		private Office.CommandBarPopup _objNewMenuBar;
		private Outlook.Inspectors Inspectors;
		private Office.CommandBarButton _objButtonForSom;//Кнопка в надстройке
		private Office.CommandBarButton _objButton;//Кнопка в надстройке
		private Office.CommandBarButton _objButtonRegistration;//кнопка окна регистрации
		private Office.CommandBarButton _objButtonUpdate;//по приколу
		internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1 ;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton RibButton1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		static string host = "http://79.137.216.214/redmine/";
		static string apiKey = "4444025d7e83c49e92466b5399ba7ee06c464637";
		public static  RedmineManager manager = new RedmineManager(host, apiKey);
		
		//MyRibbon myRibbon = new MyRibbon();
		//protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
		//{
		//	return new MyRibbon2();
		//}
		//First we try to get the default toolbar
		
		private string menuTag = "MyfirstPlugin";
		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{
			
			
			
			
			
			
		}
		//private void AddButtonsToMenu()
		//{
		//	////RibbonButton tempButton = Ta.Factory.CreateRibbonButton();
		//	//tempButton.Label = "Button 1";
		//	//tempButton.ControlSize =
		//	//	Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
		//	//tempButton.Description = "My Ribbon Button";
		//	//tempButton.ShowImage = true;
		//	//tempButton.ShowImage = true;
			
			

		//}

		

		private void _objButton1_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			throw new NotImplementedException();
		}

		private void MyOtherBar()
		{
			//this.ErsMyMenuBar();
			try
			{


				//_objOtherMenuBar = this.Application.ActiveExplorer().CommandBars.ActiveMenuBar;
				
				//_objNewOtherMenuBar = (Office.CommandBarPopup)
				//				 _objOtherMenuBar.Controls.Add(Office.MsoControlType.msoControlPopup
				//										, missing
				//										, missing
				//										, missing
				//										, true);

				//if (_objNewOtherMenuBar != null)
				//{


				//	_objNewOtherMenuBar.Caption = "Другое меню";
				//	_objNewOtherMenuBar.Tag = menuTag;

				//	_objButtonRegistration = (Office.CommandBarButton)_objNewOtherMenuBar.Controls.
				//	Add(Office.MsoControlType.msoControlButton, missing,
				//		missing, 1, true);
				//	_objButton = (Office.CommandBarButton)_objNewOtherMenuBar.Controls.
				//	Add(Office.MsoControlType.msoControlButton, missing,
				//		missing, 1, true);
				//	//_objButton.Picture = "redmine.ico";
				//	_objButtonUpdate = (Office.CommandBarButton)_objNewOtherMenuBar.Controls.
				//	Add(Office.MsoControlType.msoControlButton, missing,
				//		missing, 1, true);
				//	_objButtonUpdate.Caption = "Update";
				//	_objButtonUpdate.Click += new Office._CommandBarButtonEvents_ClickEventHandler(_objButtonUpdate_Click);
				//	_objButtonRegistration.Caption = "Настройки";
				//	_objButtonRegistration.Click += new Office._CommandBarButtonEvents_ClickEventHandler(_objButtonRegistration_Click);
				//	_objButton.Caption = "Добавить задачу";
				//	//Icon 
				//	_objButton.FaceId = 5000;
				//	_objButton.Tag = "ItemTag";
				//	//EventHandler
				//	_objButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(_objButton_Click);
				//	_objNewMenuBar.Visible = true;

				//}
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Error: " + ex.Message.ToString()
												   , "Error Message");
			}
		}

		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{
			// Note: Outlook no longer raises this event. If you have code that 
			//    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
		}


		#region "Outlook07 Menu"
		private void MyMenuBar()
		{
			this.ErsMyMenuBar();
			
			try
			{

				
				_objMenuBar = this.Application.ActiveExplorer().CommandBars.ActiveMenuBar;
				_objNewMenuBar = (Office.CommandBarPopup)
								 _objMenuBar.Controls.Add(Office.MsoControlType.msoControlPopup
														, missing
														, missing
														, missing
														, false);

				if (_objNewMenuBar != null)
				{


					_objNewMenuBar.Caption = "Главное меню";
					_objNewMenuBar.Tag = menuTag;
					
					_objButtonRegistration = (Office.CommandBarButton)_objNewMenuBar.Controls.
					Add(Office.MsoControlType.msoControlButton, missing,
						missing, 1, true);
					_objButton = (Office.CommandBarButton)_objNewMenuBar.Controls.
					Add(Office.MsoControlType.msoControlButton, missing,
						missing, 1, true);
					//_objButton.Picture = "redmine.ico";
					_objButtonUpdate = (Office.CommandBarButton)_objNewMenuBar.Controls.
					Add(Office.MsoControlType.msoControlButton, missing,
						missing, 1, true);
					_objButtonUpdate.Caption = "Update";
					_objButtonUpdate.Click += new Office._CommandBarButtonEvents_ClickEventHandler(_objButtonUpdate_Click);
					_objButtonRegistration.Caption = "Настройки";
					_objButtonRegistration.Click += new Office._CommandBarButtonEvents_ClickEventHandler(_objButtonRegistration_Click);
					_objButton.Caption = "Добавить задачу";
					//Icon 
					_objButton.FaceId = 5000;
					_objButton.Tag = "ItemTag";
					//EventHandler
					_objButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(_objButton_Click);
					_objNewMenuBar.Visible = true;

				}
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Error: " + ex.Message.ToString()
												   , "Error Message");
			}

		}

		private void _objButtonUpdate_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			//try
			//{
				TasksUpdates();
			//}
			//catch (System.Exception ex)
			//{

				//MessageBox.Show(ex.Message);
			//}
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

			MessageBox.Show("NewTAsk  "+AddedTastFromOutlook);
			
			



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

		#region "Event Handler"
		#region "Menu Button"
		private void _objButtonRegistration_Click(Office.CommandBarButton ctrl,ref bool cancel)
		{

			try
			{
				var conectMenu = new Connection();
				conectMenu.Show();

			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Error " + ex.Message.ToString() + $"/n{ex.StackTrace}");
			}
		}
		private void _objButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
		{
			try
			{
				AddTask();

			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Error " + ex.Message.ToString() + $"/n{ex.StackTrace}");
			}
		}


		#endregion

		#region "Remove Existing"

		private void ErsMyMenuBar()
		{
			// If the menu already exists, remove it.
			try
			{
				
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Error: " + ex.Message.ToString()
												   , "Error Message");
			}
		}
		
		 internal  void AddTask()
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

				var newIssue = new Issue { Subject = newTaskItem.Subject,
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
					str += item.Description+$"{Environment.NewLine}";
				}
				string task_list = string.Empty;
				
				MessageBox.Show(manager.ToString()+$"{Environment.NewLine}"+str);

			}
			catch (System.Exception ex)
			{

				MessageBox.Show("The following error occurred: " + ex.Message); throw;
			}
		}
		private void AddAppointment()
		{
			try
			{
				Outlook.AppointmentItem newAppointment =
					(Outlook.AppointmentItem)
				this.Application.CreateItem(Outlook.OlItemType.olAppointmentItem);
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

		#endregion

		#endregion
	}
}

