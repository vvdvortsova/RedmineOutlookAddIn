using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Controls;

namespace RedmineOutlookAddIn
{
	public static class Ext
	{
		public static void SetText(this RichTextBox richTextBox, string text)
		{
			richTextBox.Document.Blocks.Clear();
			richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
		}

		public static string GetText(this RichTextBox richTextBox)
		{
			return new TextRange(richTextBox.Document.ContentStart,
				richTextBox.Document.ContentEnd).Text;
		}
	}
	/// <summary>
	/// Interaction logic for addForm.xaml
	/// </summary>
	public partial class addForm : Window
    {
        public addForm()
        {
            InitializeComponent();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			WikiEditor wikiEditor = new WikiEditor();
			string writePath = "../../description.txt";
			//запись в файл 
			TextRange doc = new TextRange(richTextBoxMain.Document.ContentStart, richTextBoxMain.Document.ContentEnd);
			using (FileStream fs = File.Create(writePath))
			{
				if (Path.GetExtension(writePath).ToLower() == ".rtf")
					doc.Save(fs, DataFormats.Rtf);
				else if (Path.GetExtension(writePath).ToLower() == ".txt")
					doc.Save(fs, DataFormats.Text);
				else
					doc.Save(fs, DataFormats.Xaml);
			}
			//загрузка в рич бокс
			TextRange doc2 = new TextRange(wikiEditor.rtbEditor.Document.ContentStart, wikiEditor.rtbEditor.Document.ContentEnd);
			using (FileStream fs = new FileStream(writePath, FileMode.Open))
			{
				if (Path.GetExtension(writePath).ToLower() == ".rtf")
					doc2.Load(fs, DataFormats.Rtf);
				else if (Path.GetExtension(writePath).ToLower() == ".txt")
					doc2.Load(fs, DataFormats.Text);
				else
					doc2.Load(fs, DataFormats.Xaml);
			}
			wikiEditor.Show();
			
			
			//Ext.SetText(wikiEditor.rtbEditor,Ext.GetText(richTextBoxMain));
			//Ext.SetText(richTextBoxMain, Ext.GetText(wikiEditor.rtbEditor));
			if ((bool)wikiEditor.checkSave.IsChecked)
			{
				//снова запись
				TextRange doc3 = new TextRange(wikiEditor.rtbEditor.Document.ContentStart, wikiEditor.rtbEditor.Document.ContentEnd);
				using (FileStream fs = File.Create(writePath))
				{
					if (Path.GetExtension(writePath).ToLower() == ".rtf")
						doc3.Save(fs, DataFormats.Rtf);
					else if (Path.GetExtension(writePath).ToLower() == ".txt")
						doc3.Save(fs, DataFormats.Text);
					else
						doc3.Save(fs, DataFormats.Xaml);
				}
				//загрузка в старый рич бокс
				TextRange doc4 = new TextRange(richTextBoxMain.Document.ContentStart, richTextBoxMain.Document.ContentEnd);
				using (FileStream fs = new FileStream(writePath, FileMode.Open))
				{
					if (Path.GetExtension(writePath).ToLower() == ".rtf")
						doc4.Load(fs, DataFormats.Rtf);
					else if (Path.GetExtension(writePath).ToLower() == ".txt")
						doc4.Load(fs, DataFormats.Text);
					else
						doc4.Load(fs, DataFormats.Xaml);
				}
			}
			
			
			

		}
		
	}
}
