using Microsoft.Extensions.Configuration;
using System.Diagnostics;

class Program
{
	static void Main()
	{
		string solutionDir =
			Environment.GetEnvironmentVariable("SOLUTION_DIR") ??
			Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName ?? 
			throw new InvalidOperationException("Solution directory could not be determined.");

		string projectPath = Path.Combine(solutionDir, "Minibox.SQLDatabase", "Minibox.SQLDatabase.sqlproj");
		string outputPath = Path.Combine(solutionDir, "Minibox.Database.Deployer", "output");

		if (!Directory.Exists(outputPath))
			Directory.CreateDirectory(outputPath);

		Console.WriteLine("--Building DACPAC...");

		string msbuildCmd = $"msbuild \"{projectPath}\" /p:Configuration=Release /p:OutputPath={outputPath}";
		RunCommand(msbuildCmd);

		string dacpacPath = Path.Combine(outputPath, "Minibox.SQLDatabase.dacpac");

		if (File.Exists(dacpacPath))
		{
			Console.WriteLine($"--DACPAC built successfully: {dacpacPath}");
		}
		else
		{
			Console.WriteLine("--DACPAC build failed!");
			return;
		}

		// Deploy DACPAC
		DeployDatabase(dacpacPath);

		// Delete DACPAC
		if (File.Exists(dacpacPath))
		{
			Console.WriteLine($"Deleting DACPAC file: {dacpacPath}");
			File.Delete(dacpacPath);
		}
	}

	static void DeployDatabase(string dacpacPath)
	{
		Console.WriteLine("--Deploying DACPAC to SQL Server...");

		var config = LoadConfiguration();
		string server = config["DatabaseConfig:Server"] ?? string.Empty;
		string database = config["DatabaseConfig:Database"] ?? string.Empty;
		string user = config["DatabaseConfig:User"] ?? string.Empty;
		string password = config["DatabaseConfig:Password"] ?? string.Empty;

		string sqlpackageCmd = $"sqlpackage /Action:Publish /SourceFile:\"{dacpacPath}\" " +
						   $"/TargetServerName:{server} /TargetDatabaseName:{database} " +
						   $"/TargetUser:{user} /TargetPassword:{password} " +
						   $"/TargetTrustServerCertificate:True";

		RunCommand(sqlpackageCmd);
	}

	static void RunCommand(string command)
	{
		Process process = new();

		bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

		if (isWindows)
		{
			process.StartInfo.FileName = "cmd.exe";
			process.StartInfo.Arguments = $"/C {command}";
		}
		else
		{
			process.StartInfo.FileName = "/bin/bash";
			process.StartInfo.Arguments = $"-c \"{command}\"";
		}

		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;

		process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
		process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

		process.Start();
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();
		process.WaitForExit();
	}

	static IConfiguration LoadConfiguration()
	{
		var configBuilder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

		return configBuilder.Build();
	}
}
