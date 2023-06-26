namespace BowlingLogic.Models
{
    public class Frame
    {
        public int FrameNumber { get; set; }
        public int? Throw1 { get; set; }
        public int? Throw2 { get; set; }
        public int? Throw3 { get; set; }
        public string Throw1Display 
        { 
            get { return Throw1 == 10 ? "X" : Throw1 == 0 ? "-" : Throw1.ToString(); } 
        }
        public string Throw2Display
        {
            //get { return (Throw1 + Throw2 == 10) ? (FrameNumber == 10 && Throw1 == 10 ? "X" : "/") : Throw2 == 0 ? "-" : Throw2.ToString(); }
            get
            {
                if (Throw2 == 0)
                {
                    return "-";
                }
                else if (FrameNumber == 10)
                {
                    if (Throw1 == 10)
                    {
                        if (Throw2 == 10)
                        {
                            return "X";
                        }
                        else
                        {
                            if (Throw1 + Throw2 == 10)
                            {
                                return "/";
                            }
                            else
                            {
                                return Throw2.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (Throw1 + Throw2 == 10)
                        {
                            return "/";
                        }
                        else
                        {
                            return Throw2.ToString();
                        }
                    }
                }
                else
                {
                    if (Throw1 + Throw2 == 10)
                    {
                        return "/";
                    }
                    else
                    {
                        return Throw2.ToString();
                    }
                }
            }
        }
        public string Throw3Display
        {
            get { return Throw3 == 10 ? "X" : Throw3 == 0 ? "-" : Throw3.ToString(); }
        }
        public int FrameTotal { get; set; }
        public int FrameRunningTotal { get; set; }
        public int LookAheadThrows { get; set; }

        public Frame(int frameNumber)
        {
            FrameNumber = frameNumber;
            Throw1 = null;
            Throw2 = null;
            Throw3 = null;
            FrameTotal = 0;
            FrameRunningTotal = 0;
            LookAheadThrows = 0;
        }

    }
}
