using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace RenameArtistFolders {
  /// <summary>
  /// Fetches data from spirit of metal
  /// </summary>
  public class SomFetchArtistWorker : FetchArtistInfoWorker {
    private WebBrowser webBrowser;
    private string html;

    protected override string BuildUrl(string artistName) {
      //http://www.spirit-of-metal.com/groupe-groupe-Black_Sabbath-l-de.html
      string artist = artistName.Replace(" ", "_");
      return $"http://www.spirit-of-metal.com/groupe-groupe-" + artist + "-l-de.html";
    }

    private void Foo(string url) {
      this.webBrowser = new WebBrowser();
      webBrowser.Navigated += WebBrowserOnNavigated;
      webBrowser.Navigate(url);


    }

    private void WebBrowserOnNavigated(object sender, WebBrowserNavigatedEventArgs webBrowserNavigatedEventArgs) {
      // get the root element
      HtmlElement documentElement = webBrowser.Document.GetElementsByTagName("html")[0];

      // poll the current HTML for changes asynchronosly
      string html = documentElement.OuterHtml;
      while (true) {
        // wait asynchronously, this will throw if cancellation requested
         Task.Delay(500);

        // continue polling if the WebBrowser is still busy
        if (webBrowser.IsBusy)
          continue;

        string htmlNow = documentElement.OuterHtml;
        if (html == htmlNow)
          break; // no changes detected, end the poll loop

        html = htmlNow;
        this.html = html;
      }
    }

    protected override ArtistInfo FetchDataFromWebsite(string url) {
      Foo(url);
      while (true) {
        Task.Delay(500);

        if (!string.IsNullOrEmpty(this.html)) {
          break;
        }
      }

      //HtmlWeb web = new HtmlWeb();
      //HtmlDocument doc = web.Load(url);

      //HtmlNodeCollection htmlNodeCollection = doc.DocumentNode.SelectNodes("/html/body/table/tbody/tr[4]/td/table[1]/tbody/tr/td[2]/table/tbody/tr[2]/td/table[1]/tbody/tr/td[1]/ul[1]/a");
      //HtmlNode selectNode = htmlNodeCollection[0];
      //string metascore = selectNode.InnerText;
      //string userscore = doc.DocumentNode.SelectNodes("//*[@id=\"main\"]/div[3]/div/div[2]/div[1]/div[2]/div[1]/div/div[2]/a/span[1]")[0].InnerText;
      //string summary = doc.DocumentNode.SelectNodes("//*[@id=\"main\"]/div[3]/div/div[2]/div[2]/div[1]/ul/li/span[2]/span/span[1]")[0].InnerText;
      throw new NotImplementedException();
    }
  }
}