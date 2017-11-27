using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Model
{


    public class HighScore
    {
        public string Name;       // Name
        public Level Level;       // Last Level Complete
        public Difficulty Diff;   // Difficulty
        public int Score;         // Final Score
        public string ShipImage;

        public HighScore()
        {
            Name = "Unknown";
            Score = 0;
            Diff = Difficulty.Easy;
            Level = Level.Level_1;
        }

        public HighScore(string name, Level level, Difficulty diff, int score, string shipimage)
        {
            if (name == "")
                Name = "Unknown";
            else
                Name = name;

            Level = level;
            Diff = diff;
            Score = score;
            ShipImage = shipimage;
        }
        public override string ToString()
        {
            string lev = "";
            if (Level == Level.Level_1)
                lev = "Level 1";
            else if (Level == Level.Level_2)
                lev = "Level 2";
            else if (Level == Level.Boss)
                lev = "Boss";

            string name = Name.PadRight(10);
            return " " + name.Substring(0, 9) + "   " + lev.PadRight(9) + "   " + Convert.ToString(Diff).PadRight(10) + "   " + Convert.ToString(Score).PadLeft(8);
        }
    }


    public class HighScoreManager
    {
        // List of HighScores saved
        public List<HighScore> highScores;

        public HighScoreManager()
        {
            highScores = new List<HighScore>();
            this.Load();
        }
        /// On startup - Load from JSON file
        /// List of HighScores.
        public void Load()
        {
            // Read JSON File

            if (!File.Exists(Environment.CurrentDirectory + @"/JSON.txt"))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(Environment.CurrentDirectory + @"/JSON.txt"))
                {
                    sw.WriteLine("[");
                    sw.WriteLine("{\"Name\":\"R Astley\",\"Level\":2,\"Diff\":2,\"Score\":80,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"R Astley\",\"Level\":2,\"Diff\":2,\"Score\":70,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Joanna\",\"Level\":2,\"Diff\":2,\"Score\":1000,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Noah\",\"Level\":2,\"Diff\":2,\"Score\":900,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Robert\",\"Level\":2,\"Diff\":2,\"Score\":800,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Pettit\",\"Level\":2,\"Diff\":2,\"Score\":700,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Schaub\",\"Level\":2,\"Diff\":2,\"Score\":600,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Knisely\",\"Level\":2,\"Diff\":2,\"Score\":500,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Putin\",\"Level\":2,\"Diff\":2,\"Score\":400,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Turnbull\",\"Level\":2,\"Diff\":2,\"Score\":200,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"Elizabeth II\",\"Level\":2,\"Diff\":2,\"Score\":100,\"ShipImage\":\"spaceship-hi.png\"},");
                    sw.WriteLine("{\"Name\":\"R Astley\",\"Level\":2,\"Diff\":2,\"Score\":90,\"ShipImage\":\"spaceship-hi.png\"},");                    
                    sw.WriteLine("{\"Name\":\"R Astley\",\"Level\":2,\"Diff\":2,\"Score\":60,\"ShipImage\":\"spaceship-hi.png\"}");

                    sw.WriteLine("]");
                }
            }
            string loadString = File.ReadAllText(Environment.CurrentDirectory + @"/JSON.txt");

            // Convert to list
            // load to HighScores     
            List<HighScore> list = new JavaScriptSerializer().Deserialize<List<HighScore>>(loadString);
            if (list != null)
                highScores = list;

        }


        // At end of Level or death
        // Updates / Sorts HighScoreManager - highScores
        public void Update(HighScore newScore)
        {
            // Add score to highScores
            highScores.Add(newScore);

            // Sorts the list by Score
            
        }
        public void Sort()
        {
            highScores = highScores.OrderByDescending(o => o.Score).ToList();
        }

        // After update
        // Saves to JSON File
        public void Save()
        {
            //ystring loadString = File.ReadAllText(Environment.CurrentDirectory + @"/JSON.txt"); 
            //yList<HighScore> list = new JavaScriptSerializer().Deserialize<List<HighScore>>(loadString);
            //ylist.Add(newScore);

            string json = new JavaScriptSerializer().Serialize(highScores);
            File.WriteAllText(Environment.CurrentDirectory + @"\JSON.txt", json);
        }

    }

    // http://matijabozicevic.com/blog/csharp-net-development/csharp-serialize-object-to-json-format-using-javascriptserialization
    // https://stackoverflow.com/questions/7000811/cannot-find-javascriptserializer-in-net-4-0
}
