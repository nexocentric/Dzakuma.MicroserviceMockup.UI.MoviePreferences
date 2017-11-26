using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using NLog;

namespace Dzakuma.MicroserviceMockup.UI.MoviePreferences
{
	public class InterprocessDataSiphon
	{
		private static Logger _internalLogger = LogManager.GetCurrentClassLogger();

		public (JObject individualData, JArray personnelList) DeserializeJsonString(string executablePath, params string[] arguments)
		{
			var data = DecodeProcessString(executablePath, arguments);

			return (SafeDeserializeJsonObject(data), SafeDeserializeJsonArray(data));
		}

		public JObject SafeDeserializeJsonObject(string jsonString)
		{
			try
			{
				return JObject.Parse(jsonString);
			}
			catch (Exception anomaly)
			{
				_internalLogger.Trace(anomaly, "The selected object is not a JSON object.");
				return null;
			}
		}

		public JArray SafeDeserializeJsonArray(string jsonString)
		{
			try
			{
				return JArray.Parse(jsonString);
			}
			catch (Exception anomaly)
			{
				_internalLogger.Trace(anomaly, "The selected object is not a JSON array.");
				return null;
			}
		}

		public string DecodeString(string executablePath, params string[] arguments)
		{
			return DecodeProcessString(executablePath, arguments);
		}

		public string DecodeProcessString(string executablePath, params string[] arguments)
		{
			return TransportSafeString.Decode(ReadDataFromProcess(executablePath, arguments));
		}

		public string ReadDataFromProcess(string executablePath, params string[] arguments)
		{
			var testProgram = new Process();
			testProgram.StartInfo.FileName = executablePath;
			testProgram.StartInfo.Arguments = string.Join(" ", arguments);
			testProgram.StartInfo.CreateNoWindow = true;
			testProgram.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			testProgram.StartInfo.UseShellExecute = false;
			testProgram.StartInfo.RedirectStandardError = true;
			testProgram.Start();
			testProgram.WaitForExit(6000);

			var standardErrorOutput = testProgram.StandardError.ReadToEnd().Trim();

			if (testProgram.ExitCode != 0)
			{
				_internalLogger.Warn($"Call to [{executablePath}] did not complete cleanly. Exit Code: [{testProgram.ExitCode}]");
			}

			return standardErrorOutput;
		}
	}
}
