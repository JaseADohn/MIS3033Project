using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Formats.Asn1;
using System.Text.Json;
using System.Globalization;
using System.Security.Cryptography;
using System.ComponentModel;
//using Newtonsoft.Json;


namespace MIS3033Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Movie> movieListInput = new List<Movie>();
        //private List<Director> directorInput = new List<Director>();


        public MainWindow()
        {
            InitializeComponent();

            CboFileType.Items.Add("CSV");
            CboFileType.Items.Add("JSON");
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = path;
            ofd.Filter = "Comma Separated Value Documents (.csv)|* .csv";

            if (ofd.ShowDialog() == true)
            {
                movieListInput = ReadMoviesFromCsv(ofd.FileName);

                PopulateListBox(movieListInput);
            }
        }

        public static List<Movie> ReadMoviesFromCsv(string filePath)
        {
            var movies = new List<Movie>();
            var lines = File.ReadAllLines(filePath);


            for (int i = 1; i < lines.Length; i++) // skip header (1st row)
            {
                var line = lines[i];
                var movie = new Movie(line);
                movies.Add(movie);
            }

            return movies;
        }

        private void PopulateListBox(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                lstAllInfo.Items.Add(movie);

                foreach (Director director in movie.Directors)
                {
                    if (!lstDirectors.Items.Contains(director.ToString()))
                    {
                        lstDirectors.Items.Add(director.ToString());
                    }
                }
                foreach (string genre in movie.Genres)
                {
                    if (!lstGenres.Items.Contains(genre.ToString()))
                    {
                        lstGenres.Items.Add(genre.ToString());
                    }
                }
            }
            lstAllInfo.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));

            lstDirectors.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

            lstGenres.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            if (CboFileType.SelectedItem == null)
            {
                MessageBox.Show("Please select a file format.");
                return;
            }

            var selectedFormat = CboFileType.SelectedItem.ToString();

            if (selectedFormat == "JSON")
            {
                string serializedData = "";
                var options = new JsonSerializerOptions { WriteIndented = true };
                serializedData = JsonSerializer.Serialize(movieListInput, options);

                string path = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads";
                File.WriteAllText($"{path}\\MovieData_All.json", serializedData);
                MessageBox.Show("Files saved");

            }
            else if (selectedFormat == "CSV")
            {
                StreamWriter csvOutput = new StreamWriter(@"C:\Users\jased\source\repos\MIS3033Project\MIS3033Project\AllData.csv");
                csvOutput.WriteLine("Title, Genre, Director, Actor, IMDB");

                foreach (var movie in lstAllInfo.Items)
                {
                    csvOutput.Write(movie + ", ");
                    csvOutput.WriteLine();
                }
                csvOutput.Close();
                MessageBox.Show("Files saved");

                csvOutput = new StreamWriter(@"C:\Users\jased\source\repos\MIS3033Project\MIS3033Project\Directors.csv");
                csvOutput.WriteLine("Title, Genre, Director, Actor, IMDB");

                foreach (var director in lstDirectors.Items)
                {
                    csvOutput.Write(director);
                    csvOutput.WriteLine();
                }
                csvOutput.Close();                

                csvOutput = new StreamWriter(@"C:\Users\jased\source\repos\MIS3033Project\MIS3033Project\Genres.csv");
                csvOutput.WriteLine("Title, Genre, Director, Actor, IMDB");

                foreach (var genres in lstGenres.Items)
                {
                    csvOutput.Write(genres);
                    csvOutput.WriteLine();
                }
                csvOutput.Close();
                
            }

            else
            {
                MessageBox.Show($"Unknown file format: {selectedFormat}");
                return;
            }
        }
    }
}