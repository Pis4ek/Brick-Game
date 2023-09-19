using Services.Timer;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LeaderboardComponents
{
    [RequireComponent(typeof(Image))]
    public class LeaderboardViewElement : MonoBehaviour
    {
        public Image Image { get; private set; }
        public TextMeshProUGUI PlaceText { get; private set; }
        public TextMeshProUGUI ScoreText { get; private set; }
        public TextMeshProUGUI TimeText { get; private set; }
        public TextMeshProUGUI DateText { get; private set; }

        public LeaderboardViewElement Init()
        {
            Image = GetComponent<Image>();
            PlaceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            ScoreText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TimeText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            DateText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

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
        public void UpdateText(string colName1, string colName2, string colName3, string colName4)
        {
            PlaceText.text = colName1;
            ScoreText.text = colName2;
            TimeText.text = colName3;
            DateText.text = colName4;
        }
    }
}