using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.GameResultCalculation
{
    public class GameResultView : MonoBehaviour
    {
        [SerializeField] GameObject _gameResultScreen;
        [SerializeField] Text _scoreText;
        [SerializeField] Text _timeText;

        public GameResultView Init(GameResultCalculator gameResultCalculator)
        {
            _gameResultScreen.SetActive(false);

            gameResultCalculator.OnResultCalculated += delegate (GameResult result)
            {
                _gameResultScreen.SetActive(true);
                _scoreText.text = $"Score: {result.Score}";
                _timeText.text = $"Time: {result.Time}";
            };

            return this;
        }
    }
}