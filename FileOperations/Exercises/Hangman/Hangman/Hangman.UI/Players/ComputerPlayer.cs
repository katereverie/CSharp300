﻿using Hangman.UI.Interfaces;
using Hangman.UI.WordSources;

namespace Hangman.UI.Players
{
    public class ComputerPlayer : IPlayer
    {
        private static Random _letterPicker = new Random();
        private static char[] _alphabet = new char[26];
        private static List<char> _guessedLetter = new List<char>();
        public string Name { get; }
        public bool IsHuman { get; }
        public IWordSource WordSource { get; }
        

        public ComputerPlayer(string name)
        {
            Name = name;
            IsHuman = false;
            WordSource = new DefaultDictionary();
            for (int i = 0; i < 26; i++)
            {
                _alphabet[i] = (char)('A' + i);
            }
        }

        public string GetGuess()
        {
            char guess;

            do
            {
                guess = _alphabet[_letterPicker.Next(0, 26)];

                if (_guessedLetter.Contains(guess))
                {
                    continue;
                }

                _guessedLetter.Add(guess);

                return guess.ToString();

            } while (true);

        }
    }
}
