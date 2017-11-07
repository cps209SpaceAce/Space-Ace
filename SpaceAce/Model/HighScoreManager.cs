using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Model
{
    enum Difficulty
    {
        Easy, Medium, Hard
    }
    enum Level
    {
        Level_1,Level_2,Boss
    }

    
    class HighScore
    {
        public string Name;       // Name
        public Level Level;       // Last Level Complete
        public Difficulty Diff;   // Difficulty
        public int Score;         // Final Score
        public string shipImage;

        public HighScore()
        {
        }
    }


    class HighScoreManager
    {
        // List of HighScores saved
        public List<HighScore> highScores { get; set; }


        /// On startup - Load from JSON file
        /// List of HighScores.
        private void Load()
        {
            // Read JSON File
            // Convert to list
            // load to HighScores     
        }


        // At end of Level or death
        // Updates / Sorts HighScoreManager - highScores
        public void Update(HighScore newScore)
        {
            // Add score to highScores
            // https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
            // Sort by Score - (Look at P4)
        }

        // After update
        // Saves to JSON File
        public void Save()
        {

        }
        
        
    }

    // http://matijabozicevic.com/blog/csharp-net-development/csharp-serialize-object-to-json-format-using-javascriptserialization
    // https://stackoverflow.com/questions/7000811/cannot-find-javascriptserializer-in-net-4-0
}
