namespace RedmineOutlookAddIn
{
	partial class MyRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public MyRibbon()
			: base(Globals.Factory.GetRibbonFactory())
		{
			InitializeComponent();
		}

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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyRibbon));
			this.tab2 = this.Factory.CreateRibbonTab();
			this.group2 = this.Factory.CreateRibbonGroup();
			this.menu1 = this.Factory.CreateRibbonMenu();
			this.button4 = this.Factory.CreateRibbonButton();
			this.button5 = this.Factory.CreateRibbonButton();
			this.buttonUser = this.Factory.CreateRibbonButton();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.buttonAddTask = this.Factory.CreateRibbonButton();
			this.buttonCalendar = this.Factory.CreateRibbonButton();
			this.group3 = this.Factory.CreateRibbonGroup();
			this.buttonUpdate = this.Factory.CreateRibbonButton();
			this.buttonSetting = this.Factory.CreateRibbonButton();
			this.group4 = this.Factory.CreateRibbonGroup();
			this.btnAddMyFormsTask = this.Factory.CreateRibbonButton();
			this.btnTaskTable = this.Factory.CreateRibbonButton();
			this.progressBarArray1 = new Microsoft.VisualBasic.Compatibility.VB6.ProgressBarArray(this.components);
			this.tab2.SuspendLayout();
			this.group2.SuspendLayout();
			this.group1.SuspendLayout();
			this.group3.SuspendLayout();
			this.group4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.progressBarArray1)).BeginInit();
			this.SuspendLayout();
			// 
			// tab2
			// 
			this.tab2.Groups.Add(this.group2);
			this.tab2.Groups.Add(this.group1);
			this.tab2.Groups.Add(this.group3);
			this.tab2.Groups.Add(this.group4);
			resources.ApplyResources(this.tab2, "tab2");
			this.tab2.Name = "tab2";
			// 
			// group2
			// 
			this.group2.Items.Add(this.menu1);
			resources.ApplyResources(this.group2, "group2");
			this.group2.Name = "group2";
			// 
			// menu1
			// 
			resources.ApplyResources(this.menu1, "menu1");
			this.menu1.Items.Add(this.button4);
			this.menu1.Items.Add(this.button5);
			this.menu1.Items.Add(this.buttonUser);
			this.menu1.Name = "menu1";
			this.menu1.ShowImage = true;
			// 
			// button4
			// 
			resources.ApplyResources(this.button4, "button4");
			this.button4.Name = "button4";
			this.button4.ShowImage = true;
			// 
			// button5
			// 
			resources.ApplyResources(this.button5, "button5");
			this.button5.Name = "button5";
			this.button5.ShowImage = true;
			// 
			// buttonUser
			// 
			resources.ApplyResources(this.buttonUser, "buttonUser");
			this.buttonUser.Name = "buttonUser";
			this.buttonUser.ShowImage = true;
			this.buttonUser.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonUser_Click);
			// 
			// group1
			// 
			this.group1.Items.Add(this.buttonAddTask);
			this.group1.Items.Add(this.buttonCalendar);
			resources.ApplyResources(this.group1, "group1");
			this.group1.Name = "group1";
			// 
			// buttonAddTask
			// 
			this.buttonAddTask.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonAddTask, "buttonAddTask");
			this.buttonAddTask.Name = "buttonAddTask";
			this.buttonAddTask.ShowImage = true;
			this.buttonAddTask.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
			// 
			// buttonCalendar
			// 
			this.buttonCalendar.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonCalendar, "buttonCalendar");
			this.buttonCalendar.Name = "buttonCalendar";
			this.buttonCalendar.ShowImage = true;
			this.buttonCalendar.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
			// 
			// group3
			// 
			this.group3.Items.Add(this.buttonUpdate);
			this.group3.Items.Add(this.buttonSetting);
			resources.ApplyResources(this.group3, "group3");
			this.group3.Name = "group3";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.ShowImage = true;
			this.buttonUpdate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button6_Click);
			// 
			// buttonSetting
			// 
			this.buttonSetting.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonSetting, "buttonSetting");
			this.buttonSetting.Name = "buttonSetting";
			this.buttonSetting.ShowImage = true;
			this.buttonSetting.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
			// 
			// group4
			// 
			this.group4.Items.Add(this.btnAddMyFormsTask);
			this.group4.Items.Add(this.btnTaskTable);
			resources.ApplyResources(this.group4, "group4");
			this.group4.Name = "group4";
			// 
			// btnAddMyFormsTask
			// 
			this.btnAddMyFormsTask.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.btnAddMyFormsTask, "btnAddMyFormsTask");
			this.btnAddMyFormsTask.Name = "btnAddMyFormsTask";
			this.btnAddMyFormsTask.ShowImage = true;
			this.btnAddMyFormsTask.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddMyFormsTask_Click);
			// 
			// btnTaskTable
			// 
			this.btnTaskTable.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.btnTaskTable, "btnTaskTable");
			this.btnTaskTable.Name = "btnTaskTable";
			this.btnTaskTable.ShowImage = true;
			this.btnTaskTable.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnTaskTable_Click);
			// 
			// MyRibbon
			// 
			this.Name = "MyRibbon";
			this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
			this.Tabs.Add(this.tab2);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.MyRibbon_Load);
			this.tab2.ResumeLayout(false);
			this.tab2.PerformLayout();
			this.group2.ResumeLayout(false);
			this.group2.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();
			this.group3.ResumeLayout(false);
			this.group3.PerformLayout();
			this.group4.ResumeLayout(false);
			this.group4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.progressBarArray1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		internal Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddTask;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSetting;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCalendar;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button5;
		private Microsoft.Office.Tools.Ribbon.RibbonButton buttonUpdate;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonUser;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddMyFormsTask;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btnTaskTable;
		private Microsoft.VisualBasic.Compatibility.VB6.ProgressBarArray progressBarArray1;
	}

	partial class ThisRibbonCollection
	{
		internal MyRibbon MyRibbon
		{
			get { return this.GetRibbon<MyRibbon>(); }
		}
	}
}
