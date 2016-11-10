using System.Collections.Generic;

namespace Assets.Scripts.ParserXML
{
    public class Dialogue
    {
        public string Text { get; set; }
        public string Answer { get; set; }
        public List<string> Options { get; set; }

        public Dialogue(string Text, string Answer, List<string> Options)
        {
            this.Text = Text;
            this.Answer = Answer;
            this.Options = Options;
        }
    }
}