using System;

namespace SimpleCSConsole
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Command : Attribute
    {
        public String commandName;

        public Command(String commandName)
        {
            this.commandName = commandName;
        }
    }
}