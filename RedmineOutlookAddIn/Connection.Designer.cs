namespace RedmineOutlookAddIn
{
	partial class Connection
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
			this.checkBoxRemeberApiUrl = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.button3 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxApiKey = new System.Windows.Forms.TextBox();
			this.textBoxUrl = new System.Windows.Forms.TextBox();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// checkBoxRemeberApiUrl
			// 
			this.checkBoxRemeberApiUrl.AutoSize = true;
			this.checkBoxRemeberApiUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxRemeberApiUrl.Location = new System.Drawing.Point(189, 156);
			this.checkBoxRemeberApiUrl.Name = "checkBoxRemeberApiUrl";
			this.checkBoxRemeberApiUrl.Size = new System.Drawing.Size(95, 20);
			this.checkBoxRemeberApiUrl.TabIndex = 8;
			this.checkBoxRemeberApiUrl.Text = "Remember";
			this.checkBoxRemeberApiUrl.UseVisualStyleBackColor = true;
			this.checkBoxRemeberApiUrl.CheckedChanged += new System.EventHandler(this.checkBoxRemeberApiUrl_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBox2.Location = new System.Drawing.Point(342, 105);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(60, 20);
			this.checkBox2.TabIndex = 7;
			this.checkBox2.Text = "Show";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel1.LinkColor = System.Drawing.Color.Maroon;
			this.linkLabel1.LinkVisited = true;
			this.linkLabel1.Location = new System.Drawing.Point(71, 125);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(86, 16);
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Redmine.org";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3.Location = new System.Drawing.Point(74, 156);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "войти";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(7, 105);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "api key";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(9, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(22, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "url";
			// 
			// textBoxApiKey
			// 
			this.textBoxApiKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxApiKey.Location = new System.Drawing.Point(74, 103);
			this.textBoxApiKey.Multiline = true;
			this.textBoxApiKey.Name = "textBoxApiKey";
			this.textBoxApiKey.PasswordChar = '*';
			this.textBoxApiKey.Size = new System.Drawing.Size(262, 20);
			this.textBoxApiKey.TabIndex = 1;
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxUrl.Location = new System.Drawing.Point(74, 73);
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(262, 22);
			this.textBoxUrl.TabIndex = 0;
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Location = new System.Drawing.Point(275, 291);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(127, 13);
			this.linkLabel3.TabIndex = 3;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "vvdvortsova@edu.hse.ru";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(135, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Добро пожаловать ";
			// 
			// Connection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(409, 313);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.checkBoxRemeberApiUrl);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.textBoxUrl);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.textBoxApiKey);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label3);
			this.Name = "Connection";
			this.Text = "Connection";
			this.Load += new System.EventHandler(this.Connection_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		internal System.Windows.Forms.LinkLabel linkLabel1;
		internal System.Windows.Forms.Button button3;
		internal System.Windows.Forms.Label label3;
		internal System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox textBoxApiKey;
		internal System.Windows.Forms.TextBox textBoxUrl;
		internal System.Windows.Forms.LinkLabel linkLabel3;
		internal System.Windows.Forms.CheckBox checkBox2;
		internal System.Windows.Forms.CheckBox checkBoxRemeberApiUrl;
		internal System.Windows.Forms.Label label1;
	}
}