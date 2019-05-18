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
			this.menuPROJECT = this.Factory.CreateRibbonMenu();
			this.buttonCurretProject = this.Factory.CreateRibbonButton();
			this.buttonChangeOrCreateProject = this.Factory.CreateRibbonButton();
			this.menu2 = this.Factory.CreateRibbonMenu();
			this.buttonShowCurrentCalendar = this.Factory.CreateRibbonButton();
			this.buttonCreateNewCalendarOrChange = this.Factory.CreateRibbonButton();
			this.buttonSetting = this.Factory.CreateRibbonButton();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.buttonAddTask = this.Factory.CreateRibbonButton();
			this.separator3 = this.Factory.CreateRibbonSeparator();
			this.buttonOpenCalendary = this.Factory.CreateRibbonButton();
			this.separator1 = this.Factory.CreateRibbonSeparator();
			this.buttonCalendR = this.Factory.CreateRibbonButton();
			this.separator2 = this.Factory.CreateRibbonSeparator();
			this.buttonUpdate = this.Factory.CreateRibbonButton();
			this.progressBarArray1 = new Microsoft.VisualBasic.Compatibility.VB6.ProgressBarArray(this.components);
			this.tab2.SuspendLayout();
			this.group2.SuspendLayout();
			this.group1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.progressBarArray1)).BeginInit();
			this.SuspendLayout();
			// 
			// tab2
			// 
			this.tab2.Groups.Add(this.group2);
			this.tab2.Groups.Add(this.group1);
			resources.ApplyResources(this.tab2, "tab2");
			this.tab2.Name = "tab2";
			// 
			// group2
			// 
			this.group2.Items.Add(this.menu1);
			this.group2.Items.Add(this.menuPROJECT);
			this.group2.Items.Add(this.menu2);
			this.group2.Items.Add(this.buttonSetting);
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
			this.button4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button4_Click);
			// 
			// button5
			// 
			resources.ApplyResources(this.button5, "button5");
			this.button5.Name = "button5";
			this.button5.ShowImage = true;
			this.button5.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button5_Click);
			// 
			// buttonUser
			// 
			resources.ApplyResources(this.buttonUser, "buttonUser");
			this.buttonUser.Name = "buttonUser";
			this.buttonUser.ShowImage = true;
			this.buttonUser.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonUser_Click);
			// 
			// menuPROJECT
			// 
			resources.ApplyResources(this.menuPROJECT, "menuPROJECT");
			this.menuPROJECT.Items.Add(this.buttonCurretProject);
			this.menuPROJECT.Items.Add(this.buttonChangeOrCreateProject);
			this.menuPROJECT.Name = "menuPROJECT";
			this.menuPROJECT.ShowImage = true;
			// 
			// buttonCurretProject
			// 
			resources.ApplyResources(this.buttonCurretProject, "buttonCurretProject");
			this.buttonCurretProject.Name = "buttonCurretProject";
			this.buttonCurretProject.ShowImage = true;
			this.buttonCurretProject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonCurretProject_Click);
			// 
			// buttonChangeOrCreateProject
			// 
			resources.ApplyResources(this.buttonChangeOrCreateProject, "buttonChangeOrCreateProject");
			this.buttonChangeOrCreateProject.Name = "buttonChangeOrCreateProject";
			this.buttonChangeOrCreateProject.ShowImage = true;
			this.buttonChangeOrCreateProject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonChangeOrCreateProject_Click);
			// 
			// menu2
			// 
			resources.ApplyResources(this.menu2, "menu2");
			this.menu2.Items.Add(this.buttonShowCurrentCalendar);
			this.menu2.Items.Add(this.buttonCreateNewCalendarOrChange);
			this.menu2.Name = "menu2";
			this.menu2.ShowImage = true;
			// 
			// buttonShowCurrentCalendar
			// 
			resources.ApplyResources(this.buttonShowCurrentCalendar, "buttonShowCurrentCalendar");
			this.buttonShowCurrentCalendar.Name = "buttonShowCurrentCalendar";
			this.buttonShowCurrentCalendar.ShowImage = true;
			this.buttonShowCurrentCalendar.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonShowCurrentCalendar_Click);
			// 
			// buttonCreateNewCalendarOrChange
			// 
			resources.ApplyResources(this.buttonCreateNewCalendarOrChange, "buttonCreateNewCalendarOrChange");
			this.buttonCreateNewCalendarOrChange.Name = "buttonCreateNewCalendarOrChange";
			this.buttonCreateNewCalendarOrChange.ShowImage = true;
			this.buttonCreateNewCalendarOrChange.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonCreateNewCalendarOrChange_Click);
			// 
			// buttonSetting
			// 
			this.buttonSetting.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonSetting, "buttonSetting");
			this.buttonSetting.Name = "buttonSetting";
			this.buttonSetting.ShowImage = true;
			this.buttonSetting.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
			// 
			// group1
			// 
			this.group1.Items.Add(this.buttonAddTask);
			this.group1.Items.Add(this.separator3);
			this.group1.Items.Add(this.buttonOpenCalendary);
			this.group1.Items.Add(this.separator1);
			this.group1.Items.Add(this.buttonCalendR);
			this.group1.Items.Add(this.separator2);
			this.group1.Items.Add(this.buttonUpdate);
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
			// separator3
			// 
			this.separator3.Name = "separator3";
			// 
			// buttonOpenCalendary
			// 
			this.buttonOpenCalendary.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonOpenCalendary, "buttonOpenCalendary");
			this.buttonOpenCalendary.Name = "buttonOpenCalendary";
			this.buttonOpenCalendary.ShowImage = true;
			this.buttonOpenCalendary.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonOpenCalendary_Click);
			// 
			// separator1
			// 
			this.separator1.Name = "separator1";
			// 
			// buttonCalendR
			// 
			this.buttonCalendR.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonCalendR, "buttonCalendR");
			this.buttonCalendR.Name = "buttonCalendR";
			this.buttonCalendR.ShowImage = true;
			this.buttonCalendR.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonCalendR_Click);
			// 
			// separator2
			// 
			this.separator2.Name = "separator2";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.ShowImage = true;
			this.buttonUpdate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button6_Click);
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
			((System.ComponentModel.ISupportInitialize)(this.progressBarArray1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		internal Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddTask;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSetting;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button5;
		private Microsoft.Office.Tools.Ribbon.RibbonButton buttonUpdate;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonUser;
		private Microsoft.VisualBasic.Compatibility.VB6.ProgressBarArray progressBarArray1;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuPROJECT;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCurretProject;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonChangeOrCreateProject;
		internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCalendR;
		internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
		internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator3;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonOpenCalendary;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu2;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonShowCurrentCalendar;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCreateNewCalendarOrChange;
	}

	partial class ThisRibbonCollection
	{
		internal MyRibbon MyRibbon
		{
			get { return this.GetRibbon<MyRibbon>(); }
		}
	}
}
