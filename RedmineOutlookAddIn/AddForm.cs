using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace RedmineOutlookAddIn
{
	public partial class AddForm : Form
	{
		public static string host = "http://79.137.216.214/redmine/";
		public static string apiKey = "4444025d7e83c49e92466b5399ba7ee06c464637";
		public static RedmineManager manager = new RedmineManager(host, apiKey);
		public AddForm()
		{
			InitializeComponent();
		}
		
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			//Globals.Ribbons.MyRibbon.manager
		}

		private void btnEditor_Click(object sender, EventArgs e)
		{
			WikiEditor wikiEditor = new WikiEditor();
			wikiEditor.rtbEditor.Document.Blocks.Add(new Paragraph(new Run(richTextBoxAddForms.Text)));
			
			wikiEditor.Show();
			richTextBoxAddForms.Text = new TextRange(wikiEditor.rtbEditor.Document.ContentStart, wikiEditor.rtbEditor.Document.ContentEnd).Text;


		}

		private void btnAddTask_Click(object sender, EventArgs e)
		{

			var newIssue = new Issue
			{
				Subject = "Добавление из моей формы",
				Project = new IdentifiableName { Id = 1 },
				Description = "srgsrg"
				
			};
			manager.CreateObject(newIssue);
		}

	}
}
