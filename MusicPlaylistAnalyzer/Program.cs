using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.IO;



namespace MusicPlayerAnalyzer

{

    public class Song

    {

        public String Name;

        public String Artist;

        public String Album;

        public String Genre;

        public int Size;

        public int Time;

        public int Year;

        public int Plays;




        public Song(String name, String artist, String album, String genre, int size,

        int time, int year, int plays)

        {

            Name = name;

            Artist = artist;

            Album = album;

            Genre = genre;

            Size = size;

            Time = time;

            Year = year;

            Plays = plays;




        }

        override public string ToString()

        {

            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);

        }

    }

    class Program

    {

        static void Main(string[] args)

        {

            string report = null;

            int i;

            List<Song> RowCol = new List<Song>();

            try

            {

                if (File.Exists($"SampleMusicPlaylist.txt") == false)

                {

                    Console.WriteLine("This text doesn't exist. Try again. ");

                }

                else

                {

                    StreamReader sr = new StreamReader($"SampleMusicPlaylist.txt");
                    
                    i = 0;

                    string line = sr.ReadLine();

                    while ((line = sr.ReadLine()) != null)

                    {

                        i = i + 1;

                        try

                        {
                        


                            string[] strings = line.Split("\t");



                            if (strings.Length < 8)

                            {

                                Console.Write("This record doesn't contain correct number of data elements. Try again");

                                Console.WriteLine($"Row {i} contains {strings.Length} values. It should contain 8.");

                                break;

                            }

                            else

                            {

                                //Song dataTemp = new Song((strings[0]), (strings[1]), (strings[2]), (strings[3]), Int32.Parse(strings[4]), Int32.Parse(strings[5]));
                                Song dataTemp = new Song(strings[0], strings[1], strings[2], strings[3], Int32.Parse(strings[4]), Int32.Parse(strings[5]), Int32.Parse(strings[6]), Int32.Parse(strings[7]));
                                RowCol.Add(dataTemp);

                            }

                        }

                        catch

                        {

                            Console.Write("Errors have ocurred reading lines from data file. Try again");

                            break;

                        }

                    }

                    sr.Close();

                }

            }

            catch (Exception e)

            {

                Console.WriteLine("this playlist data file can't be opened.");

            }




            try

            {

                Song[] songs = RowCol.ToArray();

                using (StreamWriter write = new StreamWriter("MusicPlayer.txt"))

                {

                    write.WriteLine("Music Player");

                    write.WriteLine("");




                    //1

                    var SongsPlays = from thisSong in songs where thisSong.Plays >= 200 select thisSong;

                    report += "song plays >= 200: \n";

                    foreach (Song thisSong in SongsPlays)

                    {

                        report += thisSong + "\n";

                    }

                    //2

                    var AlternativeGenre = from thisSong in songs where thisSong.Genre == "Alternative" select thisSong;

                    i = 0;

                    foreach (Song thisSong in AlternativeGenre)

                    {

                        i++;

                    }

                    report += $"songs in genre of Alternative: {i}\n";
    
                    //3

                    var HipHopGenre = from thisSong in songs where thisSong.Genre == "Hip-Hop/Rap" select thisSong;

                    i = 0;

                    foreach (Song thisSong in HipHopGenre)

                    {

                        i++;

                    }

                    report += $"Number of Hip-Hop/Rap songs: {i}\n";

                    //4   

                    var FishbowlAlbumSongs = from thisSong in songs where thisSong.Album == "Welcome to the Fishbowl" select thisSong;

                    report += "Songs from the album Welcome to the Fishbowl: \n";

                    foreach (Song thisSong in FishbowlAlbumSongs)

                    {

                        report += thisSong + "\n";

                    }

                    //5

                    var Songs1970 = from thisSong in songs where thisSong.Year < 1970 select thisSong;

                    report += "Songs from before 1970: \n";

                    foreach (Song thisSong in Songs1970)

                    {

                        report += thisSong + "\n";

                    }

                    //6

                    var characters85 = from thisSong in songs where thisSong.Name.Length > 85 select thisSong.Name;

                    report += "Song names longer than 85 characters: \n";

                    foreach (String name in characters85)

                    {

                        report += name + "\n";

                    }

                    //7

                    var LongSong = from thisSong in songs orderby thisSong.Time descending select thisSong;

                    report += "Longest song: \n";

                    report += LongSong.First();



                    write.Write(report);




                    write.Close();

                }

                Console.WriteLine("Playlist file is created");

            }

            catch (Exception ex)

            {

                Console.WriteLine("Report file can't be open or written to. Please try again");

            }

            Console.ReadLine();

        }

    }

}












