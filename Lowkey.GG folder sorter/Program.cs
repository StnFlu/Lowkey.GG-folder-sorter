using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lowkey.GG_folder_sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            string txtPath = Directory.GetCurrentDirectory();

            bool notSorted = true;
            string ext = "";
            Console.WriteLine("\nWhat is your file extension?");
            Console.WriteLine("1: MKV --- 2: MP4");
            while (notSorted)
            {            
                switch (Console.ReadKey(true).KeyChar.ToString())
                {
                    case "1":

                        ext = "*.mkv";
                        goto LoopEnd;
                    
                    case "2":

                        ext = "*.mp4";
                        goto LoopEnd;
                        ;
                    default:
                        Console.WriteLine("\nInvalid Selection!");
                        break;
                }
            }
            LoopEnd:
            string[] files = Directory.GetFiles(txtPath, ext, SearchOption.TopDirectoryOnly);
            string[] fileExtended = new string[files.Length];
            string[] fullfiles = Directory.GetFiles(txtPath, ext, SearchOption.TopDirectoryOnly);
            string[] folders = Directory.GetDirectories(txtPath);
            for (int i = 0; i < fileExtended.Length; i++)
            {
                string[] filesSplit = files[i].Split(txtPath);
                fileExtended[i] = filesSplit[1];
            }           
            files = sweep(files, txtPath);        
            folders = sweep(folders, txtPath);
            List<String> folds = folders.ToList();
            for (int i = 0; i < files.Length; i++)
            {
                if(folds.Contains(files[i]))
                {
                    if (Directory.Exists(txtPath + files[i] + @"\" + fileExtended[i]))
                        System.IO.Directory.Move(fullfiles[i], txtPath + files[i] + @"\" + fileExtended[i]);
                    else
                        Console.WriteLine("\n" + fileExtended[i] + " already exists.");
                }
                else
                {
                    System.IO.Directory.CreateDirectory(txtPath + files[i]);
                    Console.WriteLine("Moved " + txtPath + files[i]);
                    System.IO.Directory.Move(fullfiles[i], txtPath + files[i] + @"\" + fileExtended[i]);
                    Console.WriteLine(fileExtended[i]);
                }              
            }
        }
        public static string[] sweep(string[] array, string txtPath)
        {
            string[] finalArray = new string[array.Length];
            for (int i = 0; i < finalArray.Length; i++)
            {
                string[] filesSplit = array[i].Split(txtPath);
         
                filesSplit = filesSplit[1].Split("_");
                var removeLast = filesSplit.Take(filesSplit.Length - 1).ToArray();
                string final = String.Join(" ", removeLast);

                finalArray[i] = final.Trim();
            }
            return finalArray;
        }

    }

  
}
