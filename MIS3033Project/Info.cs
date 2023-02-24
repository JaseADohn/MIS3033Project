using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MIS3033Project
{
    public class Movie
    {
        public string Title { get; set; }
        public List<string> Genres { get; set; }
        public List<Director> Directors { get; set; }
        public List<string> StarActors { get; set; }
        public string ImdbLink { get; set; }

        public Movie(string title, List<string> genres, List<Director> directors, List<string> starActors, string imdbLink)
        {
            Title = title;
            Genres = genres;
            Directors = directors;
            StarActors = starActors;
            ImdbLink = imdbLink;
        }

        // Constructor that takes a CSV row and parses it into a Movie object
        public Movie(string csvRow)
        {
            var fields = csvRow.Split(',');

            Title = fields[0];
            Genres = fields[1].Split('/').ToList();

            for (int i = 0; i < Genres.Count; i++)
            {
                Genres[i] = Genres[i].Replace("Comedi", "Comedy").Replace("Advinture", "Adventure").Replace("Sci-Fo", "Sci-Fi").Replace("Drame", "Drama");
            }

            Directors = fields[2].Split(';').Select(director => new Director(director)).ToList();
            StarActors = fields[3].Split(';').ToList();
            ImdbLink = fields[4];
        }

        public override string ToString()
        {
            return $"{Title}, {Genres}, {StarActors}, {Directors}, {ImdbLink}";
        }
    }

    public class Director
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string ImageLink { get; set; }

        public Director(string details)
        {
            var fields = details.Split('|');

            FirstName = fields[0];
            LastName = fields[1];
            Birthday = DateTime.Parse(fields[2]);
            ImageLink = fields[3];
        }


        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}