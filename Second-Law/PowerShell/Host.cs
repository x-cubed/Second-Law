using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SecondLaw.PowerShell {
	/// <summary>
	/// Provides a popup window for running PowerShell scripts in, that supports
	/// logging of messages and progress.
	/// </summary>
	class Host : PSHost, IDisposable {
		private readonly IWin32Window _owner;
		private readonly Guid _instanceID;
		private readonly Runspace _runspace;
		private readonly HostUI _hostUI;

		private Pipeline _pipeline;

		public Host(IWin32Window owner) {
			_owner = owner;
			_instanceID = Guid.NewGuid();
			_hostUI = new HostUI();

			_runspace = RunspaceFactory.CreateRunspace(this);
			_runspace.ApartmentState = ApartmentState.STA;
			_runspace.Open();
		}

		public void RunAsync(string title, string scriptText, Action<PipelineStateInfo> callback, IEnumerable<KeyValuePair<string, object>> sessionVariables = null) {
			if (_pipeline != null) {
				throw new InvalidOperationException("Another script is already running");
			}

			// Pass variables into the script
			if (sessionVariables != null) {
				foreach (var variable in sessionVariables) {
					_runspace.SessionStateProxy.SetVariable(variable.Key, variable.Value);
				}
			}

			// create a pipeline and feed it the script text
			_pipeline = _runspace.CreatePipeline(scriptText);
			_pipeline.Commands.Add("Out-Default");

			// Hook up event handling for when the pipeline ends			
			EventHandler<PipelineStateEventArgs> stateChanged = null;
			stateChanged = (sender, args) => {
			  Debug.Print("Host.Run.StateChanged(): {0}", args.PipelineStateInfo.State);
				switch (args.PipelineStateInfo.State) {
					case PipelineState.Stopped:
					case PipelineState.Completed:
					case PipelineState.Failed:
						_pipeline.StateChanged -= stateChanged;
						_pipeline.Dispose();
						_pipeline = null;

						_hostUI.EndScript();
						callback(args.PipelineStateInfo);
						break;
				}
			};
			_pipeline.StateChanged += stateChanged;

			// Show the form and execute the script
			_hostUI.StartScript(_owner, title);
			_pipeline.InvokeAsync();
		}

		public void RunAsync(string title, FileInfo scriptFile, Action<PipelineStateInfo> callback, 
			IEnumerable<KeyValuePair<string, object>> sessionVariables = null) {
			string scriptText;
			using (var reader = scriptFile.OpenText()) {
				scriptText = reader.ReadToEnd();
			}
			RunAsync(title, scriptText, callback, sessionVariables);
		}

		public void RunAsync(string title, FileInfo scriptFile, Action<PipelineStateInfo> callback, 
			params KeyValuePair<string, object>[] sessionVariables) {
			RunAsync(title, scriptFile, callback, (IEnumerable<KeyValuePair<string, object>>)sessionVariables);
		}

		public override void SetShouldExit(int exitCode) {
			throw new NotImplementedException();
		}

		public override void EnterNestedPrompt() {
			throw new NotImplementedException();
		}

		public override void ExitNestedPrompt() {
			throw new NotImplementedException();
		}

		public override void NotifyBeginApplication() {
		}

		public override void NotifyEndApplication() {
		}

		public override string Name {
			get {
				var assembly = Assembly.GetExecutingAssembly();
				var assemblyTitle = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
				return assemblyTitle.Title;
			}
		}

		public override Version Version {
			get {
				var assembly = Assembly.GetExecutingAssembly();
				return assembly.GetName().Version;
			}
		}

		public override Guid InstanceId {
			get { return _instanceID; }
		}

		public override PSHostUserInterface UI {
			get { return _hostUI.UI; }
		}

		public override CultureInfo CurrentCulture {
			get { return CultureInfo.CurrentCulture; }
		}

		public override CultureInfo CurrentUICulture {
			get { return CultureInfo.CurrentUICulture; }
		}

		public void Dispose() {
			_runspace.Dispose();
		}
	}
}
