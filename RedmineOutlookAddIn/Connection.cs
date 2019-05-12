using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RedmineOutlookAddIn
{
	public partial class Connection : Form
	{
		public Connection()
		{
			InitializeComponent();
			
		}

		

		

		private void Connection_Load(object sender, EventArgs e)
		{
			
			
		}

		

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					if (checkBoxRemeberApiUrl.Checked)
					{
						RedmineOutlookAddIn.Properties.Settings.Default.apyKey = textBoxApiKey.Text;
						RedmineOutlookAddIn.Properties.Settings.Default.host = textBoxUrl.Text;
						RedmineOutlookAddIn.Properties.Settings.Default.Save();
						MessageBox.Show("Вход выполнен успешно");
					}
				}
				catch (Exception EX)
				{

					MessageBox.Show(EX.Message + Environment.NewLine + "Произошла ошибка:(" + Environment.NewLine + "Попробуйте снова)");
				}
				//if (!File.Exists("../../Mypassword.txt"))
				//{
				//	File.Create("../../Mypassword.txt");


				//}
				//using (StreamWriter stream = new StreamWriter("../../Mypassword.txt"))
				//{
				//	stream.WriteLine(textBoxUrl.Text);
				//	stream.WriteLine(textBoxApiKey.Text);
				//	MessageBox.Show("Пароль записан");
				//}

			}
			catch (Exception EX)
			{

				MessageBox.Show(EX.Message + Environment.NewLine+"Произошла ошибка:("+ Environment.NewLine+"Попробуйте снова)");
			}
		}

		
		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked)
			{
				textBoxApiKey.UseSystemPasswordChar = true;
			}
			else textBoxApiKey.UseSystemPasswordChar = false;
		}

		

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.redmine.org/");
		}

		private void checkBoxRemeberApiUrl_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxRemeberApiUrl.Checked)
			{
				RedmineOutlookAddIn.Properties.Settings.Default.apyKey = textBoxApiKey.Text;
				RedmineOutlookAddIn.Properties.Settings.Default.host = textBoxUrl.Text;
				RedmineOutlookAddIn.Properties.Settings.Default.Save();

			}

		}

		private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}
	}
}
