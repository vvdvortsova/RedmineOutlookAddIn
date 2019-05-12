namespace RedmineOutlookAddIn
{
	partial class AddOrChangeProjectWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrChangeProjectWindow));
			this.comboBoxProjects = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonCreateProject = new System.Windows.Forms.Button();
			this.buttonChangeProject = new System.Windows.Forms.Button();
			this.textBoxNameProject = new System.Windows.Forms.TextBox();
			this.textBoxDescrptProject = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboBoxProjects
			// 
			this.comboBoxProjects.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboBoxProjects.FormattingEnabled = true;
			this.comboBoxProjects.Location = new System.Drawing.Point(16, 39);
			this.comboBoxProjects.Name = "comboBoxProjects";
			this.comboBoxProjects.Size = new System.Drawing.Size(121, 24);
			this.comboBoxProjects.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(266, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Выберите проект или создайте новый";
			// 
			// buttonCreateProject
			// 
			this.buttonCreateProject.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonCreateProject.Location = new System.Drawing.Point(16, 79);
			this.buttonCreateProject.Name = "buttonCreateProject";
			this.buttonCreateProject.Size = new System.Drawing.Size(100, 23);
			this.buttonCreateProject.TabIndex = 2;
			this.buttonCreateProject.Text = "Добавить";
			this.buttonCreateProject.UseVisualStyleBackColor = true;
			this.buttonCreateProject.Click += new System.EventHandler(this.buttonCreateProject_Click);
			// 
			// buttonChangeProject
			// 
			this.buttonChangeProject.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonChangeProject.Location = new System.Drawing.Point(125, 79);
			this.buttonChangeProject.Name = "buttonChangeProject";
			this.buttonChangeProject.Size = new System.Drawing.Size(87, 23);
			this.buttonChangeProject.TabIndex = 3;
			this.buttonChangeProject.Text = "Изменить";
			this.buttonChangeProject.UseVisualStyleBackColor = true;
			this.buttonChangeProject.Click += new System.EventHandler(this.buttonChangeProject_Click);
			// 
			// textBoxNameProject
			// 
			this.textBoxNameProject.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxNameProject.Location = new System.Drawing.Point(16, 142);
			this.textBoxNameProject.Name = "textBoxNameProject";
			this.textBoxNameProject.Size = new System.Drawing.Size(196, 23);
			this.textBoxNameProject.TabIndex = 4;
			
			// 
			// textBoxDescrptProject
			// 
			this.textBoxDescrptProject.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxDescrptProject.Location = new System.Drawing.Point(16, 212);
			this.textBoxDescrptProject.Multiline = true;
			this.textBoxDescrptProject.Name = "textBoxDescrptProject";
			this.textBoxDescrptProject.Size = new System.Drawing.Size(196, 69);
			this.textBoxDescrptProject.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(13, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Имя проекта*";
		
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(16, 184);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 16);
			this.label3.TabIndex = 7;
			this.label3.Text = "Описание";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(233, 249);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(123, 32);
			this.label5.TabIndex = 10;
			this.label5.Text = "* Обязательные \r\n  поля";
			// 
			// AddOrChangeProjectWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(363, 296);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxDescrptProject);
			this.Controls.Add(this.textBoxNameProject);
			this.Controls.Add(this.buttonChangeProject);
			this.Controls.Add(this.buttonCreateProject);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxProjects);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddOrChangeProjectWindow";
			this.Text = "Проект";
			
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxProjects;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonCreateProject;
		private System.Windows.Forms.Button buttonChangeProject;
		private System.Windows.Forms.TextBox textBoxNameProject;
		private System.Windows.Forms.TextBox textBoxDescrptProject;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
	}
}