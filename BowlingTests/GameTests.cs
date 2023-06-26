using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingLogic;
using BowlingLogic.Models;

namespace BowlingTests
{
    [TestClass]
    public class GameTests
    {
        private Game _game;

        public GameTests()
        {
            _game = new Game();
        }

        [TestMethod]
        public void Test_Strike_Adds_Next_Two()
        {
            _game = new Game();
            Frame baseFrame = _game.ActiveGameStatus.Frames[2];
            baseFrame.Throw1 = 10;
            baseFrame.FrameRunningTotal = 10;
            baseFrame.LookAheadThrows = 2;
            _game.ActiveGameStatus.CurrentThrow = 0;
            _game.ActiveGameStatus.CurrentFrame = 3;

            _game.Throw();
            _game.Throw();
            _game.Throw();
            Frame frame2 = _game.ActiveGameStatus.Frames[3];
            int next1 = frame2.Throw1 ?? 0;
            int next2 = (frame2.Throw1 ?? 0) < 10 ? (frame2.Throw2 ?? 0) : (_game.ActiveGameStatus.Frames[3].Throw1 ?? 0);

            Assert.AreEqual(baseFrame.FrameTotal, 10 + next1 + next2);
            
        }

        [TestMethod]
        public void Test_Spare_Adds_Next_One()
        {
            _game = new Game();
            Frame baseFrame = _game.ActiveGameStatus.Frames[2];
            baseFrame.Throw1 = 3;
            baseFrame.Throw2 = 7;
            baseFrame.FrameRunningTotal = 10;
            baseFrame.LookAheadThrows = 1;
            _game.ActiveGameStatus.CurrentThrow = 0;
            _game.ActiveGameStatus.CurrentFrame = 3;

            _game.Throw();
            _game.Throw();
            Frame frame2 = _game.ActiveGameStatus.Frames[3];
            int next1 = frame2.Throw1 ?? 0;

            Assert.AreEqual(baseFrame.FrameTotal, 10 + next1);

        }

        [TestMethod]
        public void Test_Tenth_Frame_Third_Roll_On_Strike()
        {
            _game = new Game();
            Frame tenthFrame = _game.ActiveGameStatus.Frames[9];
            tenthFrame.Throw1 = 10;
            tenthFrame.FrameRunningTotal = 10;
            tenthFrame.LookAheadThrows = 2;
            _game.ActiveGameStatus.CurrentThrow = 1;
            _game.ActiveGameStatus.CurrentFrame = 9;

            _game.Throw();
            _game.Throw();

            Assert.IsNotNull(tenthFrame.Throw3);
            Assert.AreEqual(tenthFrame.FrameTotal, tenthFrame.Throw1 + tenthFrame.Throw2 + tenthFrame.Throw3);
        }

        [TestMethod]
        public void Test_Tenth_Frame_Third_Roll_On_Spare()
        {
            _game = new Game();
            Frame tenthFrame = _game.ActiveGameStatus.Frames[9];
            tenthFrame.Throw1 = 3;
            tenthFrame.Throw2 = 7;
            tenthFrame.FrameRunningTotal = 10;
            tenthFrame.LookAheadThrows = 1;
            _game.ActiveGameStatus.CurrentThrow = 2;
            _game.ActiveGameStatus.CurrentFrame = 9;

            _game.Throw();

            Assert.IsNotNull(tenthFrame.Throw3);
            Assert.AreEqual(tenthFrame.FrameTotal, tenthFrame.Throw1 + tenthFrame.Throw2 + tenthFrame.Throw3);
        }
    }
}