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
            // command:library con:"Server=(LocalDB)\ProjectsV12;Database=TinyCmsDB;Integrated Security=SSPI;" metadata:"C:\GitHub\TinyCms\TinyCms.Models\Models\TinyCmsDB.Metadata.json" path:"C:\Source\PWDEV\Webshop\PWWeb\Scripts" Library:{00000000-1000-0000-A000-000000000000}
            // command:pages con:"Server=(LocalDB)\ProjectsV12;Database=TinyCmsDB;Integrated Security=SSPI;" metadata:"C:\GitHub\TinyCms\TinyCms.Models\Models\TinyCmsDB.Metadata.json" Culture:en-us Host:Default Move:0 ReserveRoutes:1 Publish:1 ViewRoot:"C:\GitHub\TinyCms\TinyCms\Views"
            // command:publish con:"Server=(LocalDB)\ProjectsV12;Database=TinyCmsDB;Integrated Security=SSPI;" metadata:"C:\GitHub\TinyCms\TinyCms.Models\Models\TinyCmsDB.Metadata.json" Host:Default Id:6be0bd7d-6b0f-4a36-94ec-85a86badcece


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
            TinySql.Cache.CacheProvider.UseResultCache = false;
            switch (Commands.Get<string>("command").ToLowerInvariant())
            {
                case "library":
                    BuildLibrary(Commands);
                    break;

                case "pages":
                    BuildWebPages(Commands);
                    break;

                case "publish":
                    PublishPages(Commands);
                    break;

                default:
                    break;
            }
        }


        private static void PublishPages(List<StartArg> Commands)
        {
            string Ids = Commands.Get<string>("Id");
            PageHost Host = PageHost.ByName(Commands.Get<string>("Host"));

            List<Guid> pages = new List<Guid>();
            if (!string.IsNullOrEmpty(Ids))
            {
                foreach (string id in Ids.Split(','))
                {
                    Guid g;
                    if (Guid.TryParse(id,out g))
                    {
                        pages.Add(g);
                    }
                    else
                    {
                        Console.WriteLine("/tERROR: The Id '" + id + "' is not a valid GUID");
                    }
                }
            }

            if (pages.Count > 0)
            {
                Console.WriteLine(string.Format("{0} pages will be published",pages.Count));
            }

            foreach (Guid g in pages)
            {
                Page page = Page.ById(g, false);
                if (page == null)
                {
                    Console.WriteLine("/tERROR: An unpublished page with the Id '" + g.ToString() + "' could not be found");
                }
                else
                {
                    if (!Page.Publish(page))
                    {
                        Console.WriteLine(string.Format("/tERROR: The page {0} with the path {1} was not published", page.Name, page.FullPath));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("The page {0} with the path {1} was succesfully published", page.Name, page.FullPath));
                    }
                }
            }
        }

        private static void BuildWebPages(List<StartArg> Commands)
        {
            // step 1: Get PageHost
                PageHost Host = PageHost.ByName(Commands.Get<string>("Host"));

            // step 2: Load existing pages and folders
            List<PageFolder> folders = PageFolder.ByHost(Host.Id);
            List<Page> unpub = Page.ByHost(Host.Id,false);

            string Culture = Commands.Get<string>("Culture", Host.Culture).ToLower();
            string ViewRoot = Commands.Get<string>("ViewRoot");
            bool Move = Commands.Get<int>("Move", 0) == 1;
            bool ReserveRoute = Commands.Get<int>("ReserveRoutes", 0) == 1;
            bool Publish = Commands.Get<int>("Publish", 0) == 1;
            PageFolder CurrentFolder = folders.First(x => x.folderpath.Equals("/") && x.PageHostId.Equals(Host.Id));
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(ViewRoot);

            BuildPageDirectory(Host, Culture, folders, unpub, Move, ReserveRoute, Publish, CurrentFolder, dir);
        }


        private static void BuildPageDirectory(PageHost Host, string Culture, List<PageFolder> Folders, List<Page> UnpublishedPages, bool Move, bool ReserveRoutes, bool Publish, PageFolder CurrentFolder, System.IO.DirectoryInfo CurrentDir, int FolderIndex = 0, int FolderLevel = 0)
        {
            Console.WriteLine("Building from the Directory at {0}", CurrentDir.FullName);

            if (FolderLevel != 0)
            {
                if (!Folders.Any(x => x.Name.Equals(CurrentDir.Name, StringComparison.OrdinalIgnoreCase) && x.folderlevel == FolderLevel))
                {
                    CurrentFolder = PageFolder.Create(Host, CurrentFolder, FolderIndex, CurrentDir.Name);
                    FolderLevel = CurrentFolder.folderlevel.Value;
                    Folders.Add(CurrentFolder);
                    Console.WriteLine("Added the folder {0} to the host {1}", CurrentFolder.folderpath, Host.Name);
                }
                else
                {
                    CurrentFolder = Folders.Single(x => x.Name.Equals(CurrentDir.Name, StringComparison.OrdinalIgnoreCase) && x.folderlevel == FolderLevel);
                }
            }

            List<string> DeleteFiles = new List<string>();
            foreach (System.IO.FileInfo fi in CurrentDir.GetFiles("*.cshtml"))
            {
                string FileName = System.IO.Path.GetFileNameWithoutExtension(fi.Name);
                Page page;
                if (UnpublishedPages.Any(x => x.Name.Equals(FileName.ToUrlFriendly(), StringComparison.OrdinalIgnoreCase) && x.Culture.Equals(Culture) && x.PageFolderId.Equals(CurrentFolder.Id)))
                {
                    page = UnpublishedPages.First(x => x.Name.Equals(FileName.ToUrlFriendly(), StringComparison.OrdinalIgnoreCase) && x.Culture.Equals(Culture) && x.PageFolderId.Equals(CurrentFolder.Id));
                    page.Model = System.IO.File.ReadAllText(fi.FullName, Encoding.UTF8);
                    page.ModifiedOn = fi.LastWriteTime;
                    Page.Update(page, new string[] { "Model","ModifiedOn" });
                    Console.WriteLine("Updated page {1} from the file {0}", fi.Name, page.FullPath);
                }
                else
                {
                    PageBase pb = new PageBase()
                    {
                        Id = Guid.NewGuid(),
                        Name = FileName.ToUrlFriendly(),
                        Culture = Culture,
                        Title = FileName,
                        Description = null,
                        Model = System.IO.File.ReadAllText(fi.FullName, Encoding.UTF8),
                        RequireSsl = false,
                        PageTypeId = null,
                        PageFolderId = CurrentFolder.Id.Value,
                        PageSecurityId = 0,
                        PageAudienceId = null,
                        IsExternal = ReserveRoutes,
                        CreatedOn = fi.CreationTime,
                        ModifiedOn = fi.LastWriteTime
                    };
                    page = Page.Create(pb);
                    Console.WriteLine("Created the file {0} as {1}", fi.Name, page.FullPath);
                }

                if (Publish)
                {
                    if (!Page.Publish(page))
                    {
                        Console.WriteLine("/tERROR: the page {0} from the file {1} was not published. You need to publish the page manually", page.Name, fi.Name);
                    }
                }

                if (Move)
                {
                    DeleteFiles.Add(fi.FullName);
                }
            }

            if (Move)
            {
                DeleteFiles.ForEach((file) =>
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("/tERROR: Could not delete the file {0}. Message: {1}", file, e.Message);
                    }
                });
            }

            int index = 0;
            List<string> ControllerNames = new List<string>();
            foreach (System.IO.DirectoryInfo subDir in CurrentDir.EnumerateDirectories())
            {
                BuildPageDirectory(Host, Culture, Folders, UnpublishedPages, Move,ReserveRoutes, Publish, CurrentFolder, subDir, ++index, FolderLevel+ 1);
                if (FolderLevel == 0 && ReserveRoutes && !subDir.Name.Equals("Shared", StringComparison.OrdinalIgnoreCase))
                {
                    ControllerNames.Add(subDir.Name);
                }
            }

            if (ReserveRoutes && ControllerNames.Count > 0)
            {
                ReservedRoutes route = ReservedRoutes.ByName("DefaultMvc");
                if (route != null)
                {
                    ControllerNames.ForEach((name) =>
                    {
                        if (route.Constraints.IndexOf(name,  StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            route.Constraints += "|" + name;
                        }
                    });
                    if (!ReservedRoutes.Update(route))
                    {
                        Console.WriteLine("/tERROR: Could not update the reserved route {0} with the constraints: {1}", route.Name, route.Constraints);
                    }
                    else
                    {
                        Console.WriteLine("Updated the reserved route {0} with the new constraints: {1}", route.Name, route.Constraints);
                    }
                }
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
