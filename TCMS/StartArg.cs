using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMS
{
    public static class StartArgExtensions
    {
        public static bool Has(this List<StartArg> list, string name)
        {
            return list.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static T Get<T>(this List<StartArg> list, string name, T DefaultValue = default(T))
        {
            StartArg arg = list.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (arg == null)
            {
                return DefaultValue;
            }
            else
            {
                if (string.IsNullOrEmpty(arg.Parameter) && typeof(T) == typeof(bool))
                {
                    return (T)Convert.ChangeType(true, typeof(bool));
                }
                return (T)Convert.ChangeType(arg.Parameter, typeof(T));
            }
        }
    }
    public class StartArg
    {
        public string Name { get; set; }
        public string Parameter { get; set; }

        private static string[] SplitCommand(string cmd)
        {
            int i = cmd.IndexOf(':');
            if (i < 0)
            {
                return new string[] { cmd };
            }
            else
            {
                return new string[] { cmd.Substring(0, i), cmd.Substring(i + 1) };
            }

        }
        public static List<StartArg> ParseArguments()
        {
            List<StartArg> commands = new List<StartArg>();
            string[] s = System.Environment.GetCommandLineArgs();
            foreach (string arg in s)
            {
                string[] cmd = SplitCommand(arg);
                if (cmd.Length == 2)
                {
                    commands.Add(new StartArg()
                    {
                        Name = cmd[0].ToLower(),
                        Parameter = cmd[1].Trim('\"')

                    });
                }
                else
                {
                    commands.Add(new StartArg() { Name = cmd[0].ToLower() });
                }
            }
            return commands;
        }


    }


}
