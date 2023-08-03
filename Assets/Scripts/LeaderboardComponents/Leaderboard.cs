using PlayMode.GameResultCalculation;
using Services;
using Services.Storage;
using Services.Timer;
using System;
using System.Collections.Generic;

namespace LeaderboardComponents
{
    public class Leaderboard : IService
    {
        private const string CLASSIC_SAVE_KEY = "Classic play leaderboard";
        private const string CUSTOM_SAVE_KEY = "Custom play leaderboard";

        private IStorageService _storageService;
        private LinkedList<LeaderbordElement> _leaders;

        public Leaderboard(bool isClassicGame)
        {
            _storageService = new BinaryStorageService();

            InitLeaderboardList(isClassicGame);
        }

        public bool TryAddGameResult(int score, GameTime time, out LeaderbordElement leaderboardPlace)
        {
            leaderboardPlace = null;

            if (_leaders != null && _leaders.Last != null)
            {
                if (score > _leaders.Last.Value.Score)
                {
                    var current = _leaders.Last.Previous;
                    for (int i = current.Value.LeaderboardPlace; i > 0; i--)
                    {
                        if(score < current.Value.Score)
                        {
                            leaderboardPlace = AddElementInLeaderboard(
                                current, score, time, current.Value.LeaderboardPlace + 1);

                            return true;
                        }
                        current = current.Previous;
                    }

                    leaderboardPlace = AddElementInLeaderboard(current, score, time, 1);
                    return true;
                }
                return false;
            }
            return false;
        }

        private LeaderbordElement AddElementInLeaderboard(LinkedListNode<LeaderbordElement> current, 
            int score, GameTime time, int place)
        {
            var leaderboardPlace = new LeaderbordElement(score, time, place, DateTime.Today);
            _leaders.AddAfter(current, leaderboardPlace);

            if (_leaders.Count > 10)
            {
                _leaders.RemoveLast();
            }
            return leaderboardPlace;
        }

        private void InitLeaderboardList(bool isClassicGame)
        {
            try
            {
                if (isClassicGame)
                {
                    _storageService.Load(CLASSIC_SAVE_KEY,
                        delegate (LinkedList<LeaderbordElement> list) { _leaders = list; });
                }
                else
                {
                    _storageService.Load(CUSTOM_SAVE_KEY,
                        delegate (LinkedList<LeaderbordElement> list) { _leaders = list; });
                }
            }
            catch
            {
                _leaders = new LinkedList<LeaderbordElement>();
            }
        }
    }
}
