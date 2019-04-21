namespace RedmineOutlookAddIn
{
	[System.ComponentModel.ToolboxItemAttribute(false)]
	partial class FormRegion2 : Microsoft.Office.Tools.Outlook.FormRegionBase
	{
		public FormRegion2(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
			: base(Globals.Factory, formRegion)
		{
			this.InitializeComponent();
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Проект = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ИД = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Проект,
            this.ИД,
            this.Column1});
			this.dataGridView1.Location = new System.Drawing.Point(63, 26);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(347, 150);
			this.dataGridView1.TabIndex = 0;
			// 
			// Проект
			// 
			this.Проект.HeaderText = "Проект";
			this.Проект.Name = "Проект";
			// 
			// ИД
			// 
			this.ИД.HeaderText = "ИД";
			this.ИД.Name = "ИД";
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Задача";
			this.Column1.Name = "Column1";
			// 
			// FormRegion2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView1);
			this.Name = "FormRegion2";
			this.Size = new System.Drawing.Size(502, 303);
			this.FormRegionShowing += new System.EventHandler(this.FormRegion2_FormRegionShowing);
			this.FormRegionClosed += new System.EventHandler(this.FormRegion2_FormRegionClosed);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region Form Region Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private static void InitializeManifest(Microsoft.Office.Tools.Outlook.FormRegionManifest manifest, Microsoft.Office.Tools.Outlook.Factory factory)
		{
			manifest.FormRegionName = "FormRRegionForTable";
			manifest.ShowReadingPane = false;

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Проект;
		private System.Windows.Forms.DataGridViewTextBoxColumn ИД;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;

		public partial class FormRegion2Factory : Microsoft.Office.Tools.Outlook.IFormRegionFactory
		{
			public event Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler FormRegionInitializing;

			private Microsoft.Office.Tools.Outlook.FormRegionManifest _Manifest;

			[System.Diagnostics.DebuggerNonUserCodeAttribute()]
			public FormRegion2Factory()
			{
				this._Manifest = Globals.Factory.CreateFormRegionManifest();
				FormRegion2.InitializeManifest(this._Manifest, Globals.Factory);
				this.FormRegionInitializing += new Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler(this.FormRegion2Factory_FormRegionInitializing);
			}

			[System.Diagnostics.DebuggerNonUserCodeAttribute()]
			public Microsoft.Office.Tools.Outlook.FormRegionManifest Manifest
			{
				get
				{
					return this._Manifest;
				}
			}

			[System.Diagnostics.DebuggerNonUserCodeAttribute()]
			Microsoft.Office.Tools.Outlook.IFormRegion Microsoft.Office.Tools.Outlook.IFormRegionFactory.CreateFormRegion(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
			{
				FormRegion2 form = new FormRegion2(formRegion);
				form.Factory = this;
				return form;
			}

			[System.Diagnostics.DebuggerNonUserCodeAttribute()]
			byte[] Microsoft.Office.Tools.Outlook.IFormRegionFactory.GetFormRegionStorage(object outlookItem, Microsoft.Office.Interop.Outlook.OlFormRegionMode formRegionMode, Microsoft.Office.Interop.Outlook.OlFormRegionSize formRegionSize)
			{
				throw new System.NotSupportedException();
			}

			[System.Diagnostics.DebuggerNonUserCodeAttribute()]
			bool Microsoft.Office.Tools.Outlook.IFormRegionFactory.IsDisplayedForItem(object outlookItem, Microsoft.Office.Interop.Outlook.OlFormRegionMode formRegionMode, Microsoft.Office.Interop.Outlook.OlFormRegionSize formRegionSize)
			{
				if (this.FormRegionInitializing != null)
				{
					Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs cancelArgs = Globals.Factory.CreateFormRegionInitializingEventArgs(outlookItem, formRegionMode, formRegionSize, false);
					this.FormRegionInitializing(this, cancelArgs);
					return !cancelArgs.Cancel;
				}
				else
				{
					return true;
				}
			}

			[System.Diagnostics.DebuggerNonUserCodeAttribute()]
			Microsoft.Office.Tools.Outlook.FormRegionKindConstants Microsoft.Office.Tools.Outlook.IFormRegionFactory.Kind
			{
				get
				{
					return Microsoft.Office.Tools.Outlook.FormRegionKindConstants.WindowsForms;
				}
			}
		}
	}

	partial class WindowFormRegionCollection
	{
		internal FormRegion2 FormRegion2
		{
			get
			{
				foreach (var item in this)
				{
					if (item.GetType() == typeof(FormRegion2))
						return (FormRegion2)item;
				}
				return null;
			}
		}
	}
}
