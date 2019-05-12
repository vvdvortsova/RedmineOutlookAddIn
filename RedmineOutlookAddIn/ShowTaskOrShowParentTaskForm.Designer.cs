namespace RedmineOutlookAddIn
{
	partial class ShowTaskOrShowParentTaskForm
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
			this.buttonOpenTask = new System.Windows.Forms.Button();
			this.buttonShowParent = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonOpenTask
			// 
			this.buttonOpenTask.Location = new System.Drawing.Point(38, 33);
			this.buttonOpenTask.Name = "buttonOpenTask";
			this.buttonOpenTask.Size = new System.Drawing.Size(102, 23);
			this.buttonOpenTask.TabIndex = 0;
			this.buttonOpenTask.Text = "Редактировать";
			this.buttonOpenTask.UseVisualStyleBackColor = true;
			this.buttonOpenTask.Click += new System.EventHandler(this.buttonOpenTask_Click);
			// 
			// buttonShowParent
			// 
			this.buttonShowParent.Location = new System.Drawing.Point(179, 33);
			this.buttonShowParent.Name = "buttonShowParent";
			this.buttonShowParent.Size = new System.Drawing.Size(113, 23);
			this.buttonShowParent.TabIndex = 1;
			this.buttonShowParent.Text = "Посмореть Parent";
			this.buttonShowParent.UseVisualStyleBackColor = true;
			this.buttonShowParent.Click += new System.EventHandler(this.buttonShowParent_Click);
			// 
			// ShowTaskOrShowParentTaskForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 96);
			this.Controls.Add(this.buttonShowParent);
			this.Controls.Add(this.buttonOpenTask);
			this.Name = "ShowTaskOrShowParentTaskForm";
			this.Text = "ShowTaskOrShowParentTaskForm";
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.Button buttonOpenTask;
		internal System.Windows.Forms.Button buttonShowParent;
	}
}