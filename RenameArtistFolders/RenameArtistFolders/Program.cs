using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NoobyGames.Logging;

namespace RenameArtistFolders {
  public class Program {
    static void Main(string[] args) {
      Console.WriteLine("### RenameArtistFolders ###");
      Console.WriteLine(@"Please insert the root path. (Like C:\foo\music)");
      string input = Console.ReadLine();
      Console.WriteLine("Checking directory..");
      while (!Directory.Exists(input)) {
        Console.WriteLine(@"Please insert the root path. (Like C:\foo\music)");
        input = Console.ReadLine();
      }
      Console.WriteLine("Directory found..");
      string assemblyDirectory = GetAssemblyDirectory();
      Logger logger = new Logger(assemblyDirectory + @"\log.txt");
      Worker worker = new Worker(input, true, logger);
     
      IEnumerable<string> artistFolders = worker.ScanFolder();
      logger.Log($"Found {artistFolders.Count()} folders");
      IEnumerable<string> filteredFolders = worker.FilterFolders(artistFolders);
      logger.Log($"Found {filteredFolders.Count()} folders after filtering");
    }

    private static string GetAssemblyDirectory() {
      string codeBase = Assembly.GetCallingAssembly().CodeBase;
      UriBuilder uri = new UriBuilder(codeBase);
      string path = Uri.UnescapeDataString(uri.Path);
      return Path.GetDirectoryName(path);
    }
  }

  public class Worker {
    private readonly string _rootPath;
    private readonly bool _skipFirstLevel;
    private readonly Logger _logger;

    public Worker(string rootPath, bool skipFirstLevel, Logger logger) {
      this._rootPath = rootPath;
      this._skipFirstLevel = skipFirstLevel;
      this._logger = logger;
    }

    public IEnumerable<string> ScanFolder() {
      List<string> result = new List<string>();
      string[] directories = Directory.GetDirectories(this._rootPath);
      if (this._skipFirstLevel) {
        foreach (string directory in directories) {
          try {
            string[] artistDirectories = Directory.GetDirectories(directory);
            result.AddRange(artistDirectories);
          }
          catch (Exception e) {
            this._logger.Log($"Failed to parse directory : {directory}");
            this._logger.Log(e.Message);
          }
         
        }
        return result;
      } else {
        throw new NotImplementedException("Not skipping the first level is not implemented yet");
      }
    }

    public IEnumerable<string> FilterFolders(IEnumerable<string> artistFolders) {
      ICollection<string> result = new List<string>();
      foreach (string artistFolder in artistFolders) {
        if (artistFolder.Contains("[")) {
          continue;
        }
        result.Add(artistFolder);
      }
      return result;
    }
  }
}
