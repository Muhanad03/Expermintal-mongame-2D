using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg.UI
{
    public class Dialogue
    {
        public List<string> Lines { get; private set; }
        private int currentLineIndex = 0;

        public Dialogue(List<string> lines)
        {
            Lines = lines;
        }

        public string GetCurrentLine()
        {
            if (currentLineIndex < Lines.Count)
                return Lines[currentLineIndex];
            else
                return null;
        }

        public void NextLine()
        {
            currentLineIndex++;
        }
        public void restart()
        {
            currentLineIndex = 0;
        }
        public int getCurrentIndex()
        {
            return currentLineIndex;
        }

        public bool IsDialogueComplete()
        {
            return currentLineIndex >= Lines.Count;
        }
    }

}
