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


using System.IO;
using System.Threading;
using GemBox.Document;

using System.Diagnostics;


//https://social.msdn.microsoft.com/Forums/office/en-US/4fa50649-db5c-4d2c-8ef0-5c08635c28e4/creating-an-outlook-task-in-net?forum=outlookdev
//https://docs.microsoft.com/en-us/visualstudio/vsto/how-to-programmatically-create-a-custom-calendar?view=vs-2019

namespace RedmineOutlookAddIn
{
	public partial class MyRibbon
	{
		internal static string host = RedmineOutlookAddIn.Properties.Settings.Default.host;//"https://edu.pais.hse.ru/redmine/";//"http://79.137.216.214/redmine/";
		internal static string apiKey = RedmineOutlookAddIn.Properties.Settings.Default.apyKey;// "150dfb2182cab1f02cac7aff2da8224207632e67";//"4444025d7e83c49e92466b5399ba7ee06c464637";
		internal RedmineManager manager;
		internal static Outlook.MAPIFolder CustomFolder = null;
		internal string current_folder = RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder;//папка сохранения задач
		internal string outlookTaskParent;

		private void MyRibbon_Load(object sender, RibbonUIEventArgs e)
		{
			try
			{
				manager = new RedmineManager(Properties.Settings.Default.host, Properties.Settings.Default.apyKey);

			}
			catch (System.Exception ex)
			{

				MessageBox.Show(ex.Message);
				MessageBox.Show("Чтобы пользоваться надстройкой" + Environment.NewLine + "Введите пользовательские данные");
			}






		}

		/// <summary>
		/// Метод, создающий словарь из имени и EntireId текущей папки.
		/// </summary>
		/// <returns>словарь с именем и уникальным Id задачи </returns>
		internal Dictionary<string, string> GetTasksFromCurrentFolder()
		{
			Dictionary<string, string> OutlookTasks = new Dictionary<string, string>();

			Outlook.Application outlookApp = new Outlook.Application();
			Outlook.Items tasks = null;

			tasks = CustomFolder.Items;
			foreach (Outlook.TaskItem task in tasks)
			{
				if (OutlookTasks.Keys.Contains<string>(task.Subject))
					continue;
				OutlookTasks.Add(task.Subject, task.EntryID);
			}
			return OutlookTasks;
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
				manager = new RedmineManager(RedmineOutlookAddIn.Properties.Settings.Default.host, RedmineOutlookAddIn.Properties.Settings.Default.apyKey);
				AddTask();

			}
			catch (System.Exception ex)
			{
				//To Do 
				//Перед демонстрацией будь добра - это закомментить (не могу понять почему она вылетает)
				MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
			}

		}


