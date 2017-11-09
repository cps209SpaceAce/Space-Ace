using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Model
{
    
    
    class HighScore
    {
        public string Name;       // Name
        public Level Level;       // Last Level Complete
        public Difficulty Diff;   // Difficulty
        public int Score;         // Final Score
        public string ShipImage;

        public HighScore(string name, Level level, Difficulty diff, int score, string shipimage )
        {
            this.Name = name;
            this.Level = level;
            this.Diff = diff;        
            this.Score = score;
            this.ShipImage = shipimage;
        }
    }


    class HighScoreManager
    {
        // List of HighScores saved
        public List<HighScore> highScores { get; set; }


        /// On startup - Load from JSON file
        /// List of HighScores.
        public  void Load()
        {
            // Read JSON File
            string loadString = File.ReadAllText(Environment.CurrentDirectory + @"\JSON.txt");

            // Convert to list
            // load to HighScores     
            highScores = new JavaScriptSerializer().Deserialize<List<HighScore>>(loadString);
            
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
        public void Save()
        {
            // Convert to json
            string json = new JavaScriptSerializer().Serialize(highScores);

            // Write to file
            File.WriteAllText(Environment.CurrentDirectory + @"\JSON.txt", json);
        }
        
        
    }

    // http://matijabozicevic.com/blog/csharp-net-development/csharp-serialize-object-to-json-format-using-javascriptserialization
    // https://stackoverflow.com/questions/7000811/cannot-find-javascriptserializer-in-net-4-0
}
