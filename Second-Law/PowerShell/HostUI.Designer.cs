namespace SecondLaw.PowerShell {
	partial class HostUI {
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
			this.lsvLog = new System.Windows.Forms.ListView();
			this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.prgProgress = new System.Windows.Forms.ProgressBar();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lsvLog
			// 
			this.lsvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessage});
			this.lsvLog.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lsvLog.FullRowSelect = true;
			this.lsvLog.Location = new System.Drawing.Point(12, 12);
			this.lsvLog.Name = "lsvLog";
			this.lsvLog.Size = new System.Drawing.Size(771, 330);
			this.lsvLog.TabIndex = 0;
			this.lsvLog.UseCompatibleStateImageBehavior = false;
			this.lsvLog.View = System.Windows.Forms.View.Details;
			// 
			// colMessage
			// 
			this.colMessage.Text = "Message";
			this.colMessage.Width = 600;
			// 
			// prgProgress
			// 
			this.prgProgress.Location = new System.Drawing.Point(12, 352);
			this.prgProgress.Name = "prgProgress";
			this.prgProgress.Size = new System.Drawing.Size(690, 26);
			this.prgProgress.TabIndex = 1;
			this.prgProgress.Visible = false;
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Enabled = false;
			this.butCancel.Location = new System.Drawing.Point(708, 352);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			// 
			// HostUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 390);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.prgProgress);
			this.Controls.Add(this.lsvLog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HostUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "HostUI";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HostUI_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lsvLog;
		private System.Windows.Forms.ColumnHeader colMessage;
		private System.Windows.Forms.ProgressBar prgProgress;
		private System.Windows.Forms.Button butCancel;
	}
}