
namespace FilmsInventory.Entities
{
    public enum FilmType
    {
        NewReleases,
        RegularFilms,
        OldFilms
    }

    public class Film
    {
        public string Name { get; private set; }

        public FilmType Type { get; private set; }

        public Film(string name, FilmType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public void ChangeType(FilmType newType)
        {
            this.Type = newType;
        }
    }
}
