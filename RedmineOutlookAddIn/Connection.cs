using System;
using System.Windows.Forms;

namespace RedmineOutlookAddIn
{
	public partial class Connection : Form
	{
		public Connection()
		{
			InitializeComponent();

		}

		
		/// <summary>
		/// Метод записывающий данные в переменные среды.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

				MessageBox.Show(EX.Message + Environment.NewLine + "Произошла ошибка:(" + Environment.NewLine + "Попробуйте снова)");
			}
		}

		/// <summary>
		/// Метод ,скрывающий пароль звездочками.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked)
			{
				textBoxApiKey.UseSystemPasswordChar = true;
			}
			else textBoxApiKey.UseSystemPasswordChar = false;
		}


		/// <summary>
		/// Метод переводящий на главную страницу Redmine.org.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.redmine.org/");
		}
		/// <summary>
		/// Метод записывающий айди и хост в переменные среды.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
