using System;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace RedmineOutlookAddIn
{

	public partial class ThisAddIn
	{





		internal Object selObject = null;
		internal Outlook.Explorer currentExplorer = null;
		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{

			currentExplorer = this.Application.ActiveExplorer();
			currentExplorer.SelectionChange += new Outlook
				.ExplorerEvents_10_SelectionChangeEventHandler
				(CurrentExplorer_Event);


		}
		/// <summary>
		/// Метод, проеряющий какая в данный момент открыта папка и при нажатии на задачу открывает доп окно.
		/// </summary>
		private void CurrentExplorer_Event()
		{
			Outlook.MAPIFolder selectedFolder =
				this.Application.ActiveExplorer().CurrentFolder;


			try
			{
				if (this.Application.ActiveExplorer().Selection.Count > 0)
				{
					selObject = this.Application.ActiveExplorer().Selection[1];


					if (selObject is Outlook.TaskItem)
					{
						Outlook.TaskItem taskItem =
							(selObject as Outlook.TaskItem);

						ShowTaskOrShowParentTaskForm showTask = new ShowTaskOrShowParentTaskForm();
						showTask.Show();


					}

				}

			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}



		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{

			// Note: Outlook no longer raises this event. If you have code that 
			//    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
		}


		#region "Outlook07 Menu"









		#endregion
		#region VSTO generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(ThisAddIn_Startup);
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}
		#endregion








	}
}

