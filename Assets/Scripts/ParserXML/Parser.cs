using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.Scripts.ParserXML
{
    class Parser
    {
        static Parser()
        {
            XDocument doc = XDocument.Load("Assets/Scripts/ParserXML/XML.xml");

            List<NPC> npcs = new List<NPC>();

            foreach(XElement npc in doc.Root.Element("npcs").Elements("npc")) {
                List<Dialogue> dialogues = new List<Dialogue>();
                foreach(XElement dialogue in npc.Elements("dialogue"))
                {
                    List<string> options = new List<string>();
                    foreach(XElement option in dialogue.Elements("options"))
                    {
                        options.Add(option.Value);
                    }
                    dialogues.Add(new Dialogue(
                        dialogue.Element("text").Value,
                        dialogue.Element("answer").Value,
                        options
                        ));
                }
                npcs.Add(new NPC(npc.Attribute("name").Value, dialogues));
            }
            Debug.Log(npcs.ElementAt(0).Name);
            Debug.Log(npcs.ElementAt(0).dialogues.ElementAt(0).Text);
        }

        public void parseConversation(string npc)
        {

        }
    }
}
