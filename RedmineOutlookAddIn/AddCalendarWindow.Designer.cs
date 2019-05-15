namespace RedmineOutlookAddIn
{
	partial class AddCalendarWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCalendarWindow));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxNameCalendar = new System.Windows.Forms.TextBox();
			this.buttonCreateCalendar = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(30, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Название календаря";
			// 
			// textBoxNameCalendar
			// 
			this.textBoxNameCalendar.Location = new System.Drawing.Point(33, 42);
			this.textBoxNameCalendar.Name = "textBoxNameCalendar";
			this.textBoxNameCalendar.Size = new System.Drawing.Size(182, 20);
			this.textBoxNameCalendar.TabIndex = 1;
			// 
			// buttonCreateCalendar
			// 
			this.buttonCreateCalendar.Location = new System.Drawing.Point(33, 81);
			this.buttonCreateCalendar.Name = "buttonCreateCalendar";
			this.buttonCreateCalendar.Size = new System.Drawing.Size(182, 23);
			this.buttonCreateCalendar.TabIndex = 2;
			this.buttonCreateCalendar.Text = "Создать";
			this.buttonCreateCalendar.UseVisualStyleBackColor = true;
			this.buttonCreateCalendar.Click += new System.EventHandler(this.buttonCreateCalendar_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(226, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(196, 26);
			this.label2.TabIndex = 3;
			this.label2.Text = "*Теперь все текущие задачи \r\nбудут добавляться в этот календарь.";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(247, 24);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(155, 80);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// AddCalendarWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(425, 150);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCreateCalendar);
			this.Controls.Add(this.textBoxNameCalendar);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddCalendarWindow";
			this.Text = "AddCalendarWindow";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNameCalendar;
		private System.Windows.Forms.Button buttonCreateCalendar;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}