using LeaderboardComponents;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MainMenu
{
    public class ScoreTableMenu : MonoBehaviour
    {
        [SerializeField] ConfirmationWidget _confirmation;
        [SerializeField] LeaderboardView _leaderboardView;
        [SerializeField] TextMeshProUGUI _modeTitleText;

        public IMediator Mediator;

        private bool _isClassicMode = true;

        public ScoreTableMenu Init()
        {
            _confirmation.OnClickNo += delegate () { Mediator.Notify(this, "Back"); };
            _confirmation.OnClickYes += SwitchMode;

            _leaderboardView.Init(_isClassicMode);

            return this;
        }

        private void SwitchMode()
        {
            _isClassicMode = !_isClassicMode;

            if (_isClassicMode)
            {
                _modeTitleText.text = "Classic mode records";
            }
            else
            {
                _modeTitleText.text = "Custom mode records";
            }

            _leaderboardView.Init(_isClassicMode);
        }

    }
}