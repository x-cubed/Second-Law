using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;
using System.Windows.Forms;
using Rectangle = System.Management.Automation.Host.Rectangle;
using Size = System.Management.Automation.Host.Size;

namespace SecondLaw.PowerShell {
	public partial class HostUI : Form {
		private readonly Font _defaultFont;
		private readonly Font _boldFont;

		private string _scriptName;
		private bool _closeRequested;

		public HostUI() {
			InitializeComponent();
			UI = new PowerShellUI(this);

			_defaultFont = lsvLog.Font;
			_boldFont = new Font(_defaultFont, FontStyle.Bold);
		}

		public void StartScript(IWin32Window owner, string scriptName) {
			prgProgress.Visible = false;
			butCancel.Visible = true;
			lsvLog.Items.Clear();

			_scriptName = scriptName;
			Text = _scriptName;
			Show(owner);
		}

		public void EndScript() {
			if (InvokeRequired) {
				Invoke(new Action(EndScript));
				return;
			}
			butCancel.Visible = false;
		}

		public new void Close() {
			_closeRequested = true;
			base.Close();
		}

		private void HostUI_FormClosing(object sender, FormClosingEventArgs e) {
			if (_closeRequested) {
				_closeRequested = false;
				return;
			}
			e.Cancel = true;
			Hide();
		}

		public PSHostUserInterface UI { get; private set; }

		private class PowerShellUI : PSHostUserInterface {
			private readonly HostUI _hostUI;
			private readonly PowerShellRawUI _rawUI;

			public PowerShellUI(HostUI hostUI) {
				_hostUI = hostUI;
				_rawUI = new PowerShellRawUI(hostUI);
			}

			public override string ReadLine() {
				throw new NotImplementedException();
			}

			public override SecureString ReadLineAsSecureString() {
				throw new NotImplementedException();
			}

			private void AddLogItem(ListViewItem item) {
				if (_hostUI.InvokeRequired) {
					_hostUI.Invoke(new Action(() => AddLogItem(item)));
					return;
				}
				Debug.Print("PowerShellUI.AddLogItem(): {0}", item.Text);
				_hostUI.lsvLog.Items.Add(item);
				_hostUI.lsvLog.EnsureVisible(_hostUI.lsvLog.Items.Count - 1);
			}

			public override void Write(string value) {
				AddLogItem(new ListViewItem(value));
			}

			private static Color ConsoleColorToColor(ConsoleColor console) {
				return Color.FromName(console.ToString());
			}

			public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value) {
				var item = new ListViewItem(value) {
					BackColor = ConsoleColorToColor(foregroundColor),
					ForeColor = ConsoleColorToColor(foregroundColor),
				};
				AddLogItem(item);
			}

			public override void WriteLine(string value) {
				AddLogItem(new ListViewItem(value));
			}

			public override void WriteErrorLine(string value) {
				AddLogItem(new ListViewItem(value) { ForeColor = Color.Red });
			}

			public override void WriteDebugLine(string value) {
				AddLogItem(new ListViewItem(value) { ForeColor = Color.Silver });
			}

			public override void WriteVerboseLine(string value) {
				AddLogItem(new ListViewItem(value) { ForeColor = Color.DarkGray });
			}

			public override void WriteWarningLine(string value) {
				AddLogItem(new ListViewItem(value) { ForeColor = Color.Orange });
			}

			public override void WriteProgress(long sourceId, ProgressRecord record) {
				if (_hostUI.InvokeRequired) {
					_hostUI.Invoke(new Action(() => WriteProgress(sourceId, record)));
					return;
				}
				
				AddLogItem(new ListViewItem(record.StatusDescription) {Font = _hostUI._boldFont});
				if (record.RecordType == ProgressRecordType.Completed) {
					// Activity is complete, reset to defaults
					_hostUI.Text = _hostUI._scriptName;
					_hostUI.prgProgress.Visible = false;
					_hostUI.prgProgress.Value = 0;
				} else {
					// Show the progress of the current activity
					_hostUI.Text = _hostUI._scriptName + " - " + record.Activity;
					_hostUI.prgProgress.Value = record.PercentComplete;
					_hostUI.prgProgress.Visible = true;
				}
			}

			public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions) {
				throw new NotImplementedException();
			}

			public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName) {
				throw new NotImplementedException();
			}

			public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options) {
				throw new NotImplementedException();
			}

			public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice) {
				throw new NotImplementedException();
			}

			public override PSHostRawUserInterface RawUI {
				get { return _rawUI; }
			}
		}

		private class PowerShellRawUI : PSHostRawUserInterface {
			private readonly HostUI _hostUI;
			private readonly Size _mockSize = new Size(120, 50);

			public PowerShellRawUI(HostUI hostUI) {
				_hostUI = hostUI;
			}

			public override KeyInfo ReadKey(ReadKeyOptions options) {
				throw new NotImplementedException();
			}

			public override void FlushInputBuffer() {
			}

			public override void SetBufferContents(Coordinates origin, BufferCell[,] contents) {
			}

			public override void SetBufferContents(Rectangle rectangle, BufferCell fill) {
			}

			public override BufferCell[,] GetBufferContents(Rectangle rectangle) {
				throw new NotImplementedException();
			}

			public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill) {
			}

			public override ConsoleColor ForegroundColor {
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public override ConsoleColor BackgroundColor {
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public override Coordinates CursorPosition {
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public override Coordinates WindowPosition {
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public override int CursorSize {
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public override Size BufferSize {
				get { return _mockSize; }
				set {  }
			}

			public override Size WindowSize {
				get { return _mockSize; }
				set {  }
			}

			public override Size MaxWindowSize {
				get { return _mockSize; }
			}

			public override Size MaxPhysicalWindowSize {
				get { return _mockSize; }
			}

			public override bool KeyAvailable {
				get { return false; }
			}

			public override string WindowTitle {
				get { return _hostUI.Text; }
				set { _hostUI.Text = value; }
			}
		}
	}
}
