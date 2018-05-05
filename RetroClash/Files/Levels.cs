using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RetroClash.Files
{
    public class Levels : IDisposable
    {
        public List<string> NpcLevels = new List<string>();

        public Levels()
        {
            if (Directory.Exists("Assets/") && File.Exists("Assets/starting_home.json"))
                StartingHome = Regex.Replace(File.ReadAllText("Assets/starting_home.json", Encoding.UTF8),
                    "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

            NpcLevels.Add(Regex.Replace(File.ReadAllText("Assets/level/tutorial_npc.json", Encoding.UTF8),
                "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"));
            NpcLevels.Add(Regex.Replace(File.ReadAllText("Assets/level/tutorial_npc2.json", Encoding.UTF8),
                "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"));

            for (var i = 1; i < 49; i++)
                NpcLevels.Add(Regex.Replace(File.ReadAllText($"Assets/level/npc{i}.json", Encoding.UTF8),
                    "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"));
        }

        public string StartingHome { get; set; }

        public void Dispose()
        {
            StartingHome = null;
            NpcLevels = null;
        }
    }
}