using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lowkey.GG_folder_sorter
{
    class Program
    {
        private const string txtPath = @"D:\Recordings2021\";

        static void Main(string[] args)
        {
            bool notSorted = true;
            string ext = "";
            Start:
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

            Directory.SetCurrentDirectory(txtPath);
  
            string[] files = Directory.GetFiles(txtPath, ext, SearchOption.TopDirectoryOnly);
            string[] fileExtended = new string[files.Length];

            string[] fullfiles = Directory.GetFiles(txtPath, ext, SearchOption.TopDirectoryOnly);
            string[] folders = Directory.GetDirectories(txtPath);


            for (int i = 0; i < fileExtended.Length; i++)
            {
                string[] filesSplit = files[i].Split(txtPath);
                fileExtended[i] = filesSplit[1];
            }
           
            files = sweep(files);
            
            folders = sweep(folders);

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
            Console.WriteLine("\nFiles are sorted!");
            Console.WriteLine("Press f to finish or space to restart");

            while (notSorted)
            {
                string testies;
                switch (testies = Console.ReadKey(true).KeyChar.ToString())
                {
                    case " ":

                        goto Start;

                    case "f":
                        return;
                    default:
                        
                        break;
                }
            }


        }
        public static string[] sweep(string[] array)
        {
           // char[] separators = new char[] {'1', '2', '3', '4', '5', '6', '7', '0', '8', '9' };
            string[] finalArray = new string[array.Length];

            for (int i = 0; i < finalArray.Length; i++)
            {
                string[] filesSplit = array[i].Split(txtPath);
         
                filesSplit = filesSplit[1].Split("_");
                foreach (var item in filesSplit)
                {
                    Console.WriteLine("\n" + item);
                }
                

                //   Console.WriteLine(String.Join("_", filesSplit[filesSplit.Length - 1]));
                // array[i] = filesSplit[0];

               // filesSplit = filesSplit.SkipLast(1).ToArray();
                array[i] = filesSplit.SkipLast(1).ToString();
                Console.WriteLine(array[i]);
                string final = array[i].Replace("_", " ");
                Console.WriteLine(array[i]);
                finalArray[i] = final.Trim();
            }

            return finalArray;
        }

    }

  
}
