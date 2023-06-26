namespace BowlingLogic.Models
{
    public class GameStatus
    {
        public int TotalScore { get { return Frames.Sum(x => x.FrameTotal); } }
        public List<Frame> Frames { get; set; } = new List<Frame>();
        public int CurrentFrame { get; set; }
        public int CurrentThrow { get; set; }
        public bool IsActive { get; set; }

        public GameStatus()
        {
            Frames = new List<Frame>();
            CurrentThrow = 0;
            CurrentFrame = 0;
            IsActive = true;

            for (int i = 0; i < 10; i++)
            {
                Frames.Add(new Frame(i + 1));
            }
        }
    }
}
