using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace LeaderboardComponents
{
    public class LeaderboardView : MonoBehaviour
    {
        private List<LeaderboardViewElement> _elements;
        private Leaderboard _leaderboard;
        private LeaderboardViewElement _elementPrefab;

        public LeaderboardView Init(bool isClassicGame)
        {
            _elements = new List<LeaderboardViewElement>(10);
            _leaderboard = new Leaderboard(isClassicGame);
            LoadPrefab();
            return this;
        }

        public LeaderboardView Init(Leaderboard leaderboard)
        {
            _elements = new List<LeaderboardViewElement>(10);
            _leaderboard = leaderboard;
            LoadPrefab();
            return this;
        }

        private async void LoadPrefab()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("LeaderboardViewElement");
            await handle.Task;
            _elementPrefab = handle.Result.GetComponent<LeaderboardViewElement>();
            Addressables.Release(handle);

            CreateElements();
        }

        private void CreateElements()
        {
            int i = 1;
            foreach (var e in _leaderboard.Records)
            {
                var element = Instantiate(_elementPrefab, transform).Init();
                if (e != null)
                {
                    element.UpdateText(i, e.Score, e.Time, e.Date);
                }
                else
                {
                    element.UpdateText(i);
                }

                if(i%2 == 1)
                {
                    element.Image.color = new Color(0f, 0f, 0f, 0f);
                }

                _elements.Add(element);
                i++;
            }
        }
    }
}