using System;

namespace FileManager
{
    class Programm
    {
        static string _path = string.Empty;
        static void Main(string[] args)
        {
            Commands commands = new Commands();

            bool isFirstStart = true;

            while (true)
            {
                if (isFirstStart)
                {
                    commands.Command("!help");
                    isFirstStart = false;
                }

                commands.Command(Console.ReadLine());
            }
        }
    }
}