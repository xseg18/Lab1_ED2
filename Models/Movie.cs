using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte_3.Models
{
    public class Movie : IComparable
    {
        public string director { get; set; }
        public float imdbRating { get; set; }
        public string genre { get; set; }
        public string releaseDate { get; set; }
        public int rottenTomatoesRating { get; set; }
        public string title { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Movie otherMovie = obj as Movie;
            if (otherMovie != null)
                return this.title.CompareTo(otherMovie.title);
            else
                throw new ArgumentException("Objects is not a Movie");
        }
    }
}
