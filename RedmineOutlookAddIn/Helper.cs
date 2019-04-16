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

namespace RedmineOutlookAddIn
{
	public class Helper
	{
		public void AddTask(RedmineManager manager,RedmineOutlookAddIn.ThisAddIn thisAddIn)
		{
			try
			{

				Outlook.TaskItem newTaskItem =
					(Outlook.TaskItem)thisAddIn.Application.CreateItem(Outlook.OlItemType.olTaskItem);
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
				
				//string str = string.Empty;
				//manager.GetObjectList<Issue>(new NameValueCollection());
				//foreach (var item in manager.GetObjectList<Issue>(new NameValueCollection()))
				//{
				//	str += item.Description + $"{Environment.NewLine}";
				//}
				//string task_list = string.Empty;

				//MessageBox.Show(manager.ToString() + $"{Environment.NewLine}" + str);

			}
			catch (System.Exception ex)
			{

				MessageBox.Show("The following error occurred: " + ex.Message); throw;
			}


		}
	}
}
