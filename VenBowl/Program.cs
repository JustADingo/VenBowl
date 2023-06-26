// See https://aka.ms/new-console-template for more information
using BowlingLogic;
using BowlingLogic.Models;


Console.WriteLine("Welcome to VenBowl!" + Environment.NewLine);

while (true)
{
    Console.WriteLine("Would you like to start a new game? Y/N");
    string input = Console.ReadLine();

    if (input.ToUpper() != "N")
    {
        Game _game = new Game();
        string throwDescription = "";

        //Play the game until it finishes
        while (_game.ActiveGameStatus.IsActive)
        {

            await Task.Delay(1000);

            Console.Clear();
            Console.WriteLine("Welcome to VenBowl!" + Environment.NewLine);

            throwDescription = _game.Throw();


            Console.WriteLine(throwDescription);

            foreach (Frame currFrame in _game.ActiveGameStatus.Frames)
            {
                Console.WriteLine($"Frame {currFrame.FrameNumber}:  {currFrame.Throw1Display}   |   {currFrame.Throw2Display}  |   {currFrame.Throw3Display}  Frame Total: {currFrame.FrameTotal}");
            }

            Console.WriteLine($"Total: {_game.ActiveGameStatus.TotalScore}");
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }
    else
    {
        break;
    }
}
