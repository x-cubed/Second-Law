namespace SecondLaw {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Gain root access", "SecurityLock.ico");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Install TWRP Recovery", "ram.ico");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Install CM 7", "Setup_Install.ico");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Install CM 9", "Setup_Install.ico");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Setup USB Tether", "Dialup.ico");
			this.mnuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mniFileScanForDevices = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.systemInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.rebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bootloaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.powerOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.repositoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lnkDeviceName = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.imlLargeIcons = new System.Windows.Forms.ImageList(this.components);
			this.lsvDevices = new System.Windows.Forms.ListView();
			this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.mnuStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuStrip
			// 
			this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.deviceToolStripMenuItem,
            this.repositoriesToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mnuStrip.Location = new System.Drawing.Point(0, 0);
			this.mnuStrip.Name = "mnuStrip";
			this.mnuStrip.Size = new System.Drawing.Size(884, 28);
			this.mnuStrip.TabIndex = 0;
			this.mnuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFileScanForDevices,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// mniFileScanForDevices
			// 
			this.mniFileScanForDevices.Name = "mniFileScanForDevices";
			this.mniFileScanForDevices.Size = new System.Drawing.Size(194, 24);
			this.mniFileScanForDevices.Text = "&Scan for devices...";
			this.mniFileScanForDevices.Click += new System.EventHandler(this.mniFileScanForDevices_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(191, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// deviceToolStripMenuItem
			// 
			this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewlogToolStripMenuItem,
            this.browseToolStripMenuItem,
            this.toolStripMenuItem1,
            this.systemInformationToolStripMenuItem,
            this.toolStripMenuItem2,
            this.rebootToolStripMenuItem,
            this.powerOffToolStripMenuItem});
			this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
			this.deviceToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
			this.deviceToolStripMenuItem.Text = "&Device";
			// 
			// viewlogToolStripMenuItem
			// 
			this.viewlogToolStripMenuItem.Name = "viewlogToolStripMenuItem";
			this.viewlogToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
			this.viewlogToolStripMenuItem.Text = "View &log";
			this.viewlogToolStripMenuItem.Click += new System.EventHandler(this.viewlogToolStripMenuItem_Click);
			// 
			// browseToolStripMenuItem
			// 
			this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
			this.browseToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
			this.browseToolStripMenuItem.Text = "&Browse drive...";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(204, 6);
			// 
			// systemInformationToolStripMenuItem
			// 
			this.systemInformationToolStripMenuItem.Name = "systemInformationToolStripMenuItem";
			this.systemInformationToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
			this.systemInformationToolStripMenuItem.Text = "&System information";
			this.systemInformationToolStripMenuItem.Click += new System.EventHandler(this.systemInformationToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(204, 6);
			// 
			// rebootToolStripMenuItem
			// 
			this.rebootToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.recoveryToolStripMenuItem,
            this.bootloaderToolStripMenuItem});
			this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
			this.rebootToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
			this.rebootToolStripMenuItem.Text = "&Reboot";
			// 
			// normalToolStripMenuItem
			// 
			this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
			this.normalToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
			this.normalToolStripMenuItem.Text = "Normal";
			this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
			// 
			// recoveryToolStripMenuItem
			// 
			this.recoveryToolStripMenuItem.Name = "recoveryToolStripMenuItem";
			this.recoveryToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
			this.recoveryToolStripMenuItem.Text = "Recovery";
			this.recoveryToolStripMenuItem.Click += new System.EventHandler(this.recoveryToolStripMenuItem_Click);
			// 
			// bootloaderToolStripMenuItem
			// 
			this.bootloaderToolStripMenuItem.Name = "bootloaderToolStripMenuItem";
			this.bootloaderToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
			this.bootloaderToolStripMenuItem.Text = "Bootloader";
			this.bootloaderToolStripMenuItem.Click += new System.EventHandler(this.bootloaderToolStripMenuItem_Click);
			// 
			// powerOffToolStripMenuItem
			// 
			this.powerOffToolStripMenuItem.Name = "powerOffToolStripMenuItem";
			this.powerOffToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
			this.powerOffToolStripMenuItem.Text = "&Power Off";
			// 
			// repositoriesToolStripMenuItem
			// 
			this.repositoriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripMenuItem4,
            this.addToolStripMenuItem});
			this.repositoriesToolStripMenuItem.Name = "repositoriesToolStripMenuItem";
			this.repositoriesToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
			this.repositoriesToolStripMenuItem.Text = "&Packages";
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
			this.checkForUpdatesToolStripMenuItem.Text = "&Check for updates";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(194, 6);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
			this.addToolStripMenuItem.Text = "&Add repository...";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
			this.aboutToolStripMenuItem.Text = "&About";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tableLayoutPanel1);
			this.groupBox1.Controls.Add(this.pictureBox1);
			this.groupBox1.Location = new System.Drawing.Point(12, 31);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(292, 610);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.label7, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.linkLabel2, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lnkDeviceName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label6, 1, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(17, 289);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 224);
			this.tableLayoutPanel1.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(131, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(122, 20);
			this.label7.TabIndex = 13;
			this.label7.Text = "label7";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(122, 20);
			this.label4.TabIndex = 10;
			this.label4.Text = "Serial:";
			// 
			// linkLabel2
			// 
			this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabel2.Location = new System.Drawing.Point(131, 80);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(122, 20);
			this.linkLabel2.TabIndex = 5;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Huawei";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 20);
			this.label2.TabIndex = 6;
			this.label2.Text = "Manufacturer:";
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(122, 20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Device:";
			// 
			// lnkDeviceName
			// 
			this.lnkDeviceName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lnkDeviceName.Location = new System.Drawing.Point(131, 0);
			this.lnkDeviceName.Name = "lnkDeviceName";
			this.lnkDeviceName.Size = new System.Drawing.Size(122, 20);
			this.lnkDeviceName.TabIndex = 8;
			this.lnkDeviceName.TabStop = true;
			this.lnkDeviceName.Text = "Vodafone 845 (aka Joy)";
			this.lnkDeviceName.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabel1.Location = new System.Drawing.Point(131, 60);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(122, 20);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Vodafone";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(122, 20);
			this.label5.TabIndex = 11;
			this.label5.Text = "System Version:";
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Vendor:";
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(131, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(122, 20);
			this.label6.TabIndex = 12;
			this.label6.Text = "label6";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(17, 21);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(256, 256);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(310, 320);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(562, 317);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Description";
			// 
			// listView1
			// 
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
			this.listView1.LargeImageList = this.imlLargeIcons;
			this.listView1.Location = new System.Drawing.Point(310, 52);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(562, 256);
			this.listView1.TabIndex = 4;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// imlLargeIcons
			// 
			this.imlLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlLargeIcons.ImageStream")));
			this.imlLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlLargeIcons.Images.SetKeyName(0, "cmd.ico");
			this.imlLargeIcons.Images.SetKeyName(1, "Dialup.ico");
			this.imlLargeIcons.Images.SetKeyName(2, "Hard_Drive.ico");
			this.imlLargeIcons.Images.SetKeyName(3, "Journal.ico");
			this.imlLargeIcons.Images.SetKeyName(4, "Key.ico");
			this.imlLargeIcons.Images.SetKeyName(5, "Keys.ico");
			this.imlLargeIcons.Images.SetKeyName(6, "ram.ico");
			this.imlLargeIcons.Images.SetKeyName(7, "SecurityLock.ico");
			this.imlLargeIcons.Images.SetKeyName(8, "Settings.ico");
			this.imlLargeIcons.Images.SetKeyName(9, "Setup_Install.ico");
			this.imlLargeIcons.Images.SetKeyName(10, "smartmed.ico");
			// 
			// lsvDevices
			// 
			this.lsvDevices.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvDevices.FullRowSelect = true;
			this.lsvDevices.LargeImageList = this.imlLargeIcons;
			this.lsvDevices.Location = new System.Drawing.Point(0, 28);
			this.lsvDevices.Name = "lsvDevices";
			this.lsvDevices.Size = new System.Drawing.Size(884, 625);
			this.lsvDevices.SmallImageList = this.imlSmallIcons;
			this.lsvDevices.TabIndex = 5;
			this.lsvDevices.UseCompatibleStateImageBehavior = false;
			this.lsvDevices.View = System.Windows.Forms.View.Details;
			// 
			// imlSmallIcons
			// 
			this.imlSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 628);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(884, 25);
			this.statusStrip1.TabIndex = 6;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tslStatus
			// 
			this.tslStatus.Name = "tslStatus";
			this.tslStatus.Size = new System.Drawing.Size(50, 20);
			this.tslStatus.Text = "Ready";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 653);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.lsvDevices);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.mnuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuStrip;
			this.Name = "MainForm";
			this.Text = "Second Law";
			this.mnuStrip.ResumeLayout(false);
			this.mnuStrip.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewlogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem systemInformationToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem rebootToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recoveryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bootloaderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem powerOffToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.LinkLabel lnkDeviceName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ImageList imlLargeIcons;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolStripMenuItem mniFileScanForDevices;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem repositoriesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListView lsvDevices;
		private System.Windows.Forms.ImageList imlSmallIcons;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tslStatus;
	}
}

