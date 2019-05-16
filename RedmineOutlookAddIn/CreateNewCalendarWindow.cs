using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedmineOutlookAddIn
{
	public partial class CreateNewCalendarWindow : Form
	{
		public CreateNewCalendarWindow()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Метод,создающий новый календарь текущего пользователя.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCreateNewCalendar_Click(object sender, EventArgs e)
		{
			if (textBoxNameCalendar.Text != null)
			{
				RedmineOutlookAddIn.Properties.Settings.Default.CurrentCalendar = textBoxNameCalendar.Text;
				RedmineOutlookAddIn.Properties.Settings.Default.Save();
				MessageBox.Show("Текущий календарь изменен!");
			}
			else
			{
				MessageBox.Show("Добавьте имя в календарь!");
			}
			
		}
	}
}
