using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wincolmem
{
    public struct HighScore
    {
        public int points;
        public string name;
    }

    class HighScores : List<HighScore>
    {
        public HighScores()
        {
            //Add default high scores to the list.
            for(int i = 0; i<5; i++)
                this.Add(new HighScore{ points = 5, name = "JV"});        
        }
    }
}
