namespace RenameArtistFolders {
  public class ArtistInfo {
    public string Genre { get; set; }

    public string State { get; set; }

    public string City { get; set; }

    public int Year { get; set; }

    public string Name { get; set; }

    public ArtistInfo(string genre, string state, string city, int year, string name) {
      this.Genre = genre;
      this.State = state;
      this.City = city;
      this.Year = year;
      this.Name = name;
    }
  }
}