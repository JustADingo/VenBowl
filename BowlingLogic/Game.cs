using BowlingLogic.Models;

namespace BowlingLogic
{
    public class Game
    {
        public GameStatus ActiveGameStatus { get; set; }
        public Game()
        {
            ActiveGameStatus = new GameStatus();
        }

        public string Throw()
        {
            //In case we make it past the end condition
            if (ActiveGameStatus.CurrentFrame > 9)
            {
                return $"Game Over.  Congratulations, you scored {ActiveGameStatus.TotalScore}";
            }

            string throwDescription = "";

            Frame currFrame = ActiveGameStatus.Frames[ActiveGameStatus.CurrentFrame];
            int currThrowScore = 0;

            //First 9 frames
            if (ActiveGameStatus.CurrentFrame < 9)
            {
                //First Throw
                if (ActiveGameStatus.CurrentThrow == 0)
                {
                    currThrowScore = GetKnockedDownPins(10);
                    currFrame.Throw1 = currThrowScore;
                    bool endFrame = false;

                    if (currThrowScore == 10)
                    {
                        endFrame = true;
                        currFrame.LookAheadThrows = 2;
                        currFrame.FrameRunningTotal = currThrowScore;
                        throwDescription = "Strike!";
                    }
                    else
                    {
                        ActiveGameStatus.CurrentThrow = 1;
                        currFrame.FrameRunningTotal = currThrowScore;
                        throwDescription = $"You rolled a {currThrowScore}";
                    }

                    //Add throw to any applicable previous frames.  Only possible for previous 2 frames
                    for (int i = 2; i > 0; i--)
                    {
                        if (ActiveGameStatus.CurrentFrame - i >= 0)
                        {
                            ReconcilePreviousFrames(ActiveGameStatus.CurrentFrame - i, currThrowScore);
                        }
                    }

                    if (endFrame)
                    {
                        ActiveGameStatus.CurrentThrow = 0;
                        ActiveGameStatus.CurrentFrame++;
                    }
                }
                //Second Throw
                else if (ActiveGameStatus.CurrentThrow == 1)
                {
                    currThrowScore = GetKnockedDownPins(10 - currFrame.Throw1 ?? 0);
                    currFrame.Throw2 = currThrowScore;

                    if (currThrowScore + currFrame.Throw1 == 10)
                    {
                        currFrame.LookAheadThrows = 1;
                        currFrame.FrameRunningTotal += currThrowScore;
                        throwDescription = "Spare!";
                    }
                    else
                    {
                        currFrame.FrameTotal = (currFrame.Throw1 ?? 0) + (currFrame.Throw2 ?? 0);
                        throwDescription = $"You rolled a {currThrowScore}";
                    }

                    //Add throw to applicable previous frame.  Only possible for previous frame
                    if (ActiveGameStatus.CurrentFrame - 1 >= 0)
                    {
                        ReconcilePreviousFrames(ActiveGameStatus.CurrentFrame - 1, currThrowScore);
                    }

                    ActiveGameStatus.CurrentFrame++;
                    ActiveGameStatus.CurrentThrow = 0;
                }
            }
            //Tenth frame
            else
            {
                //First Throw
                if (ActiveGameStatus.CurrentThrow == 0)
                {
                    currThrowScore = GetKnockedDownPins(10);
                    currFrame.Throw1 = currThrowScore;

                    if (currThrowScore == 10)
                    {
                        currFrame.LookAheadThrows = 2;
                        currFrame.FrameRunningTotal = currThrowScore;
                        throwDescription = "Strike!";
                    }
                    else
                    {
                        ActiveGameStatus.CurrentThrow = 1;
                        currFrame.FrameRunningTotal = currThrowScore;
                        throwDescription = $"You rolled a {currThrowScore}";
                    }
                    for (int i = 2; i > 0; i--)
                    {
                        if (ActiveGameStatus.CurrentFrame - i >= 0)
                        {
                            ReconcilePreviousFrames(ActiveGameStatus.CurrentFrame - i, currThrowScore);
                        }
                    }
                    ActiveGameStatus.CurrentThrow = 1;
                }
                //Second Throw
                else if (ActiveGameStatus.CurrentThrow == 1)
                {
                    int throw1 = currFrame.Throw1 ?? 0;
                    currThrowScore = GetKnockedDownPins(10 - (throw1 == 10 ? 0 : throw1));
                    currFrame.Throw2 = currThrowScore;

                    if (currThrowScore + currFrame.Throw1 == 10)
                    {
                        currFrame.LookAheadThrows = 1;
                        currFrame.FrameRunningTotal += currThrowScore;
                        throwDescription = "Spare!";
                    }
                    else
                    {
                        currFrame.FrameTotal = (currFrame.Throw1 ?? 0) + (currFrame.Throw2 ?? 0);
                        throwDescription = $"You rolled a {currThrowScore}";
                    }

                    //Add throw to applicable previous frame.  Only possible for previous frame
                    if (ActiveGameStatus.CurrentFrame - 1 >= 0)
                    {
                        ReconcilePreviousFrames(ActiveGameStatus.CurrentFrame - 1, currThrowScore);
                    }

                    if ((currFrame.Throw1 ?? 0) + (currFrame.Throw2 ?? 0) >= 10)
                    {
                        ActiveGameStatus.CurrentThrow = 2;
                    }
                    else
                    {
                        currFrame.FrameTotal = (currFrame.Throw1 ?? 0) + (currFrame.Throw2 ?? 0);
                        ActiveGameStatus.IsActive = false;
                    }
                }
                //Third Throw if necessary
                else if (ActiveGameStatus.CurrentThrow == 2)
                {
                    int throw1 = currFrame.Throw1 ?? 0;
                    int throw2 = currFrame.Throw2 ?? 0;
                    currThrowScore = GetKnockedDownPins(throw1 == 10 ? 10 - (throw2 == 10 ? 0 : throw2) : 10);
                    currFrame.Throw3 = currThrowScore;
                    currFrame.FrameTotal = (currFrame.Throw1 ?? 0) + (currFrame.Throw2 ?? 0) + (currFrame.Throw3 ?? 0);

                    ActiveGameStatus.IsActive = false;
                }
            }

                return throwDescription;
        }

        //Takes the number of pins left standing and knocks a random amount of them down
        private int GetKnockedDownPins(int pinsLeft)
        {
            int knockedDownPins = 0;

            Random rand = new Random();
            knockedDownPins = rand.Next(pinsLeft + 1);

            return knockedDownPins;
        }

        //Handle adding scores to previous strikes and spares
        private void ReconcilePreviousFrames(int targetFrame, int currThrowScore)
        {
            Frame prevFrame = ActiveGameStatus.Frames[targetFrame];
            if (prevFrame.LookAheadThrows > 0)
            {
                prevFrame.FrameRunningTotal += currThrowScore;
                prevFrame.LookAheadThrows--;

                if (prevFrame.LookAheadThrows == 0)
                {
                    prevFrame.FrameTotal = prevFrame.FrameRunningTotal;
                }
            }
        }
    }
}