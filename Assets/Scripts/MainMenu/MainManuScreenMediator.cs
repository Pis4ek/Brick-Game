using System.Collections;
using UnityEngine;

namespace MainMenu
{
    public class MainManuScreenMediator : IMediator
    {
        private MainManuView _mainManuView;
        private StartGameMenuView _startGameView;
        private SettingsMenuView _settingsView;
        private ScoreTableMenu _scoreTableMenu;

        public MainManuScreenMediator(MainManuView mainManuView, StartGameMenuView startGameView, 
            SettingsMenuView settingsView, ScoreTableMenu scoreTableMenu)
        {
            _mainManuView = mainManuView;
            _startGameView = startGameView;
            _settingsView = settingsView;
            _scoreTableMenu = scoreTableMenu;
        }

        public void Notify(object sender, string ev)
        {
            if(ev == "OpenStartGameMenu")
            {
                _startGameView.Activate();
                _mainManuView.Disactivate();
            }
            if (ev == "OpenScoreMenu")
            {
                _scoreTableMenu.Activate();
                _mainManuView.Disactivate();
            }
            if (ev == "OpenSettingsMenu")
            {
                _settingsView.Activate();
                _mainManuView.Disactivate();
            }
            if(ev == "Back")
            {
                _mainManuView.Activate();
                _startGameView.Disactivate();
                _settingsView.Disactivate();
                _scoreTableMenu.Disactivate();
            }
        }
    }
}