
using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace RedmineOutlookAddIn
{
	public partial class AddOrChangeProjectWindow : Form
	{
		
		/// <summary>
		/// Метод - создавет уникальный идентификатор проекта.
		/// </summary>
		/// <returns>уникальный идентификатор</returns>
		internal string RandomIdetifier()
		{
			string res = "otlkid";
			Random rnd = new Random();
			int n = rnd.Next(4, 7);
			for (int i = 0; i < n; i++)
			{
				res += (char)rnd.Next('a', 'z');
			}
			return res;
		}
		internal  List<Project> projects = (List<Project>)Globals.Ribbons.MyRibbon.manager.GetObjectList<Project>(new NameValueCollection());

		
		public AddOrChangeProjectWindow()
		{
			InitializeComponent();
			
			for (int i = 0; i < projects.Count; i++)
			{
				comboBoxProjects.Items.Add(projects[i].Name);
			}
			comboBoxProjects.Items.Add("Redmine");
		}
		
		/// <summary>
		/// Изменить проект
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonChangeProject_Click(object sender, EventArgs e)
		{
			RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder=comboBoxProjects.SelectedItem.ToString();
			RedmineOutlookAddIn.Properties.Settings.Default.Save();
			MessageBox.Show("Текущий проект изменен" + Environment.NewLine + $"{RedmineOutlookAddIn.Properties.Settings.Default.CurrentFolder}");
		}

		
	}
}
