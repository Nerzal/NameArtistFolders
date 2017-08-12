namespace RenameArtistFolders {
  public abstract class FetchArtistInfoWorker {
    public ArtistInfo FetchInformation(string artistName) {
      ArtistInfo result = null;
      string url = BuildUrl(artistName);
      result = FetchDataFromWebsite(url);
      return result;
    }

    protected abstract string BuildUrl(string artistName);

    protected abstract ArtistInfo FetchDataFromWebsite(string url);
  }
}