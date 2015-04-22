using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_ListeningHabits
{
    class Program
    {
        // Global List
        public static List<Play> musicDataList = new List<Play>();
        static void Main(string[] args)
        {
            // initalize dataset into list
            InitList();
            // keep console open
            Console.ReadLine();
        }
        /// <summary>
        /// A function to initalize the List from the csv file
        /// needed for testing
        /// </summary>
        public static void InitList()
        {
            // load data
            using (StreamReader reader = new StreamReader("scrobbledata.csv"))
            {
                // Get and don't use the first line
                string firstline = reader.ReadLine();
                // Loop through the rest of the lines
                while (!reader.EndOfStream)
                {
                    musicDataList.Add(new Play(reader.ReadLine()));
                }
            }
        }
        /// <summary>
        /// A function that will return the total ammount of plays in the dataset
        /// </summary>
        /// <returns>total number of plays</returns>
        public static int TotalPlays()
        {
            //explains itself
            int totalNumPlays = musicDataList.Count();
            return totalNumPlays;
        }
        /// <summary>
        /// A function that returns the number of plays ever by an artist
        /// </summary>
        /// <param name="artistName">artist name</param>
        /// <returns>total number of plays</returns>
        public static int TotalPlaysByArtistName(string artistName)
        {
            //only count desired artist
            int totalArtistPlays = musicDataList.Count(x => x.Artist.ToUpper() == artistName.ToUpper());
            return totalArtistPlays;
        }
        /// <summary>
        /// A function that returns the number of plays by a specific artist in a specific year
        /// </summary>
        /// <param name="artistName">artist name</param>
        /// <param name="year">one year</param>
        /// <returns>total plays in year</returns>
        public static int TotalPlaysByArtistNameInYear(string artistName, string year)
        {
            //pare list by year
            //only count desired artist upper case for comparison
            int totalPlays = musicDataList.Where(x => x.Time.Year.ToString() == year).Count(x => x.Artist.ToUpper() == artistName.ToUpper());
            return totalPlays;
        }
        /// <summary>
        /// A function that returns the number of unique artists in the entire dataset
        /// </summary>
        /// <returns>number of unique artists</returns>
        public static int CountUniqueArtists()
        {
            //group by artists
            //count list
            int uniqueNewYork = musicDataList.GroupBy(x => x.Artist).Count();
            return uniqueNewYork;
        }
        /// <summary>
        /// A function that returns the number of unique artists in a given year
        /// </summary>
        /// <param name="year">year to check</param>
        /// <returns>unique artists in year</returns>
        public static int CountUniqueArtists(string year)
        {
            //pare by year
            //group remaining objects by artist
            //count list
            int uniqueUpOnHim = musicDataList.Where(x => x.Time.Year.ToString() == year).GroupBy(x => x.Artist).Count();
            return uniqueUpOnHim;
        }
        /// <summary>
        /// A function that returns a List of unique strings which contains
        /// the Title of each track by a specific artists
        /// </summary>
        /// <param name="artistName">artist</param>
        /// <returns>list of song titles</returns>
        public static List<string> TrackListByArtist(string artistName)
        {
            //sort by name
            //select the title property of every object
            //add titles to a new list
            List<string> franzLiszt = musicDataList.Where(x => x.Artist == artistName).Select(x => x.Title).ToList();
            return franzLiszt;
        }
        /// <summary>
        /// A function that returns the first time an artist was ever played
        /// </summary>
        /// <param name="artistName">artist name</param>
        /// <returns>DateTime of first play</returns>
        public static DateTime FirstPlayByArtist(string artistName)
        {
            //sort artist to upper 
            //select the datetime segment
            //return first item from that list
            DateTime deloreanMotorcar = musicDataList.Where(x => x.Artist.ToUpper() == artistName.ToUpper()).Select(x => x.Time).First();
            return deloreanMotorcar;
        }
        /// <summary>
        ///                     ***BONUS***
        /// A function that will determine the most played artist in a specified year
        /// </summary>
        /// <param name="year">year to check</param>
        /// <returns>most popular artist in year</returns>
        public static string MostPopularArtistByYear(string year)
        {
            //strip out only the year necessary for comparing
            //group by artist, artist 3, 4, 7, 2, 7, 8 counts of play object each in a list
            //order the list by descending by artist count 8, 7, 7, 4, 3, 2
            //take the first element of the first element in the larger list
            //get the artist property from that list
            string difficult = musicDataList.Where(x => x.Time.Year.ToString() == year).GroupBy(y => y.Artist).OrderByDescending(z => z.Count()).First().First().Artist;
            return difficult;
        }
    }

    public class Play
    {
        // Properties
        public DateTime Time { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public Play(string lineInput)
        {
            // Split using the tab character due to the tab delimited data format
            string[] playData = lineInput.Split('\t');

            // Get the time in milliseconds and convert to C# DateTime
            DateTime posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            this.Time = posixTime.AddMilliseconds(long.Parse(playData[0]));

            //property used to search for artist
            this.Artist = playData[1];
            //property used to search for title of song
            this.Title = playData[2];
            //property used to search for album... never used this assignment
            this.Album = playData[3];


        }
    }
}
