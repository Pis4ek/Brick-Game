using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] Text _secondsSinceStartText;
        [SerializeField] Text _minutesSinceStartText;

        private Timer _timer;

        public TimerView Init(Timer timer)
        {
            _timer = timer;
            _timer.OnSecondTickedEvent += delegate ()
            {
                UpdateValues();
            };
            return this;
        }

        public void UpdateValues()
        {
            _secondsSinceStartText.text = $"SecondsSinceStart: {_timer.SecondsSinceStart}";
            _minutesSinceStartText.text = $"MinutesSinceStart: {_timer.MinutesSinceStart}";
        }
    }
}
