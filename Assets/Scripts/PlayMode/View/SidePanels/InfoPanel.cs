using PlayMode.Level;
using PlayMode.Score;
using Services.Timer;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace PlayMode.View
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] Text _scoreText;
        [SerializeField] Text _timerText;
        [SerializeField] Text _levelText;

        private IReadOnlyScoreData _score;
        private IReadOnlyTimerData _timer;
        private IReadOnlyLevelData _level;

        public InfoPanel Init(IReadOnlyScoreData score, IReadOnlyTimerData timer, IReadOnlyLevelData level)
        {
            _score = score;
            _timer = timer;
            _level = level;

            _score.Score.Subscribe(_ => { _scoreText.text = $"Score\n{_score.Score.Value:d5}"; }).AddTo(this);
            _timer.SecondsSinceStart.Subscribe(_ => { _timerText.text = $"Time\n{_timer.GameTime}"; }).AddTo(this);
            _level.Level.Subscribe(_ => { _levelText.text = $"Level\n{_level.Level:d2}"; }).AddTo(this);

            return this;
        }
    }
}
