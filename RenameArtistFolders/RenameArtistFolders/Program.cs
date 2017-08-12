using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      Manager manager = new Manager(input, true);
      IEnumerable<string> artistFolders = manager.ScanFolder();
    }
  }

  public class Manager {
    private readonly string _rootPath;
    private readonly bool _skipFirstLevel;

    public Manager(string rootPath, bool skipFirstLevel) {
      this._rootPath = rootPath;
      this._skipFirstLevel = skipFirstLevel;
    }

    public IEnumerable<string> ScanFolder() {
      List<string> result = new List<string>();
      string[] directories = Directory.GetDirectories(this._rootPath);
      if (this._skipFirstLevel) {
        foreach (string directory in directories) {
          string[] artistDirectories = Directory.GetDirectories(directory);
          result.AddRange(artistDirectories);
        }
        return result;
      } else {
        throw new NotImplementedException("Not skipping the first level is not implemented yet");
      }
    }
  }
}
