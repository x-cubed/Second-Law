using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using SessionVariable = System.Collections.Generic.KeyValuePair<string, object>;

namespace SecondLaw {
	public static class PowerShell {
		public static string Run(string scriptText, IEnumerable<SessionVariable> sessionVariables = null) {
			Collection<PSObject> results;
			using (Runspace runspace = RunspaceFactory.CreateRunspace()) {
				runspace.ApartmentState = ApartmentState.STA;
				runspace.Open();

				// Pass variables into the script
				if (sessionVariables != null) {
					foreach (var variable in sessionVariables) {
						runspace.SessionStateProxy.SetVariable(variable.Key, variable.Value);
					}
				}

				// create a pipeline and feed it the script text
				Pipeline pipeline = runspace.CreatePipeline();
				pipeline.Commands.AddScript(scriptText);

				// add an extra command to transform the script
				// output objects into nicely formatted strings

				// remove this line to get the actual objects
				// that the script returns. For example, the script

				// "Get-Process" returns a collection
				// of System.Diagnostics.Process instances.
				pipeline.Commands.Add("Out-String");

				// execute the script
				results = pipeline.Invoke();

				// close the runspace
				runspace.Close();
			}

			// convert the script result into a single string
			var stringBuilder = new StringBuilder();
			foreach (PSObject obj in results) {
				stringBuilder.AppendLine(obj.ToString());
			}

			return stringBuilder.ToString();
		}

		public static string Run(FileInfo scriptFile, IEnumerable<SessionVariable> sessionVariables = null) {
			string scriptText;
			using (var reader = scriptFile.OpenText()) {
				scriptText = reader.ReadToEnd();
			}
			return Run(scriptText, sessionVariables);
		}

		public static string Run(FileInfo scriptFile, params SessionVariable[] sessionVariables) {
			return Run(scriptFile, (IEnumerable<SessionVariable>)sessionVariables);
		}
	}
}
