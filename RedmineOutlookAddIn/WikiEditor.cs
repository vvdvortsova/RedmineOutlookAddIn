using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using GemBox.Document;
using WindowsFormsRichTextEditor;

namespace RedmineOutlookAddIn
{
	public partial class WikiEditor : Form
	{
		private readonly List<byte[]> sampleFiles = new List<byte[]>()
		{
			//Resources.Character_Formatting,
			//Resources.Paragraph_Formatting,
			//Resources.Lists,
			//Resources.Style_Resolution,
			//Resources.Simple_Table,
		};
		public WikiEditor()
		{
			InitializeComponent();

			ComponentInfo.SetLicense("FREE-LIMITED-KEY");
			ComponentInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
		}
		private void DoGemBoxCopy()
		{
			using (var stream = new MemoryStream())
			{
				// Save RichTextBox selection to RTF stream.
				var writer = new StreamWriter(stream);
				writer.Write(this.richTextBox.SelectedRtf);
				writer.Flush();

				stream.Position = 0;

				// Save RTF stream to clipboard.
				DocumentModel.Load(stream, LoadOptions.RtfDefault).Content.SaveToClipboard();
			}
		}

		private void DoGemBoxPaste(bool prepend)
		{
			using (var stream = new MemoryStream())
			{
				// Save RichTextBox content to RTF stream.
				var writer = new StreamWriter(stream);
				writer.Write(this.richTextBox.SelectedRtf);
				writer.Flush();

				stream.Position = 0;

				// Load document from RTF stream and prepend or append clipboard content to it.
				var document = DocumentModel.Load(stream, LoadOptions.RtfDefault);
				var content = document.Content;
				(prepend ? content.Start : content.End).LoadFromClipboard();

				stream.Position = 0;

				// Save document to RTF stream.
				document.Save(stream, SaveOptions.RtfDefault);

				stream.Position = 0;

				// Load RTF stream into RichTextBox.
				var reader = new StreamReader(stream);
				this.richTextBox.SelectedRtf = reader.ReadToEnd();
			}
		}

		private FontStyle ToggleFontStyle(FontStyle item, FontStyle toggle)
		{
			return item ^ toggle;
		}
	}
}
