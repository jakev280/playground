using System;
using SmallGame.Core; // Points to the new Core folder

namespace SmallGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Now identifies Game inside the SmallGame.Core namespace
            Game game = new Game();
            game.Run();
        }
    }
}