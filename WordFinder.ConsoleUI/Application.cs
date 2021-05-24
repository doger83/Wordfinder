﻿using System;
using System.Collections.Generic;
using WordFinder.ConsoleUI.Massages;
using WordFinder.Data;
using WordFinder.Core;
using System.Threading.Tasks;

namespace WordFinder.ConsoleUI
{
    class Application
    {
        private bool running;
        IEnumerable<string> resultWords;

        internal Application()
        {
            Initialize();
        }

        internal void Run()
        {
            while (running)
            {
                UIManager.AskForNewWord(out string baseWord);

                try
                {
                    //Wordfinder.FindPossibleWords_static(baseWord, out SortedSet<string> resultWords);
                    resultWords = Wordfinder.FindPossibleWords_yield(baseWord);

                    UIManager.PrintWordList(resultWords, out int possibleWordsCount);
                    UIManager.PrintGeneratedWordsCount(possibleWordsCount, baseWord);

                    UIManager.TryAgainMassage(ref running);
                }
                catch (Exception e)
                {
                    UIManager.ShowErrorMassage(e.Message);
                    running = false;
                }
            }
            UIManager.ProgrammEndsMassage();
        }

        private void Initialize()
        {
            running = true;
            Console.SetBufferSize(160, short.MaxValue - 1);
            Console.SetWindowSize(160, 65);
            //resultWords = new SortedSet<string>();
        }
    }
}