		public void AddTask()
		{


			Outlook.TaskItem newTaskItem = null;
			//Проверяем текущую папку CustomFolder
			if (ChekCustomFolderExists())
			{
				//Создаем задачу в этой папке.
				newTaskItem =
				(Outlook.TaskItem)CustomFolder.Items.Add(Outlook.OlItemType.olTaskItem);

				newTaskItem.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
				newTaskItem.UserProperties.Add("Project", OlUserPropertyType.olText, true, true);
				newTaskItem.UserProperties["Project"].Value = CustomFolder.Name;
				newTaskItem.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
				newTaskItem.UserProperties["Parent"].Value = outlookTaskParent;


				newTaskItem.Display("True");


				if (newTaskItem.Subject != null || newTaskItem.DueDate != null)
				{
					Outlook.MAPIFolder calendar = CreateCustomCalendar();
					CreateOutlookTaskAppointment(calendar, newTaskItem.Subject, newTaskItem.Body, newTaskItem.StartDate, newTaskItem.DueDate);
					//Создаем  задачу в Redmine
					CreateNewTaskRedmine(newTaskItem);
					newTaskItem.Save();
					//Добавляю в папку с текущем проектом
					CustomFolder.Items.Add(newTaskItem);

				}
				else
				{
					MessageBox.Show($"Отсутствует описание или дата! {Environment.NewLine} Задача не была добавлена");
				}




			}
			else
			{
				CustomFolder = CreateCustomFolder(RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder);
				newTaskItem =
				(Outlook.TaskItem)CustomFolder.Items.Add(Outlook.OlItemType.olTaskItem);



				newTaskItem.Display("True");
				if (newTaskItem.Subject != null)
				{
					//Создаем  задачу в Redmine
					CreateNewTaskRedmine(newTaskItem);
					newTaskItem.Save();
					//Добавляю в папку с текущем проектом
					CustomFolder.Items.Add(newTaskItem);
				}
				else
				{
					MessageBox.Show($"Отсутствует описание! {Environment.NewLine} Задача не была добавлена");
				}

			}
			Marshal.ReleaseComObject(newTaskItem);


		}
		/// <summary>
		/// Метод ,создающий задачу в Redmine.
		/// </summary>
		/// <param name="newTaskItem"></param>
		private void CreateNewTaskRedmine(TaskItem newTaskItem)
		{
			//To Do
			//Здесь нужно будет сделать выбор текущего проекта в Redmine
			int id = 1;
			foreach (Project proj in manager.GetObjectList<Project>(new NameValueCollection()))
			{
				if (RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder == "Redmine")
				{
					id = 1;
					break;
				}
				if (proj.Name == RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder)
				{
					id = proj.Id;
					break;

				}
			}

			var newIssue = new Issue
			{
				Subject = newTaskItem.Subject,
				//по умолчанию добавляется в первый проект,он будет текущим(их может быть несколько и у каждого есть свой номер)
				Project = new IdentifiableName { Id = id },
				//Tracker = new IdentifiableName { Name = "Bug" },
				//Status = new IdentifiableName { Name = "New" },
				//Priority = new IdentifiableName { Name = "Normal" },
				Description = newTaskItem.Body,
				StartDate = newTaskItem.StartDate

			};
			if (newTaskItem.DueDate != null)
			{
				newIssue.DueDate = newTaskItem.DueDate;
			}
			MessageBox.Show($"Задача создана ***{newTaskItem.Subject}***{Environment.NewLine} в папке ***{CustomFolder.Name}*** ");
			//Добавляем такску в Redmine
			manager.CreateObject(newIssue);

		}

		[DebuggerStepThrough]
		/// <summary>
		/// Метод проверяющий - существует ли папка или нет.
		/// </summary>
		/// <returns></returns>
		public bool ChekCustomFolderExists()
		{

			Outlook.MAPIFolder fldContacts = (Outlook.MAPIFolder)Globals.ThisAddIn.Application.Session.
				GetDefaultFolder(Outlook.OlDefaultFolders.olFolderTasks);
			//Проверяем есть ли пользовательская папка
			//(которая сейчас текущая[текущий проект Redmine - который можно задовать в ленте] CurrentFolder ) в аутлуке
			foreach (Outlook.MAPIFolder subFolder in fldContacts.Folders)
			{
				//если да ,то у нас есть объект этой папки
				if (subFolder.Name == RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder)
				{
					CustomFolder = subFolder;
					return true;
				}

			}
			return false;


		}

		[DebuggerStepThrough]
		/// <summary>
		/// Метод , создающий папку - если она отсутствует
		/// </summary>
		/// <returns>папку Outlook</returns>
		public Outlook.MAPIFolder CreateCustomFolder(string nameFolder)
		{
			Outlook.MAPIFolder CustomFolder = null;


			Outlook.MAPIFolder taskFolder = (Outlook.MAPIFolder)
			Globals.ThisAddIn.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderTasks);


			//  проверка папки в outlook


			foreach (Outlook.MAPIFolder subFolder in taskFolder.Folders)
			{
				if (subFolder.Name == nameFolder)

				{
					CustomFolder = subFolder;
					return CustomFolder;
				}
			}


			//если не сущестует ,то создать новую


