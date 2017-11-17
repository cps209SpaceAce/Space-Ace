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
        }

        public HighScore(string name, Level level, Difficulty diff, int score, string shipimage)
        {
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

            return Name + ": " + lev + ", " + Diff + ". Score: " + Score;
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
            highScores = highScores.OrderBy(o => o.Score).ToList();
        }

        // After update
        // Saves to JSON File
        public void Save( HighScore newScore)
        {
            string loadString = File.ReadAllText(Environment.CurrentDirectory + @"/JSON.txt"); 
            List<HighScore> list = new JavaScriptSerializer().Deserialize<List<HighScore>>(loadString);
            list.Add(newScore);

            string json = new JavaScriptSerializer().Serialize(highScores);
            File.WriteAllText(Environment.CurrentDirectory + @"\JSON.txt", json);
        }

    }

    // http://matijabozicevic.com/blog/csharp-net-development/csharp-serialize-object-to-json-format-using-javascriptserialization
    // https://stackoverflow.com/questions/7000811/cannot-find-javascriptserializer-in-net-4-0
}
