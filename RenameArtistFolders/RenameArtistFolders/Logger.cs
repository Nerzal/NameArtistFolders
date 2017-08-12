using System;
using System.IO;

namespace NoobyGames.Logging {
  public class Logger {
    public string FileName { get; set; }

    public Logger(string fileName) {
      this.FileName = fileName;
    }

    public void Initialize() {
      if (!File.Exists(this.FileName)) {
        File.Create(this.FileName);
      }
    }

    public void Log(string message) {
      using (FileStream stream = new FileStream(this.FileName, FileMode.Append, FileAccess.Write)) {
        using (StreamWriter writer = new StreamWriter(stream)) {
          writer.WriteLine($"{DateTime.Now:g}: {message}");
        }
      }
    }
  }
}
