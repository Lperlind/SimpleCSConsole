using System;
using System.Reflection;
using System.Collections.Generic;

namespace SimpleCSConsole
{
    public static class ConsoleCommands
    {
        private static Dictionary<String, Action<String[]>> commands;

        static ConsoleCommands()
        {
            commands = new Dictionary<String, Action<String[]>>();
            AddCommand("help",Help);
            LoadCommands();
        }

        //adds a command to the list of commands
        public static void AddCommand(String name, Action<String[]> action)
        {
            if (commands.ContainsKey(name))
            {
                Console.WriteLine($"The command {name} already exists!");
            }

            commands.Add(name, action);
        }

        //try to execute a line from the command
        public static void TryCommand(String line)
        {
            //ignore empty lines and lines that don't start with a /
            if (String.IsNullOrEmpty(line) && line[0] != '/')
                return;

            //split the arguments based on space
            List<String> splitArgs = new List<String>(line.Split(' '));
            //find the first command
            String command = splitArgs[0];
            if (commands.TryGetValue(command.TrimStart('/'), out Action<String[]> action))
            {
                splitArgs.RemoveAt(0);
                action?.Invoke(splitArgs.ToArray());
            }
            else
            {
                Console.WriteLine($"Command {command} does not exist \n");
                Console.WriteLine($"type /help for a list of commands");
            }
        }

        //loads all commands in the assembly
        private static void LoadCommands()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            //for all the types in assembly
            foreach (Type t in myAssembly.GetTypes())
            {
                //get all the static methods withing the type
                MethodInfo[] methods = t.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (MethodInfo m in methods)
                {
                    //if the method has the console command attribute
                    Command cmd = (Command)m.GetCustomAttribute(typeof(Command));
                    if (cmd != null)
                    {
                        //add it to the list of possible commands
                        ConsoleCommands.AddCommand(cmd.commandName, (Action<String[]>)Delegate.CreateDelegate(typeof(Action<String[]>), m, false));
                    }
                }
            }
        }

        private static void Help(String[] args)
        {
            Console.WriteLine("Available Commands:");
            foreach(var cmd in commands.Keys)
            {
                Console.WriteLine($"/{cmd}");
            }
            Console.Write('\n');
        }

    }
}