			if (CustomFolder == null)
			{
				CustomFolder = taskFolder.Folders.Add(nameFolder, OlDefaultFolders.olFolderTasks);
			}
			return CustomFolder;

		}

		/// <summary>
		/// Событие ,которое записывает данные в переменные среды
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button3_Click(object sender, RibbonControlEventArgs e)
		{


			Connection connection = new Connection();
			connection.Show();

			try
			{
				if (connection.checkBox2.Checked)
				{
					RedmineOutlookAddIn.Properties.Settings.Default.host = connection.textBoxUrl.Text;
					RedmineOutlookAddIn.Properties.Settings.Default.apyKey = connection.textBoxApiKey.Text;
					//При авторизации по умолчанию задачи будут создаваться в папке Redmine
					RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder = "Redmine";
					RedmineOutlookAddIn.Properties.Settings.Default.Save();
					manager = new RedmineManager(RedmineOutlookAddIn.Properties.Settings.Default.host, RedmineOutlookAddIn.Properties.Settings.Default.apyKey);
					this.RibbonUI.Invalidate();
				}
				manager = new RedmineManager(RedmineOutlookAddIn.Properties.Settings.Default.host, RedmineOutlookAddIn.Properties.Settings.Default.apyKey);

			}
			catch (System.Exception)
			{
				MessageBox.Show("Проблемы с авторизацией" + Environment.NewLine + "Попробуйте ввести данные снова");
			}


		}



		/// <summary>
		/// Кнопка обновления задачи .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button6_Click(object sender, RibbonControlEventArgs e)
		{
			TasksUpdates();
		}
		private void TasksUpdates()
		{
			//прогресс бар процесса обновления задач
			ProgressBarShow progress = new ProgressBarShow();


			Outlook.Items tasks = null;
			Outlook.Application oApp = new Outlook.Application();

			//выбор папки
			if (CustomFolder == null)
			{
				CustomFolder = CreateCustomFolder(RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder);

			}
			tasks = CustomFolder.Items;

			bool isExist;

			int pr = 0;
			//максимальная вместимость прогресс бара
			progress.progressBar1.Maximum = manager.GetObjectList<Issue>(new NameValueCollection()).Count;
			progress.Show();

			foreach (Issue issue in manager.GetObjectList<Issue>(new NameValueCollection()))
			{
				isExist = false;

				try
				{

					//заменили или создали папку текущего проекта
					if (CustomFolder.Name != issue.Project.Name)
					{
						CustomFolder = CreateCustomFolder(issue.Project.Name);
						tasks = CustomFolder.Items;
					}

					//проходим по папке аутлука

					foreach (Outlook.TaskItem task in tasks)
					{

						if (task.Subject == issue.Subject)
						{
							isExist = true;
							if ((DateTime)issue.UpdatedOn > task.LastModificationTime)
							{

								UpdateOneTaskOutlook(task, issue);

								break;
							}
							else
							{

								UpdateOneTaskRedmine(task, issue);
								break;
							}

						}
						else
						{
							//если такой задачи нет 
							continue;
						}

					}
					if (!isExist)
					{
						if (CustomFolder.Name != issue.Project.Name)
						{
							CustomFolder = CreateCustomFolder(issue.Project.Name);
						}
						CreateNewTaskOutlook(issue);
					}


					if (progress.progressBar1.Value < progress.progressBar1.Maximum)
					{
						pr += 1;
						progress.progressBar1.Value = pr;

					}



				}
				catch (System.Exception ex)
				{

					MessageBox.Show($"Что-то пошло не так.{Environment.NewLine}Пожалуйста проверьте хост и ключ {Environment.NewLine}{ex.Message}");
				}


			}
			Thread.Sleep(150);
			progress.Close();
			MessageBox.Show("Задачи импортированы или обновлены");

			Marshal.ReleaseComObject(tasks);
			Marshal.ReleaseComObject(oApp);


		}

		/// <summary>
		/// Метод - создает задачу в Outlook при обновлении
		/// </summary>
		/// <param name="issue"></param>
		private Outlook.TaskItem CreateNewTaskOutlook(Issue issue)
		{
			Outlook.TaskItem task = null;
			Dictionary<string, string> OutlookTasks = GetTasksFromCurrentFolder();
			//To Do чтобы сохраняла в текущий фолдер !!!!!
			task = (Outlook.TaskItem)CustomFolder.Items.Add(Outlook.OlItemType.olTaskItem);
			task.Subject = issue.Subject;
			Issue parentRedmine = null;
			bool flagEistParentInOutlook = false;
			if (issue.ParentIssue != null)
			{
				parentRedmine = manager.GetObject<Issue>(issue.ParentIssue.Id.ToString(), new NameValueCollection());
				foreach (var taskID in OutlookTasks)
				{
					//если есть такая задача в текущей папке, то просто в свойстве добавляем ее ид
					if (taskID.Key == parentRedmine.Subject)
					{
						outlookTaskParent = taskID.Value;
						task.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
						task.UserProperties["Parent"].Value = outlookTaskParent;
						//MessageBox.Show($"В задачу ***{task.Subject}*** добавлен родитель ***{parentRedmine.Subject}***");
						flagEistParentInOutlook = true;
					}

				}
				//если нет то создаем ее в текущей папке и добавляем ее ид в свойства
				if (!flagEistParentInOutlook)
				{
					Outlook.TaskItem parentItemOutlook = CreateNewTaskOutlook(parentRedmine);
					outlookTaskParent = parentItemOutlook.EntryID;
					task.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
					task.UserProperties["Parent"].Value = outlookTaskParent;
					task.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
					task.UserProperties["Parent"].Value = outlookTaskParent;
					//MessageBox.Show($"В задачу ***{task.Subject}*** создан и добавлен ***{parentItemOutlook.Subject}***");
				}


			}
			if (issue.Children != null)
			{
				//MessageBox.Show("ребенок есть");
				Issue childrenRedmine = null;
				flagEistParentInOutlook = false;

				childrenRedmine = manager.GetObject<Issue>(issue.Children.ToString(), new NameValueCollection());
				foreach (var taskID in OutlookTasks)
				{
					//если есть такая задача в текущей папке, то просто в свойстве добавляем ее ид
					if (taskID.Key == childrenRedmine.Subject)
					{
						Outlook.NameSpace ns = Globals.ThisAddIn.Application.Session;


						Outlook.TaskItem chiledrenOutlook = (Outlook.TaskItem)ns.GetItemFromID(taskID.Value);
						outlookTaskParent = task.EntryID;
						chiledrenOutlook.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
						chiledrenOutlook.UserProperties["Parent"].Value = outlookTaskParent;
						//MessageBox.Show($"В задачу ***{chiledrenOutlook.Subject}*** добавлен родитель ***{task.Subject}***");
						flagEistParentInOutlook = true;
					}

				}
				//если нет то создаем ее в текущей папке и добавляем ее ид в свойства
				if (!flagEistParentInOutlook)
				{
					Outlook.TaskItem childernItemOutlook = CreateNewTaskOutlook(childrenRedmine);
					outlookTaskParent = task.EntryID;
					childernItemOutlook.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
					childernItemOutlook.UserProperties["Parent"].Value = outlookTaskParent;
					childernItemOutlook.UserProperties.Add("InCalendar", OlUserPropertyType.olText, false, true);
					childernItemOutlook.UserProperties["InCalendar"].Value = "false";
					//MessageBox.Show($"В задачу ***{childernItemOutlook.Subject}*** создан и добавлен ***{task.Subject}***");
				}

			}
			task.StartDate = (DateTime)issue.StartDate;
			if (issue.DueDate != null)
			{
				task.DueDate = (DateTime)issue.DueDate;
			}
			task.StartDate = (DateTime)issue.StartDate;
			task.PercentComplete = (int)issue.DoneRatio;
			task.Body = issue.Description;

			task.Save();

			return task;
		}
		/// <summary>
		/// Метод  обновляет задачу в Redmine при обновлении.
		/// </summary>
		/// <param name="task">задача Outlook</param>
		/// <param name="issue">задача Redmine</param>
		private void UpdateOneTaskRedmine(TaskItem task, Issue issue)
		{

			issue.DoneRatio = task.PercentComplete;
			issue.Description = task.Body;
			issue.StartDate = task.StartDate;
			if (task.DueDate != null)
			{
				issue.DueDate = task.DueDate;
			}


		}
		/// <summary>
		/// Метод - обновляет задачу в Outlook
		/// </summary>
		/// <param name="task">задача Outlook</param>
		/// <param name="issue">задача Redmine</param>
		private void UpdateOneTaskOutlook(TaskItem task, Issue issue)
		{
			Dictionary<string, string> OutlookTasks = GetTasksFromCurrentFolder();
			//To Do чтобы сохраняла в текущий фолдер !!!!!
			task = (Outlook.TaskItem)CustomFolder.Items.Add(Outlook.OlItemType.olTaskItem);
			task.Subject = issue.Subject;
			Issue parentRedmine = null;
			bool flagEistParentInOutlook = false;
			if (issue.ParentIssue != null)
			{
				parentRedmine = manager.GetObject<Issue>(issue.ParentIssue.Id.ToString(), new NameValueCollection());
				foreach (var taskID in OutlookTasks)
				{
					//если есть такая задача в текущей папке, то просто в свойстве добавляем ее ид
					if (taskID.Key == parentRedmine.Subject)
					{
						outlookTaskParent = taskID.Value;
						task.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
						task.UserProperties["Parent"].Value = outlookTaskParent;
						//MessageBox.Show($"В задачу ***{task.Subject}*** добавлен родитель ***{parentRedmine.Subject}***");
						flagEistParentInOutlook = true;
					}

				}
				//если нет то создаем ее в текущей папке и добавляем ее ид в свойства
				if (!flagEistParentInOutlook)
				{
					Outlook.TaskItem parentItemOutlook = CreateNewTaskOutlook(parentRedmine);
					outlookTaskParent = parentItemOutlook.EntryID;
					task.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
					task.UserProperties["Parent"].Value = outlookTaskParent;
					task.UserProperties.Add("InCalendar", OlUserPropertyType.olText, false, true);
					task.UserProperties["InCalendar"].Value = "false";
					//MessageBox.Show($"В задачу ***{task.Subject}*** создан и добавлен ***{parentItemOutlook.Subject}***");
				}


			}
			if (issue.Children != null)
			{

				Issue childrenRedmine = null;
				flagEistParentInOutlook = false;

				childrenRedmine = manager.GetObject<Issue>(issue.Children.ToString(), new NameValueCollection());
				foreach (var taskID in OutlookTasks)
				{
					//если есть такая задача в текущей папке, то просто в свойстве добавляем ее ид
					if (taskID.Key == childrenRedmine.Subject)
					{
						Outlook.NameSpace ns = Globals.ThisAddIn.Application.Session;


						Outlook.TaskItem chiledrenOutlook = (Outlook.TaskItem)ns.GetItemFromID(taskID.Value);
						outlookTaskParent = task.EntryID;
						chiledrenOutlook.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
						chiledrenOutlook.UserProperties["Parent"].Value = outlookTaskParent;
						//MessageBox.Show($"В задачу ***{chiledrenOutlook.Subject}*** добавлен родитель ***{task.Subject}***");
						flagEistParentInOutlook = true;
					}

				}
				//если нет то создаем ее в текущей папке и добавляем ее ид в свойства
				if (!flagEistParentInOutlook)
				{
					Outlook.TaskItem childernItemOutlook = CreateNewTaskOutlook(childrenRedmine);
					outlookTaskParent = task.EntryID;
					childernItemOutlook.UserProperties.Add("Parent", OlUserPropertyType.olText, false, true);
					childernItemOutlook.UserProperties.Add("InCalendar", OlUserPropertyType.olText, false, true);
					childernItemOutlook.UserProperties["InCalendar"].Value = "false";
					childernItemOutlook.UserProperties["Parent"].Value = outlookTaskParent;
					//MessageBox.Show($"В задачу ***{childernItemOutlook.Subject}*** создан и добавлен ***{task.Subject}***");
				}

			}
			if (issue.DueDate != null)
			{
				task.DueDate = (DateTime)issue.DueDate;
			}
			task.StartDate = (DateTime)issue.StartDate;
			task.PercentComplete = (int)issue.DoneRatio;
			task.Body = issue.Description;
			task.ReminderSet = true;
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
				tmpRedm += task.Subject + Environment.NewLine;

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
			MessageBox.Show("TASK outlook" + Environment.NewLine + tmpRedm);
			MessageBox.Show("TASK Redmine" + Environment.NewLine + temp);

		}

		private void buttonUser_Click(object sender, RibbonControlEventArgs e)
		{
			this.RibbonUI.Invalidate();
			try
			{
				MessageBox.Show(RedmineOutlookAddIn.Properties.Settings.Default.host.ToString());
			}
			catch (System.Exception ex)
			{

				MessageBox.Show(ex.Message + Environment.NewLine + "Авторезуйтесь");
			}

		}

		private void buttonChangeOrCreateProject_Click(object sender, RibbonControlEventArgs e)
		{
			AddOrChangeProjectWindow window = new AddOrChangeProjectWindow();
			window.Show();
		}

		private void buttonCurretProject_Click(object sender, RibbonControlEventArgs e)
		{
			MessageBox.Show(Properties.Settings.Default.CurrentFolder);

		}

		private void buttonCalendar_Click(object sender, RibbonControlEventArgs e)
		{

		}

		private void button5_Click(object sender, RibbonControlEventArgs e)
		{
			HelperWindow helperWindow = new HelperWindow();
			helperWindow.Show();
		}

		private void button4_Click(object sender, RibbonControlEventArgs e)
		{
			AboutAddInProject aboutAdd = new AboutAddInProject();
			aboutAdd.Show();
		}
		/// <summary>
		/// Метод ,который добавляет трекеры задач 
		/// в календарь(если они не были добавлены).
		/// Отображает календарь.(когда все задачи добавлены)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCalendR_Click(object sender, RibbonControlEventArgs e)
		{

			try
			{
				Outlook.MAPIFolder fldContacts = (Outlook.MAPIFolder)Globals.ThisAddIn.Application.Session.
				GetDefaultFolder(Outlook.OlDefaultFolders.olFolderTasks);
				//Проверяем есть ли пользовательская папка
				//(которая сейчас текущая[текущий проект Redmine - который можно задовать в ленте] CurrentFolder ) в аутлуке
				Outlook.MAPIFolder my_currentFolder = CreateCustomCalendar();
				ProgressBarShow progress = new ProgressBarShow();
				progress.progressBar1.Maximum = manager.GetObjectList<Issue>(new NameValueCollection()).Count;
				int pr = 0;
				progress.Show();

				foreach (Outlook.MAPIFolder subFolder in fldContacts.Folders)
				{
					CustomFolder = subFolder;
					foreach (var task in CustomFolder.Items)
					{
						Outlook.TaskItem taskItem = (Outlook.TaskItem)task;


						if (taskItem.DueDate == null)
						{
							taskItem.DueDate = DateTime.Today;
						}
						if (!CheckTaskInOutlookCalendary(my_currentFolder, taskItem.Subject))
						{
							CreateOutlookTaskAppointment(my_currentFolder, taskItem.Subject, taskItem.Body, taskItem.StartDate, taskItem.DueDate);
						}
						if (progress.progressBar1.Value < progress.progressBar1.Maximum)
						{
							pr += 1;
							progress.progressBar1.Value = pr;
						}

					}

				}

				MessageBox.Show("Календарь импортирован");
			}
			catch (System.Exception ex)
			{
				//MessageBox.Show(ex.Message);
				MessageBox.Show($"Задачи, у которых не было даты "+Environment.NewLine+"начала и окончания , импортированы не были!");
			}

		}
		/// <summary>
		/// Метод,проверяющий есть ли задача в календаре.
		/// </summary>
		/// <param name="my_personalCalendary"></param>
		/// <param name="taskSubject"></param>
		/// <returns></returns>
		private bool CheckTaskInOutlookCalendary(Outlook.MAPIFolder my_personalCalendary, string taskSubject)
		{
			bool isExist = false;
			foreach (var item in my_personalCalendary.Items)
			{
				if (((Outlook.AppointmentItem)item).Subject == taskSubject)
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Метод ,создающий новый ивент в календаре.
		/// </summary>
		/// <param name="my_personalCalendary">календарь</param>
		/// <param name="taskSubject">тема задачи</param>
		/// <param name="taskBody">описание задачи</param>
		/// <param name="start">начало задачи</param>
		/// <param name="finish">конец задачи</param>
		private void CreateOutlookTaskAppointment(Outlook.MAPIFolder my_personalCalendary, string taskSubject, string taskBody, DateTime start, DateTime finish)
		{

			try
			{
				Outlook.AppointmentItem newEvent =
					my_personalCalendary.Items.Add
					(Outlook.OlItemType.olAppointmentItem)
					as Outlook.AppointmentItem;
				newEvent.Start = start;
				newEvent.End = finish;
				newEvent.Subject = taskSubject;
				newEvent.Body = taskBody;
				newEvent.Save();
				//MessageBox.Show($"Задача***{newEvent.Subject}*** добавлена в календарь! **{my_personalCalendary.Name}**");

			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
				//ex.HandleException();

			}

		}
		/// <summary>
		/// Метод ,создающий объект текущего календаря.
		/// </summary>
		/// <returns>обьект календаря</returns>
		private Outlook.MAPIFolder CreateCustomCalendar()
		{
			string newCalendarName = RedmineOutlookAddIn.Properties.Settings.Default.CurrentCalendar + $"{RedmineOutlookAddIn.Properties.Settings.Default.host}";
			Outlook.MAPIFolder primaryCalendar = (Outlook.MAPIFolder)Globals.ThisAddIn.Application.ActiveExplorer().Session.GetDefaultFolder
				 (Outlook.OlDefaultFolders.olFolderCalendar);
			bool needFolder = true;
			//ищем данный календарь 
			Outlook.MAPIFolder my_personalCalendary = null;
			foreach (Outlook.MAPIFolder personalCalendar
				in primaryCalendar.Folders)
			{
				if (personalCalendar.Name == newCalendarName)
				{
					my_personalCalendary = personalCalendar;
					needFolder = false;
					break;
				}
			}
			//если такой папки нет,то создаем
			if (needFolder)
			{
				my_personalCalendary = primaryCalendar
			.Folders.Add(newCalendarName,
				Outlook.OlDefaultFolders.olFolderCalendar);
			}
			return my_personalCalendary;


		}
		/// <summary>
		/// Метод ,окрывающий календарь текущего пользователя Redmine.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOpenCalendary_Click(object sender, RibbonControlEventArgs e)
		{
			try
			{
				Outlook.MAPIFolder my_currentFolder = CreateCustomCalendar();
				Globals.ThisAddIn.Application.ActiveExplorer().CurrentFolder = my_currentFolder;
				Globals.ThisAddIn.Application.ActiveExplorer().CurrentFolder.Display();
			}
			catch (System.Exception ex)
			{

				MessageBox.Show("Невозможно открыть календарь" + Environment.NewLine + ex.Message);
			}
		}
		/// <summary>
		/// Метод, показывающий текущую папку с календарем.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonShowCurrentCalendar_Click(object sender, RibbonControlEventArgs e)
		{
			MessageBox.Show(Properties.Settings.Default.CurrentCalendar + Properties.Settings.Default.host);
		}

		private void buttonCreateNewCalendarOrChange_Click(object sender, RibbonControlEventArgs e)
		{
			try
			{
				CreateNewCalendarWindow calendarWindow = new CreateNewCalendarWindow();
				calendarWindow.Show();
			}
			catch (System.Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
		}
	}
}
