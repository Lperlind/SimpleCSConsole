# SimpleCSConsole
A simple to use console for c#

## Usage

### Adding a Command
Just add the attribute [Command("")] on any method that takes an array of strings as an argument.
The method must be marked as static
```cs
using SimpleCSConsole;

[Command("foo")]
private static void Bar(string[] args)
{
  //execute your code
}
```
The above example can be executed typing in "/foo".

### Executing a Command
Will try and execute a command
```cs
using SimpleCSConsole;

//explicitly try and run the command
void Explicit()
{
  ConsoleCommands.TryCommand("/foo");
}

//run a command based on user input, will ignore bad commands
void ThroughConsole()
{
  ConsoleCommands.TryCommand(Console.NextLine());
}
```

### Notes
* You cannot have duplicate command names
* The command /help will print all loaded commands to the console
