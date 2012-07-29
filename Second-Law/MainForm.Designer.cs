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
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Gain root access", "SecurityLock.ico");
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Install TWRP Recovery", "ram.ico");
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Install CM 7", "Setup_Install.ico");
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Install CM 9", "Setup_Install.ico");
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Setup USB Tether", "Dialup.ico");
			this.mnuStrip = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mniFileScanForDevices = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDevice = new System.Windows.Forms.ToolStripMenuItem();
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
			this.mnuPackages = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imlLargeIcons = new System.Windows.Forms.ImageList(this.components);
			this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlScanning = new System.Windows.Forms.Panel();
			this.lblInstructions = new System.Windows.Forms.Label();
			this.prgScanning = new System.Windows.Forms.ProgressBar();
			this.lblSecondLaw = new System.Windows.Forms.Label();
			this.pnlDevice = new System.Windows.Forms.TableLayoutPanel();
			this.grpDescription = new System.Windows.Forms.GroupBox();
			this.lsvScripts = new System.Windows.Forms.ListView();
			this.grpDeviceInformation = new System.Windows.Forms.GroupBox();
			this.tblDeviceProperties = new System.Windows.Forms.TableLayoutPanel();
			this.lblSystemVersion = new System.Windows.Forms.Label();
			this.lbdSerialNumber = new System.Windows.Forms.Label();
			this.lnkManufacturer = new System.Windows.Forms.LinkLabel();
			this.lbdManufacturer = new System.Windows.Forms.Label();
			this.lbdDeviceName = new System.Windows.Forms.Label();
			this.lnkDeviceName = new System.Windows.Forms.LinkLabel();
			this.lnkVendor = new System.Windows.Forms.LinkLabel();
			this.lbdSystemVersion = new System.Windows.Forms.Label();
			this.lbdVendor = new System.Windows.Forms.Label();
			this.lblSerialNumber = new System.Windows.Forms.Label();
			this.lbdBatteryCharge = new System.Windows.Forms.Label();
			this.prgBatteryCharge = new System.Windows.Forms.ProgressBar();
			this.picDevice = new System.Windows.Forms.PictureBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.mnuStrip.SuspendLayout();
			this.statusBar.SuspendLayout();
			this.pnlScanning.SuspendLayout();
			this.pnlDevice.SuspendLayout();
			this.grpDeviceInformation.SuspendLayout();
			this.tblDeviceProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picDevice)).BeginInit();
			this.SuspendLayout();
			// 
			// mnuStrip
			// 
			this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuDevice,
            this.mnuPackages,
            this.mnuHelp});
			this.mnuStrip.Location = new System.Drawing.Point(0, 0);
			this.mnuStrip.Name = "mnuStrip";
			this.mnuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.mnuStrip.Size = new System.Drawing.Size(934, 28);
			this.mnuStrip.TabIndex = 0;
			this.mnuStrip.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFileScanForDevices,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(44, 24);
			this.mnuFile.Text = "&File";
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
			// mnuDevice
			// 
			this.mnuDevice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewlogToolStripMenuItem,
            this.browseToolStripMenuItem,
            this.toolStripMenuItem1,
            this.systemInformationToolStripMenuItem,
            this.toolStripMenuItem2,
            this.rebootToolStripMenuItem,
            this.powerOffToolStripMenuItem});
			this.mnuDevice.Name = "mnuDevice";
			this.mnuDevice.Size = new System.Drawing.Size(66, 24);
			this.mnuDevice.Text = "&Device";
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
			// mnuPackages
			// 
			this.mnuPackages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripMenuItem4,
            this.addToolStripMenuItem});
			this.mnuPackages.Name = "mnuPackages";
			this.mnuPackages.Size = new System.Drawing.Size(82, 24);
			this.mnuPackages.Text = "&Packages";
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
			// mnuHelp
			// 
			this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Size = new System.Drawing.Size(53, 24);
			this.mnuHelp.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
			this.aboutToolStripMenuItem.Text = "&About";
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
			// imlSmallIcons
			// 
			this.imlSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
			this.statusBar.Location = new System.Drawing.Point(0, 589);
			this.statusBar.Name = "statusBar";
			this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
			this.statusBar.Size = new System.Drawing.Size(934, 25);
			this.statusBar.TabIndex = 6;
			this.statusBar.Text = "statusStrip1";
			// 
			// tslStatus
			// 
			this.tslStatus.Name = "tslStatus";
			this.tslStatus.Size = new System.Drawing.Size(50, 20);
			this.tslStatus.Text = "Ready";
			// 
			// pnlScanning
			// 
			this.pnlScanning.Controls.Add(this.lblInstructions);
			this.pnlScanning.Controls.Add(this.prgScanning);
			this.pnlScanning.Controls.Add(this.lblSecondLaw);
			this.pnlScanning.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlScanning.Location = new System.Drawing.Point(0, 0);
			this.pnlScanning.Name = "pnlScanning";
			this.pnlScanning.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.pnlScanning.Size = new System.Drawing.Size(934, 614);
			this.pnlScanning.TabIndex = 7;
			// 
			// lblInstructions
			// 
			this.lblInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInstructions.Location = new System.Drawing.Point(3, 198);
			this.lblInstructions.Name = "lblInstructions";
			this.lblInstructions.Size = new System.Drawing.Size(928, 135);
			this.lblInstructions.TabIndex = 2;
			this.lblInstructions.Text = "Connect your device when ready\r\n\r\nMake sure that USB debugging is enabled.\r\nThis " +
    "option is under Settings -> Applications -> Development.";
			this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// prgScanning
			// 
			this.prgScanning.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.prgScanning.Location = new System.Drawing.Point(395, 153);
			this.prgScanning.Name = "prgScanning";
			this.prgScanning.Size = new System.Drawing.Size(149, 11);
			this.prgScanning.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.prgScanning.TabIndex = 1;
			this.prgScanning.Visible = false;
			// 
			// lblSecondLaw
			// 
			this.lblSecondLaw.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblSecondLaw.Font = new System.Drawing.Font("Segoe UI Semibold", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSecondLaw.Location = new System.Drawing.Point(0, 30);
			this.lblSecondLaw.Name = "lblSecondLaw";
			this.lblSecondLaw.Size = new System.Drawing.Size(934, 79);
			this.lblSecondLaw.TabIndex = 0;
			this.lblSecondLaw.Text = "Second Law";
			this.lblSecondLaw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlDevice
			// 
			this.pnlDevice.ColumnCount = 2;
			this.pnlDevice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262F));
			this.pnlDevice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.pnlDevice.Controls.Add(this.grpDescription, 1, 1);
			this.pnlDevice.Controls.Add(this.lsvScripts, 1, 0);
			this.pnlDevice.Controls.Add(this.grpDeviceInformation, 0, 0);
			this.pnlDevice.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlDevice.Location = new System.Drawing.Point(0, 0);
			this.pnlDevice.Name = "pnlDevice";
			this.pnlDevice.Padding = new System.Windows.Forms.Padding(0, 25, 0, 25);
			this.pnlDevice.RowCount = 2;
			this.pnlDevice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.pnlDevice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 212F));
			this.pnlDevice.Size = new System.Drawing.Size(934, 614);
			this.pnlDevice.TabIndex = 10;
			this.pnlDevice.Visible = false;
			// 
			// grpDescription
			// 
			this.grpDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpDescription.Location = new System.Drawing.Point(265, 380);
			this.grpDescription.Name = "grpDescription";
			this.grpDescription.Size = new System.Drawing.Size(666, 206);
			this.grpDescription.TabIndex = 12;
			this.grpDescription.TabStop = false;
			this.grpDescription.Text = "Description";
			// 
			// lsvScripts
			// 
			this.lsvScripts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvScripts.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
			this.lsvScripts.LargeImageList = this.imlLargeIcons;
			this.lsvScripts.Location = new System.Drawing.Point(265, 50);
			this.lsvScripts.Margin = new System.Windows.Forms.Padding(3, 25, 3, 3);
			this.lsvScripts.Name = "lsvScripts";
			this.lsvScripts.Size = new System.Drawing.Size(666, 324);
			this.lsvScripts.TabIndex = 11;
			this.lsvScripts.UseCompatibleStateImageBehavior = false;
			this.lsvScripts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvScripts_MouseDoubleClick);
			// 
			// grpDeviceInformation
			// 
			this.grpDeviceInformation.Controls.Add(this.tblDeviceProperties);
			this.grpDeviceInformation.Controls.Add(this.picDevice);
			this.grpDeviceInformation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpDeviceInformation.Location = new System.Drawing.Point(3, 28);
			this.grpDeviceInformation.Name = "grpDeviceInformation";
			this.pnlDevice.SetRowSpan(this.grpDeviceInformation, 2);
			this.grpDeviceInformation.Size = new System.Drawing.Size(256, 558);
			this.grpDeviceInformation.TabIndex = 9;
			this.grpDeviceInformation.TabStop = false;
			// 
			// tblDeviceProperties
			// 
			this.tblDeviceProperties.ColumnCount = 2;
			this.tblDeviceProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblDeviceProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblDeviceProperties.Controls.Add(this.lblSystemVersion, 1, 2);
			this.tblDeviceProperties.Controls.Add(this.lbdSerialNumber, 0, 1);
			this.tblDeviceProperties.Controls.Add(this.lnkManufacturer, 1, 4);
			this.tblDeviceProperties.Controls.Add(this.lbdManufacturer, 0, 4);
			this.tblDeviceProperties.Controls.Add(this.lbdDeviceName, 0, 0);
			this.tblDeviceProperties.Controls.Add(this.lnkDeviceName, 1, 0);
			this.tblDeviceProperties.Controls.Add(this.lnkVendor, 1, 3);
			this.tblDeviceProperties.Controls.Add(this.lbdSystemVersion, 0, 2);
			this.tblDeviceProperties.Controls.Add(this.lbdVendor, 0, 3);
			this.tblDeviceProperties.Controls.Add(this.lblSerialNumber, 1, 1);
			this.tblDeviceProperties.Controls.Add(this.lbdBatteryCharge, 0, 5);
			this.tblDeviceProperties.Controls.Add(this.prgBatteryCharge, 1, 5);
			this.tblDeviceProperties.Location = new System.Drawing.Point(15, 307);
			this.tblDeviceProperties.Name = "tblDeviceProperties";
			this.tblDeviceProperties.RowCount = 7;
			this.tblDeviceProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tblDeviceProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tblDeviceProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tblDeviceProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tblDeviceProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tblDeviceProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tblDeviceProperties.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDeviceProperties.Size = new System.Drawing.Size(224, 238);
			this.tblDeviceProperties.TabIndex = 9;
			// 
			// lblSystemVersion
			// 
			this.lblSystemVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblSystemVersion.Location = new System.Drawing.Point(116, 42);
			this.lblSystemVersion.Name = "lblSystemVersion";
			this.lblSystemVersion.Size = new System.Drawing.Size(105, 21);
			this.lblSystemVersion.TabIndex = 13;
			this.lblSystemVersion.Text = "(Unknown)";
			this.lblSystemVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbdSerialNumber
			// 
			this.lbdSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbdSerialNumber.Location = new System.Drawing.Point(3, 21);
			this.lbdSerialNumber.Name = "lbdSerialNumber";
			this.lbdSerialNumber.Size = new System.Drawing.Size(107, 21);
			this.lbdSerialNumber.TabIndex = 10;
			this.lbdSerialNumber.Text = "Serial:";
			// 
			// lnkManufacturer
			// 
			this.lnkManufacturer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lnkManufacturer.Location = new System.Drawing.Point(116, 84);
			this.lnkManufacturer.Name = "lnkManufacturer";
			this.lnkManufacturer.Size = new System.Drawing.Size(105, 21);
			this.lnkManufacturer.TabIndex = 5;
			this.lnkManufacturer.TabStop = true;
			this.lnkManufacturer.Text = "Huawei";
			this.lnkManufacturer.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbdManufacturer
			// 
			this.lbdManufacturer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbdManufacturer.Location = new System.Drawing.Point(3, 84);
			this.lbdManufacturer.Name = "lbdManufacturer";
			this.lbdManufacturer.Size = new System.Drawing.Size(107, 21);
			this.lbdManufacturer.TabIndex = 6;
			this.lbdManufacturer.Text = "Manufacturer:";
			// 
			// lbdDeviceName
			// 
			this.lbdDeviceName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbdDeviceName.Location = new System.Drawing.Point(3, 0);
			this.lbdDeviceName.Name = "lbdDeviceName";
			this.lbdDeviceName.Size = new System.Drawing.Size(107, 21);
			this.lbdDeviceName.TabIndex = 7;
			this.lbdDeviceName.Text = "Device:";
			// 
			// lnkDeviceName
			// 
			this.lnkDeviceName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lnkDeviceName.Location = new System.Drawing.Point(116, 0);
			this.lnkDeviceName.Name = "lnkDeviceName";
			this.lnkDeviceName.Size = new System.Drawing.Size(105, 21);
			this.lnkDeviceName.TabIndex = 8;
			this.lnkDeviceName.TabStop = true;
			this.lnkDeviceName.Text = "Vodafone 845 (aka Joy)";
			this.lnkDeviceName.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.lnkDeviceName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_Clicked);
			// 
			// lnkVendor
			// 
			this.lnkVendor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lnkVendor.Location = new System.Drawing.Point(116, 63);
			this.lnkVendor.Name = "lnkVendor";
			this.lnkVendor.Size = new System.Drawing.Size(105, 21);
			this.lnkVendor.TabIndex = 4;
			this.lnkVendor.TabStop = true;
			this.lnkVendor.Text = "Vodafone";
			this.lnkVendor.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbdSystemVersion
			// 
			this.lbdSystemVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbdSystemVersion.Location = new System.Drawing.Point(3, 42);
			this.lbdSystemVersion.Name = "lbdSystemVersion";
			this.lbdSystemVersion.Size = new System.Drawing.Size(107, 21);
			this.lbdSystemVersion.TabIndex = 11;
			this.lbdSystemVersion.Text = "OS Version:";
			// 
			// lbdVendor
			// 
			this.lbdVendor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbdVendor.Location = new System.Drawing.Point(3, 63);
			this.lbdVendor.Name = "lbdVendor";
			this.lbdVendor.Size = new System.Drawing.Size(107, 21);
			this.lbdVendor.TabIndex = 3;
			this.lbdVendor.Text = "Vendor:";
			// 
			// lblSerialNumber
			// 
			this.lblSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblSerialNumber.Location = new System.Drawing.Point(116, 21);
			this.lblSerialNumber.Name = "lblSerialNumber";
			this.lblSerialNumber.Size = new System.Drawing.Size(105, 21);
			this.lblSerialNumber.TabIndex = 12;
			this.lblSerialNumber.Text = "(Unknown)";
			this.lblSerialNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbdBatteryCharge
			// 
			this.lbdBatteryCharge.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbdBatteryCharge.Location = new System.Drawing.Point(3, 105);
			this.lbdBatteryCharge.Name = "lbdBatteryCharge";
			this.lbdBatteryCharge.Size = new System.Drawing.Size(107, 21);
			this.lbdBatteryCharge.TabIndex = 14;
			this.lbdBatteryCharge.Text = "Battery:";
			// 
			// prgBatteryCharge
			// 
			this.prgBatteryCharge.Dock = System.Windows.Forms.DockStyle.Fill;
			this.prgBatteryCharge.Location = new System.Drawing.Point(116, 108);
			this.prgBatteryCharge.Name = "prgBatteryCharge";
			this.prgBatteryCharge.Size = new System.Drawing.Size(105, 15);
			this.prgBatteryCharge.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.prgBatteryCharge.TabIndex = 15;
			// 
			// picDevice
			// 
			this.picDevice.BackColor = System.Drawing.Color.White;
			this.picDevice.Location = new System.Drawing.Point(15, 22);
			this.picDevice.Name = "picDevice";
			this.picDevice.Size = new System.Drawing.Size(224, 272);
			this.picDevice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picDevice.TabIndex = 2;
			this.picDevice.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(934, 614);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.mnuStrip);
			this.Controls.Add(this.pnlDevice);
			this.Controls.Add(this.pnlScanning);
			this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuStrip;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Second Law";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.mnuStrip.ResumeLayout(false);
			this.mnuStrip.PerformLayout();
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.pnlScanning.ResumeLayout(false);
			this.pnlDevice.ResumeLayout(false);
			this.grpDeviceInformation.ResumeLayout(false);
			this.tblDeviceProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picDevice)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnuStrip;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuDevice;
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
		private System.Windows.Forms.ToolStripMenuItem mnuHelp;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ImageList imlLargeIcons;
		private System.Windows.Forms.ToolStripMenuItem mniFileScanForDevices;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem mnuPackages;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ImageList imlSmallIcons;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel tslStatus;
		private System.Windows.Forms.Panel pnlScanning;
		private System.Windows.Forms.ProgressBar prgScanning;
		private System.Windows.Forms.Label lblSecondLaw;
		private System.Windows.Forms.Label lblInstructions;
		private System.Windows.Forms.TableLayoutPanel pnlDevice;
		private System.Windows.Forms.GroupBox grpDescription;
		private System.Windows.Forms.ListView lsvScripts;
		private System.Windows.Forms.GroupBox grpDeviceInformation;
		private System.Windows.Forms.TableLayoutPanel tblDeviceProperties;
		private System.Windows.Forms.Label lblSystemVersion;
		private System.Windows.Forms.Label lbdSerialNumber;
		private System.Windows.Forms.LinkLabel lnkManufacturer;
		private System.Windows.Forms.Label lbdManufacturer;
		private System.Windows.Forms.Label lbdDeviceName;
		private System.Windows.Forms.LinkLabel lnkDeviceName;
		private System.Windows.Forms.LinkLabel lnkVendor;
		private System.Windows.Forms.Label lbdSystemVersion;
		private System.Windows.Forms.Label lbdVendor;
		private System.Windows.Forms.Label lblSerialNumber;
		private System.Windows.Forms.PictureBox picDevice;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label lbdBatteryCharge;
		private System.Windows.Forms.ProgressBar prgBatteryCharge;
	}
}

