using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] Text _scoreText;

        public bool IsInitialized { get; private set; } = false;

        private ScoreCounter _counter;

        public ScoreView Init(ScoreCounter counter)
        {
            _counter = counter;
            IsInitialized = true;

            _counter.OnValueChangedEvent += ChangeText;

            return this;
        }

        private void ChangeText()
        {
            _scoreText.text = $"Score: {_counter.Score}";
        }
    }
}
