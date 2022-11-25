using System.Linq;
using System.Collections.Generic;
namespace WordGame
{
    using System;

    public class WordGameService : IWordGameService
    {
        private const int maxLeaderBoardCount = 10;

        private readonly object submissionLock = new();
        private readonly IValidWords validWords;
        private char[] initialWordCharArray;

        private SortedList<LeaderBoardScore, (string PlayerName, string Word, int Points)> leaderBoard = new();
        private HashSet<string> usedWordsSet = new();

        public WordGameService(string letters, IValidWords validWords) :
            this(letters.ToCharArray(), validWords)
        { }

        public WordGameService(char[] letters, IValidWords validWords)
        {
            this.validWords = validWords;
            initialWordCharArray = letters;
        }

        public string GetPlayerNameAtPosition(int position) =>
            GetLeaderBoardEntry(position)?.PlayerName;

        public int? GetScoreAtPosition(int position) =>
            GetLeaderBoardEntry(position)?.Points;

        public string GetWordEntryAtPosition(int position) =>
            GetLeaderBoardEntry(position)?.Word;


        private (string PlayerName, string Word, int Points)? GetLeaderBoardEntry(int position)
        {
            if (position < 0 || position >= leaderBoard.Values.Count)
                return null;

            lock (submissionLock)
            {
                return leaderBoard.Values[position];
            }
        }

        public int? SubmitWord(string playerName, string word)
        {
            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(word))
                return null;

            if (!IsWordValid(word))
                return null;
            
            var points = CalculatePoints(word);
            AddWord(playerName, word, points);

            return points;
        }

        private bool IsWordValid(string word)
        {
            var wrongChars = word.ToList();
            foreach (var ch in initialWordCharArray) {
                wrongChars.Remove(ch);
            }

            if (wrongChars.Count > 0)
                return false;

            lock (submissionLock)
            {
                if (usedWordsSet.Contains(word))
                    return false;
            }

            if (!validWords.Contains(word))
                return false;

            return true;
        }

        private void AddWord(string playerName, string word, int points)
        {
            lock (submissionLock)
            {
                usedWordsSet.Add(word);

                leaderBoard.Add(
                    new LeaderBoardScore(points, DateTime.Now),
                    (playerName, word, points)
                );

                if (leaderBoard.Count > maxLeaderBoardCount)
                {
                    leaderBoard.RemoveAt(maxLeaderBoardCount);
                }
            }
        }

        private int CalculatePoints(string word) =>
            word.Length;
    }
}
