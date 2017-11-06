using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAce.Model
{
    enum Difficulty
    {
        Easy, Medium, Hard
    }
    enum Level
    {
        Level_1,Level_2,Boss
    }

    struct HighScore
    {
        string Name;       // Name
        Level Level;       // Last Level Complete
        Difficulty Diff;   // Difficulty
        int Score;         // Final Score
    }


    class HighScoreManager
    {
        // List of HighScores saved
        List<HighScore> highScores;

        public HighScoreManager()
        {
            this.highScores = Load();
        }

        /// On startup - Load from file
        /// List of HighScores.
        private List<HighScore> Load()
        {
            // Read File
            // Convert to list
            List<HighScore> list = new List<HighScore> { new HighScore() { } };
            return list;
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
        // Updates the File
        public void Save()
        {

        }
        
        
    }

    

}
