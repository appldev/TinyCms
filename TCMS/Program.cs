using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMS
{
    class Program
    {



        static void Main(string[] args)
        {
            List<StartArg> Commands = StartArg.ParseArguments();
            if (Commands.Count== 0)
            {
                WriteHelp();
            }
            else
            {
                Execute(Commands);
            }
        }

        private static void WriteHelp()
        {

        }

        private static void Execute(List<StartArg> Commands)
        {
            
        }


        private static void BuildWebPages()
        {
            // step 1: Register PageHost

            // step 2: Create folder structure
            // 2a: create 'Templates' folder
            // 2b: create 'Shared' Folder if not exists

            // step 3: Create _ViewStart.cshtml in folder root

            // step 4: Create Shared/*.cshtml pages

            // step 5: Create pages per folder

            // 5a: Create index.cshtml as empty page
            // 5b: Check templates folder for pagetype: foldername_pagename.cshtml. create ifnot exist
            // 5c: create page with reference to pagetype from 5b

        }
    }
}
