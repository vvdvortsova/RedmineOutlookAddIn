namespace RedmineOutlookAddIn
{
	partial class Redmine : Microsoft.Office.Tools.Ribbon.RibbonBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Redmine()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Redmine));
			this.MyRedmine = this.Factory.CreateRibbonTab();
			this.tab1 = this.Factory.CreateRibbonTab();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.button1 = this.Factory.CreateRibbonButton();
			this.button2 = this.Factory.CreateRibbonButton();
			this.MyRedmine.SuspendLayout();
			this.tab1.SuspendLayout();
			this.group1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MyRedmine
			// 
			this.MyRedmine.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			resources.ApplyResources(this.MyRedmine, "MyRedmine");
			this.MyRedmine.Name = "MyRedmine";
			// 
			// tab1
			// 
			this.tab1.Groups.Add(this.group1);
			resources.ApplyResources(this.tab1, "tab1");
			this.tab1.Name = "tab1";
			// 
			// group1
			// 
			this.group1.Items.Add(this.button1);
			this.group1.Items.Add(this.button2);
			resources.ApplyResources(this.group1, "group1");
			this.group1.Name = "group1";
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			// 
			// button2
			// 
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			// 
			// Redmine
			// 
			this.Name = "Redmine";
			this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
			this.Tabs.Add(this.MyRedmine);
			this.Tabs.Add(this.tab1);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
			this.MyRedmine.ResumeLayout(false);
			this.MyRedmine.PerformLayout();
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		public Microsoft.Office.Tools.Ribbon.RibbonTab MyRedmine;
		public Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
		public Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		public Microsoft.Office.Tools.Ribbon.RibbonButton button1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
	}

	partial class ThisRibbonCollection
	{
		internal Redmine Ribbon1
		{
			get { return this.GetRibbon<Redmine>(); }
		}
	}
}
