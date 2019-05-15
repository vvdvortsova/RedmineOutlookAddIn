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
	public partial class AddCalendarWindow : Form
	{
		public AddCalendarWindow()
		{
			InitializeComponent();
		}

		private void buttonCreateCalendar_Click(object sender, EventArgs e)
		{
			if (textBoxNameCalendar.Text == String.Empty)
			{
				MessageBox.Show("Введите имя календаря");
			}
			else
			{
				RedmineOutlookAddIn.Properties.Settings.Default.CurrentCalendar = textBoxNameCalendar.Text;
				RedmineOutlookAddIn.Properties.Settings.Default.Save();
				MessageBox.Show("Текущий календарь изменен!");
			}
	
		}
	}
}
