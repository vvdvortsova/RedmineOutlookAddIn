using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;

namespace RedmineOutlookAddIn
{
	public partial class ShowTaskOrShowParentTaskForm : Form
	{

		public ShowTaskOrShowParentTaskForm()
		{

			InitializeComponent();
		}
		/// <summary>
		/// Метод, отображающий задачу для редактирования
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOpenTask_Click(object sender, EventArgs e)
		{

			if (Globals.ThisAddIn.selObject is Outlook.TaskItem)
			{
				Outlook.TaskItem taskItem =
					(Globals.ThisAddIn.selObject as Outlook.TaskItem);
				taskItem.Display();

			}
		}
		/// <summary>
		/// Метод ,отображающий Parent задачи ,если он есть.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonShowParent_Click(object sender, EventArgs e)
		{
			//срабатывает когда текущий контрол ,какая -нибудь таска
			if (Globals.ThisAddIn.selObject is Outlook.TaskItem)
			{
				Outlook.TaskItem taskItem =
					(Globals.ThisAddIn.selObject as Outlook.TaskItem);
				//проверяем есть ли у таски родитель
				if (taskItem.UserProperties["Parent"] != null)
				{
					Outlook.NameSpace ns = Globals.ThisAddIn.Application.Session;

					Outlook.TaskItem theitem = (Outlook.TaskItem)ns.GetItemFromID(taskItem.UserProperties["Parent"].Value.ToString());
					//получаем текущий испектор таски
					Outlook.Inspector inspector = theitem.GetInspector;
					if (inspector != null)
					{
						inspector.Display();
						Marshal.ReleaseComObject(inspector);
						inspector = null;
					}
					Marshal.ReleaseComObject(theitem); theitem = null;
					Marshal.ReleaseComObject(ns); ns = null;
				}
				else
				{
					MessageBox.Show("Parent отсутсвует или удален");
				}
			}

		}



	}
}
