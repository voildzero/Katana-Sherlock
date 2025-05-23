using System;
using System.Threading.Tasks;
using Katana_Sherlock;

namespace Katana_Sherlock
{
    class Program
    {
        static async Task Main(string[] args)
        {

            DrawLogo logo = new DrawLogo();
            logo.Clear();
            logo.Draw();
            Console.ResetColor();
            Intro intro = new Intro();
            intro.PrintIntro();

            UsernameChecker checker = new UsernameChecker();
            await checker.RunAsync();
            Console.WriteLine("");
        }
    }
}