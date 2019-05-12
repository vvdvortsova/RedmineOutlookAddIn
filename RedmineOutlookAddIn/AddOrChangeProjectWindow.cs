
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System.Collections.Specialized;

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
		/// Создание нового проекта
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCreateProject_Click(object sender, EventArgs e)
		{
			Globals.Ribbons.MyRibbon.manager = new RedmineManager(RedmineOutlookAddIn.Properties.Settings.Default.host, RedmineOutlookAddIn.Properties.Settings.Default.apyKey);
			if (textBoxNameProject.Text != String.Empty)
			{
				
				Project project = new Project();
				project.Name = textBoxNameProject.Text;
				List<ProjectTracker> issueCategories = new List<ProjectTracker>();
				issueCategories.Add(new ProjectTracker() { Id=2 });
				issueCategories.Add(new ProjectTracker() { Name = "Feature" });
				issueCategories.Add(new ProjectTracker() { Name = "Support" });

				project.Trackers = issueCategories;
				project.Identifier = RandomIdetifier();
				comboBoxProjects.Items.Add(project.Name);
				if (textBoxDescrptProject.Text != null)
				{
					project.Description = textBoxDescrptProject.Text;
				}
				
				Globals.Ribbons.MyRibbon.manager.CreateObject(project);
				MessageBox.Show("Проект создан"+Environment.NewLine+$"{project.Name}");
				

			}


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
