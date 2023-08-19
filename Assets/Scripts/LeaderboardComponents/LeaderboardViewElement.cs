using Services.Timer;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LeaderboardComponents
{
    [RequireComponent(typeof(Image))]
    public class LeaderboardViewElement : MonoBehaviour
    {
        public Image Image { get; private set; }
        public Text PlaceText { get; private set; }
        public Text ScoreText { get; private set; }
        public Text TimeText { get; private set; }
        public Text DateText { get; private set; }

        public LeaderboardViewElement Init()
        {
            Image = GetComponent<Image>();
            PlaceText = transform.GetChild(0).GetComponent<Text>();
            ScoreText = transform.GetChild(1).GetComponent<Text>();
            TimeText = transform.GetChild(2).GetComponent<Text>();
            DateText = transform.GetChild(3).GetComponent<Text>();

            return this;
        }

        public void UpdateText(int place, int score, GameTime time, DateTime date)
        {
            PlaceText.text = $"{place:d2}";
            ScoreText.text = $"{score:d5}";
            TimeText.text = $"{time.Minutes:d2}:{time.Seconds:d2}";
            DateText.text = $"{date.Day:d2}.{date.Month:d2}.{date.Year}";
        }

        public void UpdateText(int place)
        {
            PlaceText.text = $"{place:d2}";
            ScoreText.text = "---";
            TimeText.text = "---";
            DateText.text = "---";
        }
    }
}