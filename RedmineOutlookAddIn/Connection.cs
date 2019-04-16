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

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void Connection_Load(object sender, EventArgs e)
		{
			panel2.Visible = false;
			panel3.Visible = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			panel2.Visible = true;
			panel3.Visible = false;
			
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			panel3.Visible = true;
			panel2.Visible = false;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				if (!File.Exists("../../Mypassword.txt"))
				{
					File.Create("../../Mypassword.txt");
					

				}
				using (StreamWriter stream = new StreamWriter("../../Mypassword.txt"))
				{
					stream.WriteLine(textBoxUrl.Text);
					stream.WriteLine(textBoxApiKey.Text);
					MessageBox.Show("Пароль записан");
				}

			}
			catch (Exception EX)
			{

				MessageBox.Show(EX.Message + Environment.NewLine+"Произошла ошибка:("+ Environment.NewLine+"Попробуйте снова)");
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{

			}
			catch (Exception EX)
			{

				MessageBox.Show(EX.Message + Environment.NewLine + "Произошла ошибка:(" + Environment.NewLine + "Попробуйте снова)");
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

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked)
			{
				textBoxPassword.UseSystemPasswordChar = true;
			}
			else textBoxPassword.UseSystemPasswordChar = false;
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

			}

		}

		private void checkBoxRememberLoginPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxRememberLoginPassword.Checked)
			{
				RedmineOutlookAddIn.Properties.Settings.Default.password = textBoxPassword.Text;
				RedmineOutlookAddIn.Properties.Settings.Default.login = textBoxLogin.Text;
			}
		}
	}
}
