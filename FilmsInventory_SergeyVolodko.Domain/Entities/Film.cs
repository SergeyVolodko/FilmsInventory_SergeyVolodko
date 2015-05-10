
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
        private FilmType type;

        public string Name { get; private set; }

        public FilmType Type {
            get { return type; }
        }

        public Film(string name, FilmType type)
        {
            this.Name = name;
            this.type = type;
        }

        public void ChangeType(FilmType newType)
        {
            this.type = newType;
        }
    }
}
