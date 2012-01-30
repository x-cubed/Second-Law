namespace SecondLaw {
	partial class UsbDeviceList {
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
			this.lsvDevices = new System.Windows.Forms.ListView();
			this.imlSmall = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lsvDevices
			// 
			this.lsvDevices.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvDevices.FullRowSelect = true;
			this.lsvDevices.Location = new System.Drawing.Point(0, 0);
			this.lsvDevices.Name = "lsvDevices";
			this.lsvDevices.Size = new System.Drawing.Size(282, 255);
			this.lsvDevices.SmallImageList = this.imlSmall;
			this.lsvDevices.TabIndex = 6;
			this.lsvDevices.UseCompatibleStateImageBehavior = false;
			this.lsvDevices.View = System.Windows.Forms.View.Details;
			// 
			// imlSmall
			// 
			this.imlSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlSmall.ImageSize = new System.Drawing.Size(16, 16);
			this.imlSmall.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// UsbDeviceList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 255);
			this.Controls.Add(this.lsvDevices);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "UsbDeviceList";
			this.Text = "USB Devices";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UsbDeviceList_FormClosed);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lsvDevices;
		private System.Windows.Forms.ImageList imlSmall;
	}
}