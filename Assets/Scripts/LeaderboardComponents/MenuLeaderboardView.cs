﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeaderboardComponents
{
    public class MenuLeaderboardView : MonoBehaviour
    {
        private List<LeaderboardViewElement> _elements;
        private Leaderboard _leaderboard;
        private LeaderboardViewElement _elementPrefab;

        public MenuLeaderboardView Init()
        {
            _elements = new List<LeaderboardViewElement>(10);
            _leaderboard = new Leaderboard(true);
            LoadPrefab();
            return this;
        }

        private void LoadPrefab()
        {
            _elementPrefab = Resources.Load<LeaderboardViewElement>("Place");

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

                if (i % 2 == 1)
                {
                    element.Image.color = new Color(0f, 0f, 0f, 0f);
                }

                _elements.Add(element);
                i++;
            }
        }
    }
}