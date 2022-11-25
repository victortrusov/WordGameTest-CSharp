using System;

namespace WordGame
{
    public struct LeaderBoardScore : IComparable<LeaderBoardScore>
    {
        private int wordScore;
        private long timeTicks;

        public LeaderBoardScore(int wordScore, DateTime dateTime)
        {
            this.wordScore = wordScore;
            this.timeTicks = dateTime.Ticks;
        }

        public int CompareTo(LeaderBoardScore another)
        {
            if (wordScore != another.wordScore)
                return another.wordScore - wordScore;

            return (int)(timeTicks - another.timeTicks);
        }
    }
}
