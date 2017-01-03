using Assets.Scripts.ParserXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.Scripts.ParserXML
{
    class Parser
    {
        public List<NPC> npcs;
        public string path;
        public Parser()
        {
            if (PlayerPrefs.HasKey("language"))
            {
                path = PlayerPrefs.GetString("language");
            }
            else
            {
                path = "Assets/Scripts/ParserXML/english.xml";
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            npcs = new List<NPC>();

            foreach(XmlElement npc in doc.GetElementsByTagName("npc"))
            {
                List<Dialogue> dialogues = new List<Dialogue>();
                foreach (XmlElement dialogue in npc.GetElementsByTagName("dialogue"))
                {
                    List<string> options = new List<string>();
                    foreach (XmlElement option in dialogue.GetElementsByTagName("options")[0].ChildNodes)
                    {
                        options.Add(option.InnerText);
                    }
                    dialogues.Add(new Dialogue(
                        dialogue.GetElementsByTagName("text")[0].InnerText,
                        dialogue.GetElementsByTagName("answer")[0].InnerText,
                        options
                        ));
                }
                npcs.Add(new NPC(npc.GetAttribute("name"), dialogues));
            }




            //npcs = new List<NPC>();

        }



    }

}
