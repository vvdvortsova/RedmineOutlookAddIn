namespace RedmineOutlookAddIn
{
	partial class CreateNewCalendarWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewCalendarWindow));
			this.buttonCreateNewCalendar = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxNameCalendar = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonCreateNewCalendar
			// 
			this.buttonCreateNewCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonCreateNewCalendar.Location = new System.Drawing.Point(21, 58);
			this.buttonCreateNewCalendar.Name = "buttonCreateNewCalendar";
			this.buttonCreateNewCalendar.Size = new System.Drawing.Size(154, 23);
			this.buttonCreateNewCalendar.TabIndex = 0;
			this.buttonCreateNewCalendar.Text = "Изменить";
			this.buttonCreateNewCalendar.UseVisualStyleBackColor = true;
			this.buttonCreateNewCalendar.Click += new System.EventHandler(this.buttonCreateNewCalendar_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(18, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Введите название календаря";
			// 
			// textBoxNameCalendar
			// 
			this.textBoxNameCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxNameCalendar.Location = new System.Drawing.Point(21, 32);
			this.textBoxNameCalendar.Name = "textBoxNameCalendar";
			this.textBoxNameCalendar.Size = new System.Drawing.Size(154, 22);
			this.textBoxNameCalendar.TabIndex = 2;
			// 
			// CreateNewCalendarWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(258, 93);
			this.Controls.Add(this.textBoxNameCalendar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCreateNewCalendar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CreateNewCalendarWindow";
			this.Text = "CreateNewCalendarWindow";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCreateNewCalendar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNameCalendar;
	}
}