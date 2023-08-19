using PlayMode.GameResultCalculation;
using Services;
using Services.Storage;
using Services.Timer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeaderboardComponents
{
    public class Leaderboard : IService
    {
        private const string CLASSIC_SAVE_KEY = "Classic play leaderboard";
        private const string CUSTOM_SAVE_KEY = "Custom play leaderboard";

        private IStorageService _storageService;
        public GameResult[] Records;

        public Leaderboard(bool isClassicGame)
        {
            _storageService = new BinaryStorageService();
            TryLoadResultList(isClassicGame);
        }

        public bool TryAddGameResult(int score, GameTime time)
        {
            if (Records != null)
            {
                for (int i = 0; i < Records.Length; i++)
                {
                    if (Records[i] == null)
                    {
                        Records[i] = new GameResult(score, time);
                        _storageService.Save(CLASSIC_SAVE_KEY, Records);
                        return true;
                    }
                    else if (score > Records[i].Score)
                    {
                        for (int j = Records.Length - 1; j > i; j--)
                        {
                            Records[j] = Records[j - 1];
                        }
                        Records[i] = new GameResult(score, time);
                        _storageService.Save(CLASSIC_SAVE_KEY, Records);
                        return true;
                    }
                }
                return false;
            }
            throw new Exception("Leaderboard list is null. Can not add new element.");
        }

        private void TryLoadResultList(bool isClassicGame)
        {
            try
            {
                if (isClassicGame)
                    _storageService.Load(CLASSIC_SAVE_KEY, delegate (GameResult[] list) { Records = list; });
                else
                    _storageService.Load(CUSTOM_SAVE_KEY, delegate (GameResult[] list) { Records = list; });
            }
            catch
            {
                Records = new GameResult[10];
            }
        }
    }
}
