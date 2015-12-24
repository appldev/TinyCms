using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCms;
using TinyCms.Models;

namespace TCMS
{
    class Program
    {



        static void Main(string[] args)
        {
            // build:library con:"Server=(LocalDB)\ProjectsV12;Database=TinyCmsDB;Integrated Security=SSPI;" metadata:"C:\GitHub\TinyCms\TinyCms.Models\Models\TinyCmsDB.Metadata.json" path:"C:\Source\PWDEV\Webshop\PWWeb\Scripts" Library:{00000000-1000-0000-A000-000000000000}
            // build:library con:"Server=(LocalDB)\ProjectsV12;Database=TinyCmsDB;Integrated Security=SSPI;" metadata:"C:\GitHub\TinyCms\TinyCms.Models\Models\TinyCmsDB.Metadata.json" path:"C:\Source\PWDEV\Webshop\PWWeb\Content" Library:{00000000-1000-0000-A000-000000000001}

            
            List<StartArg> Commands = StartArg.ParseArguments();
            if (Commands.Count == 0)
            {
                WriteHelp();
            }
            else
            {
                Execute(Commands);
            }
            Console.WriteLine("Finished. Press any key to exit");
            Console.ReadKey();
        }

        private static void WriteHelp()
        {

        }

        private static void Execute(List<StartArg> Commands)
        {
            TinySql.SqlBuilder.DefaultConnection = Commands.Get<string>("Con");
            TinySql.SqlBuilder.DefaultMetadata = TinySql.Serialization.SerializationExtensions.FromFile(Commands.Get<string>("Metadata"));
            
            switch (Commands.Get<string>("build").ToLowerInvariant())
            {
                case "library":
                    BuildLibrary(Commands);
                    break;

                default:
                    break;
            }
        }

        private static void BuildWebPages(List<StartArg> Commands)
        {
            // step 1: Get PageHost
            PageHost Host = PageHost.ByName(Commands.Get<string>("Host"));
            
            // step 2: Load existing pages and folders
            List<PageFolder> folders = PageFolder.ByHost(Host.Id);
            List<Page> unpub = Page.ByHost(Host.Id);
            List<PublishedPage> pub = PublishedPage.ByHost(Host.Id);

            string Culture = Commands.Get<string>("Culture", Host.Culture).ToLower();
            string ViewRoot = Commands.Get<string>("ViewRoot");
            bool Move = Commands.Get<int>("Move", 0) == 1;
            string ReserveRoute = Commands.Get<string>("ReserverRoute");
            string Path = "/";
            PageFolder CurrentFolder = folders.First(x => x.folderpath.Equals(Path));
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(ViewRoot);



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


        private static void BuildPageDirectory(PageHost Host, string Culture, List<PageFolder> Folders, List<Page> UnpublishedPages, List<PublishedPage> PublishedPages,bool Move, string ReserveRoutes, string Path, PageFolder CurrentFolder, System.IO.DirectoryInfo CurrentDir, int FolderIndex = 0, int FolderLevel = 0)
        {
            Console.WriteLine("Building Path {0} from the Directory at {1}", Path, CurrentDir.FullName);
            if (Path != "/")
            {
                if (Folders.Any(x => x.Name.Equals(CurrentDir.Name, StringComparison.OrdinalIgnoreCase) && x.folderlevel == FolderLevel))
                {

                }
            }
            foreach (System.IO.FileInfo fi in CurrentDir.GetFiles("*.cshtml"))
            {

            }
        }

        private static void BuildLibrary(List<StartArg> Commands)
        {
            Guid LibraryId = new Guid(Commands.Get<string>("Library"));
            Library lib = Library.Load(LibraryId);
            string Filter = Commands.Get<string>("Filter", "*.*");
            string basePath = Commands.Get<string>("Path");
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(basePath);
            Guid RootId = lib.Folders.First(x => x.FolderLevel == 0).Id.Value;
            BuildLibraryDirectory(lib, dir, Filter, 0, RootId, RootId);
        }

        private static void BuildLibraryDirectory(Library lib, System.IO.DirectoryInfo dir, string Filter, int FolderLevel, Guid FolderId, Guid ParentFolderId)
        {
            Console.WriteLine("Processing folder {0}...", dir.FullName);
            if (FolderLevel > 0)
            {
                // Check for folder existance
                if (!lib.Folders.Any(x => x.Name.Equals(dir.Name, StringComparison.InvariantCultureIgnoreCase) && x.FolderLevel.Value.Equals(FolderLevel)))
                {
                    Console.WriteLine("Creating new Library folder: {0}", dir.Name);
                    LibraryFolderBase folder = new LibraryFolderBase()
                    {
                        Id = Guid.NewGuid(),
                        Name = dir.Name,
                        LibraryId = lib.Id,
                        ParentId = ParentFolderId,
                        Title = dir.Name,
                        Description = string.Format("Created by TinyCms from {0}", dir.FullName)
                    };
                    if (!LibraryFolder.Create(folder))
                    {
                        throw new InvalidOperationException(string.Format("The folder {0} could not be created from {1}", dir.Name, dir.FullName));
                    }
                    FolderId = folder.Id;
                }
                else
                {
                    FolderId = lib.Folders.First(x => x.Name.Equals(dir.Name, StringComparison.InvariantCultureIgnoreCase) && x.FolderLevel.Value.Equals(FolderLevel)).Id.Value;
                }
            }
            foreach (string pattern in Filter.Split(';'))
            {
                foreach (System.IO.FileInfo fi in dir.GetFiles(pattern))
                {
                    if (lib.Items.Any(x => x.FolderLevel == FolderLevel && (x.FolderName.Equals(dir.Name, StringComparison.OrdinalIgnoreCase) || x.LibraryFolderId.Equals(FolderId)) && x.Name.Equals(fi.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine("Skipping {0}. It already exists", fi.Name);
                        continue;
                    }
                    LibraryItemBase item = new LibraryItemBase()
                    {
                        Id = Guid.NewGuid(),
                        Name = fi.Name,
                        Description = string.Format("Created by TinyCms from {0}", fi.FullName),
                        LibraryFolderId = FolderId,
                        Title = System.IO.Path.GetFileNameWithoutExtension(fi.Name)
                    };
                    if (!LibraryItem.Create(item))
                    {
                        throw new InvalidOperationException(string.Format("The Library Item {0} could not be created from {1}", item.Name, fi.FullName));
                    }
                    Console.WriteLine("Created {0}", fi.Name);
                }
            }

            foreach (System.IO.DirectoryInfo subDir in dir.GetDirectories())
            {
                BuildLibraryDirectory(lib, subDir, Filter, FolderLevel + 1, Guid.Empty, FolderId);
            }
            Console.WriteLine("Finished folder {0}", dir.FullName);
        }
    }




}
