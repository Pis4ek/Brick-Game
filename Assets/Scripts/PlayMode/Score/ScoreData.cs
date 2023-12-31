﻿using System;
using UniRx;

namespace PlayMode.Score
{
    public class ScoreData : IReadOnlyScoreData
    {
        /*public event Action OnValueChangedEvent;

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnValueChangedEvent?.Invoke();
            }
        }

        private int _score = 0;*/

        public ReactiveProperty<int> score = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> Score => score;
    }
}