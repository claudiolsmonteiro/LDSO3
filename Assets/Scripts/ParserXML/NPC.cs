using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ParserXML
{
    class NPC
    {
        public string Name { get; set; }
        public List<Dialogue> dialogues { get; set; }

        public NPC(string Name, List<Dialogue> dialogues)
        {
            this.Name = Name;
            this.dialogues = dialogues;
        }
    }
}
