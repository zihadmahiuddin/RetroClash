using System;
using System.IO;
using System.Security.Cryptography;
using RetroClashPatchCreator.Extensions;
using File = RetroClashPatchCreator.Models.File;

namespace RetroClashPatchCreator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "RetroClash Patch Creator v0.1";

            Console.WriteLine("\r\n________     _____             ______________             ______  \r\n___  __ \\______  /_______________  ____/__  /_____ __________  /_ \r\n__  /_/ /  _ \\  __/_  ___/  __ \\  /    __  /_  __ `/_  ___/_  __ \\\r\n_  _, _//  __/ /_ _  /   / /_/ / /___  _  / / /_/ /_(__  )_  / / /\r\n/_/ |_| \\___/\\__/ /_/    \\____/\\____/  /_/  \\__,_/ /____/ /_/ /_/ \r\n                                                                  \r\n");

            Console.SetOut(new Prefixed());

            Console.WriteLine("Creating Patch...");

            if (Directory.Exists("Assets"))
            {
                if (Directory.Exists("Assets/csv"))
                {
                    using (var md5 = MD5.Create())
                    {
                        foreach (var file in Directory.GetFiles("Assets/csv"))
                        {
                            var file2 = new File
                            {

                            };
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Assets/csv direcory not found. Is this application in the correct folder?");
                }
            }
            else
            {
                Console.WriteLine("Assets direcory not found. Is this application in the correct folder?");
            }

            Console.ReadKey();
        }
    }
}
